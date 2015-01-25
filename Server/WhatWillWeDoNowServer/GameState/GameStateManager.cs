using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WhatWillWeDoNowServer.Models;

namespace WhatWillWeDoNowServer.GameState
{
    public class GameStateManager
    {
        public static GameStateManager Instance = new GameStateManager();

        public const int PlayerTimeout = 10;
        public const int GameOverTimeout = 20;

        private readonly Dictionary<string, Scenario> _scenarios;
        public IList<Player> Players { get; set; }

        public string CurrentScenarioKey { get; set; }
        public Scenario CurrentScenario
        {
            get
            {
                if (!_scenarios.ContainsKey(CurrentScenarioKey))
                    return null;
                return _scenarios[CurrentScenarioKey];
            }
        }

        public string UpdateText { get; set; }

        public const int RequiredPlayers = 6;

        private readonly Timer _updateTimer;

        private DateTime _resetGameAt;

        private  enum GameState
        {
            WaitingForPlayers,
            InProgress,
            GameOver
        }

        private GameState _gameState;

        public GameStateManager()
        {
            _scenarios = Scenarios.CreateScenarios();
            Players = new List<Player>();
            _gameState = GameState.WaitingForPlayers;
            ResetScenario();

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += Update;
            _updateTimer.Enabled = true;
        }

        private void Update(object sender, ElapsedEventArgs e)
        {
            if (_gameState == GameState.InProgress)
                CheckForTimeouts();

            if ((_gameState == GameState.GameOver) && (DateTime.Now >= _resetGameAt))
                ResetGame();
        }

        private void ResetGame()
        {
            _gameState = GameState.WaitingForPlayers;
            Players.Clear();
            ResetScenario();
        }

        private void CheckForTimeouts()
        {
            bool hasTimedOut = false;
            foreach (var player in Players.Where(p => p.IsAlive))
            {
                if ((DateTime.Now - player.LastRequest).TotalSeconds > PlayerTimeout)
                {
                    hasTimedOut = true;
                    player.HitPoints = 0;
                }
            }
            if (hasTimedOut)
                CheckChoices();
        }

        private void ResetScenario()
        {
            CurrentScenarioKey = "A";
            UpdateText = "";
        }

        public RequestJoinModel RequestJoin(string playerName)
        {
            if (_gameState != GameState.WaitingForPlayers)
                return new RequestJoinModel { PlayerValid = false, PlayerNumber = 0, PlayerMessage = "Game Already In Progress" };

            if (Players.Count >= RequiredPlayers)
                return new RequestJoinModel { PlayerValid = false, PlayerNumber = 0, PlayerMessage = "Server Full" };

            var player = new Player(playerName);
            Players.Add(player);

            if (Players.Count == RequiredPlayers)
            {
                _gameState = GameState.InProgress;
                ResetScenario();
            }

            return new RequestJoinModel
                {
                    PlayerValid = true,
                    PlayerNumber = Players.Count,
                    PlayerMessage = "Welcome"
                };
        }

        public RequestUpdateClientModel RequestUpdateClient(int id)
        {
            if (!IsValidPlayerId(id))
                return RequestUpdateClientModel.CreateInvalid();

            var player = Players[id - 1];

            player.LastRequest = DateTime.Now;

            if (_gameState != GameState.InProgress)
                return RequestUpdateClientModel.CreateWaiting();

            if (!player.IsAlive || player.HasMadeChoice)
                return RequestUpdateClientModel.CreateWaiting();

            return new RequestUpdateClientModel(CurrentScenario, player);
        }

        private bool IsValidPlayerId(int id)
        {
            return (id >= 1) && (id <= Players.Count);
        }

        public void MakeChoice(int playerNumber, int choiceNumber)
        {
            if (_gameState != GameState.InProgress)
                return;

            if (!IsValidPlayerId(playerNumber))
                return;

            var player = Players[playerNumber - 1];
            player.LastRequest = DateTime.Now;

            if ((choiceNumber < 0) || (choiceNumber > 3))
                return;

            if (player.HasMadeChoice)
                return;

            // dead players don't make choices
            if (!player.IsAlive)
                return;

            // player choice locked in
            player.SelectedChoice = (Choice)choiceNumber;

            CheckChoices();
        }

        private void CheckChoices()
        {
            if (Players.Any(p => p.IsAlive && !p.HasMadeChoice))
                return;

            var outcome = CurrentScenario.Outcomes.FirstOrDefault(o => o.IsActive(Players));
            if (outcome == null)
            {
                // failsafe in case none of the outcomes are active - just use the last one
                outcome = CurrentScenario.Outcomes.LastOrDefault();
            }
            if (outcome == null)
            {
                UpdateText = "GROUNDHOG DAY!!";
                ClearChoices();
                return;
            }
            UpdateText = outcome.ActionOutcomeAndGetDisplayText(Players);
            if (Players.Any(p => p.IsAlive))
            {
                CurrentScenarioKey = outcome.NextScenarioKey;
            }
            else
            {
                // all players are dead
                CurrentScenarioKey = "";
            }
            ClearChoices();

            if ((CurrentScenarioKey == "") || CurrentScenario.IsGameOver)
                EndGame();
        }

        private void ClearChoices()
        {
            foreach (var player in Players)
                player.SelectedChoice = Choice.None;
        }

        public RequestUpdateDisplayModel RequestUpdateDisplay()
        {
            return new RequestUpdateDisplayModel(this);
        }

        public void EndGame()
        {
            if (_gameState == GameState.GameOver)
                return;

            _gameState = GameState.GameOver;
            _resetGameAt = DateTime.Now.AddSeconds(GameOverTimeout);
        }

        public static List<Player> DamagePlayers(IList<Player> players, Func<Player, bool> isDamaged, int damage = 1)
        {
            var damagedPlayers = new List<Player>();
            foreach (var player in players.Where(p => p.IsAlive))
            {
                if (isDamaged(player))
                {
                    player.HitPoints -= damage;
                    damagedPlayers.Add(player);
                }
            }
            return damagedPlayers;
        }

        public static string GetPlayerNames(IEnumerable<Player> players)
        {
            return string.Join(" & ", players.Select(p => p.Name));
        }

        public static Player GetRandomPlayer(IEnumerable<Player> players)
        {
            return GetRandomPlayer(players.ToList());
        }

        public static Player GetRandomPlayer(IList<Player> players)
        {
            var random = new Random();
            var index = random.Next(players.Count);
            return players[index];
        }
    }
}

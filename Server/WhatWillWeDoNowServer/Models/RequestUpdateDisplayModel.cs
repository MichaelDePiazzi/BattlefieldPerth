using System.Collections.Generic;
using System.Linq;
using WhatWillWeDoNowServer.GameState;

namespace WhatWillWeDoNowServer.Models
{
    public class RequestUpdateDisplayModel
    {
        public int Countdown { get; set; }
        public int ImageIndex { get; set; }
        public string Title { get; set; }
        public string TextUpdate { get; set; }
        public string TextNewScenario { get; set; }
        public IEnumerable<string> PlayerNames { get; set; }
        public IEnumerable<int> CharacterHp { get; set; }
        public IEnumerable<bool> CharacterResponded { get; set; }

        public RequestUpdateDisplayModel()
        {
        }

        public RequestUpdateDisplayModel(GameStateManager gameStateManager)
        {
            Countdown = 60;//TODO
            var scenario = gameStateManager.CurrentScenario;
            if (scenario != null)
            {
                ImageIndex = scenario.ImageIndex;
                Title = scenario.Title;
                TextNewScenario = scenario.Text;
            }
            TextUpdate = gameStateManager.UpdateText;
            PlayerNames = gameStateManager.Players.Select(p => p.Name);
            CharacterHp = gameStateManager.Players.Select(p => p.HitPoints);
            CharacterResponded = gameStateManager.Players.Select(p => p.HasMadeChoice);
        }
    }
}

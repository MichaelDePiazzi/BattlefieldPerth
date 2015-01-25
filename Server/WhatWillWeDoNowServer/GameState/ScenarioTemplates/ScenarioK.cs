using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioK : Scenario
    {
        public ScenarioK()
        {
            Id = "K";
            Title = "Maylands";
            ImageIndex = (int)GameState.ImageIndex.ScenarioK;
            Text = "The Maylands café strip lies abandoned, apart from dead bodies lying in the street. Small groups of aliens walk down the roads, carelessly someone steps out into view and the aliens shout out to each other. More groups come into view, drawn here by the shouting. You're heavily outnumbered and outgunned.";
            Choices = new[]
                {
                    "Fight a last stand",
                    "Flee on foot",
                    "Look for a vehicle",
                    "Surrender"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4()
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => p.SelectedChoice == Choice.A) >= players.Count(p => p.SelectedChoice == Choice.B) + players.Count(p => p.SelectedChoice == Choice.C) + players.Count(p => p.SelectedChoice == Choice.D)),
                    ActionOutcomeAndGetDisplayText = players =>
                    {
                        for (int i = 0; i < 3; i++)
                            GameStateManager.DamagePlayers(players, player => true);

                        return "It's time, this is now the final stand. You prepare what you have, and hunker down. The first pack of guards walk in, and you ambush them, bludgeoning them to death, and taking their weapons.  You hold out for some time, using the alien weapons against them. The flechettes they fire burst inside the creatures, dropping them quickly. After an a long fight, they stop charging the café you have fortified. You risk a look out the window, and five floating missiles fly towards the building, they smash through the windows and explode in the room, filling it with acidic gas. Your skin strips from your flesh, and your flesh melts from your bones.";
                    },
                    NextScenarioKey = "X"
                };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > players.Count(p => p.SelectedChoice == Choice.A) + players.Count(p => p.SelectedChoice == Choice.C)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players, player => player.SelectedChoice == Choice.A ||  player.SelectedChoice == Choice.D);

                    string extraText = "";
                    // if players were damaged, add more text
                    if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) + " runs through a cloud of acid and are burnt badly. ";

                    return "Running together you all head the only way you can, toward the Maylands golf course. Floating seeker missiles fly through the air and burst into clouds of acid in front of you. " +
                        extraText + "You arrive at the golf course, and take refuge in the club house.";
                },
                NextScenarioKey = "N"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > players.Count(p => p.SelectedChoice == Choice.B) + players.Count(p => p.SelectedChoice == Choice.A)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players, player => player.SelectedChoice == Choice.A || player.SelectedChoice == Choice.D);

                    string extraText = "";
                    // if players were damaged, add more text
                    if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) + " breath in the gas, and start coughing blood. ";

                    return "Frantically searching, you find a garbage truck that is still in running condition. Packing everyone into the cab, you fire it up and start making a break for it. Crushing a pack a guards as your round a corner, they signal just before they die, and 3 seeker missiles float into view. They fly in front of the truck and explode into clouds of acid. " +
                        extraText + "You keep driving, the truck barely holding  together with the acid damage. You arrive in Vic Park, as the engine shudders to a halt.";
                },
                NextScenarioKey = "O"
            };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.B) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) == 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    for (int i = 0; i < 3; i++ )
                        GameStateManager.DamagePlayers(players, player => true);

                    return "You give up, you all walk into the street with your hands in the air. The closest alien looks towards you, confused at the concept. Before you can react, he levels his weapon, and sprays flechettes into all of your bodies. The pain as they bore deep into your body is immense, but thankfully short, as they explode and tear your bodies to pieces.";
                },
                NextScenarioKey = "X"
            };
        }
    }
}

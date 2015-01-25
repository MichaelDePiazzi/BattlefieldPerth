using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioI : Scenario
    {
        public ScenarioI()
        {
            Id = "I";
            Title = "WACA";
            ImageIndex = (int)GameState.ImageIndex.ScenarioH;
            Text = "Walking through the streets of East Perth, you can hear a loud humming from somewhere unseen. As you approach the noise, the smell of ozone and burning metal fills the air. Rounding the corner, a massive triangular sculpture rises from a roundabout, filled with lightning, arcing off whenever anything gets close.";
            Choices = new[]
                {
                    "Try to disable the craft",
                    "Look for a car",
                    "Try to save the people",
                    "Pretend you see nothing and leave"
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
                        (players.Count(p => p.SelectedChoice == Choice.A) >= (players.Count(p => p.SelectedChoice == Choice.B) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                    ActionOutcomeAndGetDisplayText = players =>
                    {
                        
                        return "You look for anything you could use dent the craft, but your search is fruitless. As you prepare to give up and turn to leave, you find a badly wounded soldier hiding behind some chairs. He can barely speak but he points to his grenades, and those on the other soldiers corpses nearby. You collect as many as you can, and hand them to the dying man. He crawls into the open, and a tentacle from the ship lashes out and grabs him. He pulls the pins on all of the explosives as he is dragged towards the maw. Seconds after he is consumed, an explosion from inside the craft can be heard. Green blood flows from the orifices on the ship, as it crashes to the ground. During the confusion, you circle round the field, and cross the causeway to Burswood.";
                    },
                    NextScenarioKey = "L"
                };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > (players.Count(p => p.SelectedChoice == Choice.A) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => player.SelectedChoice == Choice.A );

                    string extraText = "";
                    // if players were damaged, add more text
                    if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) ;

                    return "Checking the large car park, you look for any vehicle that is in working condition. Most are damaged, but you find two small cars that will still run. You clamber into them and begin driving away. As you are driving to the north, a small missile launches into the air and begins tracking one of the two cars. You floor the gas and drive to avoid the missile. The weapon flies over the top of the car and explodes in a shower of acid. " + extraText + " are in the car as it swerves across the road, everyone inside is battered and bruised. You continue driving towards Maylands. ";
                },
                NextScenarioKey = "K"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > (players.Count(p => p.SelectedChoice == Choice.A) + (players.Count(p => p.SelectedChoice == Choice.B)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    GameStateManager.DamagePlayers(players, player => true, 2);
                    return "Bravado is the name of the game today, finding where the aliens are dragging the prisoners from, you prepare to launch a foolish, but brave rescue attempt. Hiding behind some seats, you surround the  guards and jump out at the same time, leaping at them with any weapons you can find. These guards seem to be larger and stronger, with better equipment than any other you have encountered yet. The fight is brutal, and you are overpowered easily. Anyone who survives turns and runs, and the guards don't give chase. You slowly walk over the causeway, towards Burswood casino.";
                },
                NextScenarioKey = "L"
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

                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => p.IsAlive));
                    damagedPlayer.HitPoints -= 1;

                    return "Looking around, no one wants to intercede, as the position is well fortified, and looks hopeless. As a group, you turn and walk away from the oval. Silence hangs heavy over the whole group, as everything feels hopeless." + damagedPlayer.Name + " Punches a tree in frustration, as anger and rage overtake them. The tree is stronger than expected, and the punch was backed with emotion, breaking their hand. Quietly you continue towards Maylands. ";
                },
                NextScenarioKey = "K"
            };
        }

    }
}

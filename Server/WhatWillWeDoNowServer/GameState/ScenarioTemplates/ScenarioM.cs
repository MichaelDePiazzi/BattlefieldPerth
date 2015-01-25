using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioM : Scenario
    {
        public ScenarioM()
        {
            Id = "M";
            Title = "Enex 100";
            ImageIndex = (int)GameState.ImageIndex.ScenarioM;
            Text = "Entry into the building is easy, as the doors have been smashed completely. People lie dead in the halls but there is no activity you can see. The building makes a small amount of noise, it seems as if the generators are still running. Lights are on and the elevators are working.";
            Choices = new[]
                {
                    "Try and go up to the offices",
                    "Leave the building to the south",
                    "Head to the Parking Bay",
                    "Loot the building"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4(),
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
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => true);

                    return "You need a security card to get into the lifts, looking around you find the corpse of a guard, with his card in hand. You take the card, and try it. It grants access to all the floors, so you take a stab and go to the top level. No one is around, and there is food aplenty in the supermarket downstairs and this place is about as secure as you can get in the city. You hunker down, and convert some offices into sleeping quarters. Let's see how long we can survive.";
                },
                NextScenarioKey = "S"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                        (players.Count(p => p.SelectedChoice == Choice.A) >= (players.Count(p => p.SelectedChoice == Choice.B) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => p.IsAlive));
                        damagedPlayer.HitPoints -= 1;
                    return "You walk through the building, and head onto St Georges Terrace. As you cross the street heading for the esplanade. A loud hissing noise fills the area, scanning the skies a swarm of flying insects fills the sky from the East, then swarm down the terrace. They fly into you tearing through your bodies, as you fall to the ground, they swarm all over you, and feast on your flesh.";
                },
                NextScenarioKey = "X"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                         (players.Count(p => p.SelectedChoice == Choice.A) >= (players.Count(p => p.SelectedChoice == Choice.B) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.B) || (player.SelectedChoice == Choice.C));
                    return "You head into the parking bay, and the cars are all untouched. Sports cars, Humvees and other vehicles. Siphoning all the fuel you can, you all bundle into a car each, and head down the Freeway as fast as you can. Down south seems like a nice a place as anywhere to spend your…. Final Days.";
                },
                NextScenarioKey = "T"
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
                    return "Looking round, electronics and food are there for the taking, you all fill your bags, and head to the South Entrance, onto St. Georges Terrace.  As you cross the street heading for the esplanade. A loud hissing noise fills the area, scanning the skies a swarm of flying insects fills the sky from the East, then swarm down the terrace. They fly into you tearing through your bodies, as you fall to the ground, they swarm all over you, and feast on your flesh.";
                },
                NextScenarioKey = "X"
            };
        }

    }
}

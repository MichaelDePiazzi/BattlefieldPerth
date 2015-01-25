using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioC : Scenario
    {
        public ScenarioC()
        {
            Id = "C";
            Title = "Esplanade";
            ImageIndex = (int)GameState.ImageIndex.ScenarioC;
            Text = "The Swan river is stained with oils and other fluids not seen on this planet before, bloated alien corpses float down the river. As you walk past Elizabeth Quay, you see a military vehicle damaged beyond repair. Inside is weapons and equipment, left on the bodies of soldiers that never had a chance to use them. While looking at the wreck, A screech breaks the noise from the southern bank of the river, a flock of giant bat like creatures  begin pouring out of the apartment blocks on the South side of the river. They circle in the sky for a few moments, before wheeling around, and start swooping towards you.";
            Choices = new[]
                {
                    "Use Military Equipment",
                    "Hide in The Train Station",
                    "Evade Them in the Gardens",
                    "Protect Yourself"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4(),
                    CreateOutcome5(),
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) > 3),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "You feel something well up from inside you, is it bravery, or is it bravado. Either way you all take the equipment from the back of the vehicle and hand it round. The battle is heated, but somehow you come out the other end unscathed. Unfortunately, all of the ammo has been used, and the weapons are no longer any use. You discard them by the side of the road, and head towards East Perth.";
                },
                NextScenarioKey = "F"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) > 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.B) < 4) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) > 4),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice != Choice.D));
                    return "Chaos and confusion takes the group, as some try to flee to the station, some race for the gardens and some run for the guns. Shots get fired and over the screams and shouts, and eventually the creatures are defeated. Everyone collects themselves and assesses their wounds. " +
                           GameStateManager.GetPlayerNames(damagedPlayers) +
                           " have been injured, You then turn and head towards east Perth.";
                },
                NextScenarioKey = "F"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > 3),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.C));
                    return "You run as a group into the train station, ducking and dodging out of the way of the swooping bats. Anyone who ran for the Gardens, turns to follow the group and gets swooped by the bat like creatures. Eventually you all are able to gather and hide in the railway tunnels, you slowly work your way along the tunnels and follow the tracks, and end up at the East Perth Train station the train line opens out, and is no longer good cover, so you turn and head towards North Perth.";
                },
                NextScenarioKey = "E"
            };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > 3),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.A) || (player.SelectedChoice == Choice.B));
                    return "The group heads for the Gardens, trying to hide from the flying creatures. The large trees provide coverage and make it hard for the bats to attack anyone hiding there. Any stragglers are swooped as they head for the cover of the trees. Eventually the bats decide to move on to easier quarry and fly to the West. Collecting yourselves, you head towards East Perth.";
                },
                NextScenarioKey = "F"
            };
        }

        private static Outcome CreateOutcome5()
        {
            return new Outcome
            {
                IsActive = players => true,
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => p.IsAlive));
                    damagedPlayer.HitPoints -= 3;
                    return "Chaos and confusion as everyone panics and tries to protect themselves. The bats almost cackle as they swoop at the confused group over and over again. Eventually you run to the train station, and dive down the stairs. The bats fly away with blood in their mouths. " +
                           damagedPlayer.Name +
                           " took the worst of the blows and collapses, dead. You slowly work your way along the tunnels and follow the tracks, and end up at the East Perth Train station the train line opens out, and is no longer good cover, so you turn and head towards North Perth.";
                },
                NextScenarioKey = "F"
            };
        }
    }
}

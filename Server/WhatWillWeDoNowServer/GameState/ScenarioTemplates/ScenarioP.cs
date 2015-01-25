using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioP : Scenario
    {
        public ScenarioP()
        {
            Id = "P";
            Title = "St Georges Terrace";
            ImageIndex = (int)GameState.ImageIndex.ScenarioP;
            Text = "The green cactus art lies in front of you as you approach Forrest Chase. It looks like it now fits in amongst the alien structures that now fill the outdoor area. As you walk through the structures you see entombed humans inside the new structures, and small insectiod larvae slowly consuming them for food. A pack of alien guards walk through the chase. A CAT bus sits in the road, which looks like it could still be in working condition.";
            Choices = new[]
                {
                    "Make for The Bus",
                    "Sneak Into Myer",
                    "Attack the Aliens",
                    "Hide from the Danger"
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
                    (players.Count(p => p.SelectedChoice == Choice.C) > 1) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) < 4),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => true);

                    return "The bravest of you collect what you can, and step behind the green cactus. Waiting for the patrol to move into a position to launch and ambush on them. The fight is a struggle but eventually you overcome them. The whole group has injuries, as they head into Myer.";
                },
                NextScenarioKey = "S"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > 4) ,
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => p.IsAlive));
                        damagedPlayer.HitPoints -= 1;
                    return "The group as a whole get ready to jump the patrol of the three alien guards. You leap from behind the cactus and knock them all to the ground. Digging into the savage nature of the human condition, you mercilessly attack the prone aliens until their bodies lie twitching. One of you has a minor injury sustained in the melee. You wipe the ichor off your hands and walk into Myer.";
                },
                NextScenarioKey = "S"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) > (players.Count(p => p.SelectedChoice == Choice.B)) + (players.Count(p => p.SelectedChoice == Choice.C))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.B) || (player.SelectedChoice == Choice.C));
                    return "You run into the bus, and find the keys in the ignition, turning them slowly, the whole machine shudders to life. The noise of the bus starting up alerts the alien Guards, and they turn and fire their flechette weapons at the last of you getting onto the bus. The driver of the bus drops the pedal to the floor, and breaks through the rubble on the road, and heads towards Mount Lawley.";
                },
                NextScenarioKey = "T"
            };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.A) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) == 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "Everyone keeps low and sneaks into Myer. The aliens don't notice your presence and you enter into the store without any incident.";
                },
                NextScenarioKey = "S"
            };
        }
    }
}

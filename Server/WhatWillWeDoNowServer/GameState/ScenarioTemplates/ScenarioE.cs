using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioE : Scenario
    {
        public ScenarioE()
        {
            Id = "E";
            Title = "North Perth";
            ImageIndex = (int)GameState.ImageIndex.ScenarioE;
            Text = "Sneaking into the back of Hyde park, your group races across the open terrain trying to find cover. Ducking into the trees near the lake to catch your breath you scan the area for any threats. Just when you exhale thinking you have some time to catch your thoughts, the water begins to bubble and hiss, as a gargantuan tentacle bursts out of the water and races towards the group.";
            Choices = new[]
                {
                    "Scatter and avoid it",
                    "Try and overpower it",
                    "Head back to the street",
                    "Use someone else as bait"
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
                    (players.Count(p => p.SelectedChoice == Choice.A) > (players.Count(p => p.SelectedChoice == Choice.B)) + (players.Count(p => p.SelectedChoice == Choice.C))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.B));

                    return "The team scatters and flees around the lake, dodging the tentacle as it swings at you. Anyone trying to fight gets knocked away, but is able to stand and run with the rest of the team away towards Mount Lawley.";
                },
                NextScenarioKey = "H"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) >= (players.Count(p => p.SelectedChoice == Choice.A)) + (players.Count(p => p.SelectedChoice == Choice.C))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var count =players.Count(p => p.SelectedChoice == Choice.B);
                    for (int i = count; i < 3;i++)
                    {GameStateManager.DamagePlayers(players, player => true);}
                    
                    
                    return "The group dives onto the tentacle and attacks it with anything they have at hand, rocks, sticks and knives smash into the appendage until it retreats under the water. Everyone takes stock of their injuries, and staggers towards Mount Lawley.";
                },
                NextScenarioKey = "H"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > (players.Count(p => p.SelectedChoice == Choice.A)) + (players.Count(p => p.SelectedChoice == Choice.B))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.B));
                    return "The group turns and runs, anyone who hesitates gets smacked in the face by the tentacle and falls to the ground. Running back to the roads, someone spots a four wheel drive, that looks like it will be driveable. You all bundle into the car and drive off towards the WACA.";
                },
                NextScenarioKey = "I"
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
                        damagedPlayer.HitPoints -= 3;

                    return "Everyone panics, and pushes each person forward into the path of the tentacle. The tentacle wraps around (Random Player), and they are pulled under water never to seen again. The rest of the group runs back to the roads, someone spots a four wheel drive, that looks like it will be driveable. You all bundle into the car and drive off towards the WACA.";
                },
                NextScenarioKey = "I"
            };
        }

       
   
    }
}

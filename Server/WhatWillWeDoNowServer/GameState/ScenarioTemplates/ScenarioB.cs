using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioB : Scenario
    {
        public ScenarioB()
        {
            Id = "B";
            Title = "Northbridge";
            ImageIndex = (int)GameState.ImageIndex.ScenarioB;
            Text = "The once busy streets lie bare, human and alien corpses lie in the street, the aliens lie twitching long after their death. An absurdly small roundabout with a bizarre black and white spike sits in the centre of the intersection of two streets. Abandoned shops and cafes line the sides of every street, and a small public park is empty. As you walk down the street a gruff voice shouts 'FREEZE!' from inside a building. As you stop to look where the voice came from, three more people emerge from their hiding places and move to surround you.";
            Choices = new[]
                {
                    "Try to talk",
                    "Try to fight",
                    "Try to run",
                    "Try to hide"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4(),
                    CreateOutcome5(),
                    CreateOutcome6(),
                    CreateOutcome7(),
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => p.SelectedChoice == Choice.C) > 4),
                    ActionOutcomeAndGetDisplayText = players =>
                    {
                        var count = GameStateManager.DamagePlayers(players,
                            player => (player.SelectedChoice == Choice.B))
                            .Count;
                        return "As most of you scatter, " + count +
                               " of you try to rush the haggard looking humans. Realising you are outnumbered, they turn and join the people fleeing. Rifle shots ring out and hit the stragglers. After a few moments, you think you have lost them, and head towards North Perth.";
                    },
                    NextScenarioKey = "E"
                };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > 4),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "You lower your heads and rush the haggard looking humans. The fight is quick, as they are hungry and weak. You turn towards the city and head for Forrest Chase";
                },
                NextScenarioKey = "D"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => player.SelectedChoice != Choice.D);
                    return "You lower your heads and rush the haggard looking humans. The fight is quick, as they are hungry and weak, but " +
                           GameStateManager.GetPlayerNames(damagedPlayers) +
                           " get injured in the scuffle. You turn towards the city and head for Forrest Chase";
                },
                NextScenarioKey = "D"
            };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.A) > 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "As the standoff hangs tense in the air, " +
                           players.First(p => p.SelectedChoice == Choice.A).Name +
                           " calls out, \"We have nothing, and we don't want to hurt you\". The small gang looks over you all, and the voice shouts again \"They are just as screwed as we are, you better get out of here before we change our minds\" Without turning back you head for Forest Chase";
                },
                NextScenarioKey = "D"
            };
        }

        private static Outcome CreateOutcome5()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.B) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) == 1),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.DamagePlayers(players,
                        player => player.SelectedChoice == Choice.C)
                        .First();
                    return damagedPlayer.Name +
                           " is spooked by the threat, and starts running, as everyone else tries to hide. A rifle shot rings out, and clips " +
                           damagedPlayer.Name +
                           ". As the gang looks around in confusion, you all take off after " +
                           damagedPlayer.Name +
                           ", and head into North Perth.";
                },
                NextScenarioKey = "E"
            };
        }

        private static Outcome CreateOutcome6()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.B) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) > 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var affectedPlayers = players.Where(p => p.SelectedChoice == Choice.C).ToList();
                    var damagedPlayer = affectedPlayers.First();
                    damagedPlayer.HitPoints--;
                    var affectedPlayerNames = GameStateManager.GetPlayerNames(affectedPlayers);
                    return affectedPlayerNames +
                           " are spooked by the surprise threat, and start to sprint away from the square. A rifle shot rings out, and clips " +
                           damagedPlayer.Name +
                           ". A cuss is heard from a balcony, \"Damn it!, jammed!\". Seizing on the opportunity, the rest of the group runs after " +
                           affectedPlayerNames +
                           ", into North Perth.";
                },
                NextScenarioKey = "E"
            };
        }

        private static Outcome CreateOutcome7()
        {
            return new Outcome
            {
                IsActive = players => true,
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "You all scatter and hide where you can, under cars in inside the bars and cafes. After a few moments, that seem like lifetimes, the small gang gives up on the search, and walks away to the North. You crawl out of your hiding places and head in the opposite direction into Forest Chase.";
                },
                NextScenarioKey = "D"
            };
        }
    }
}

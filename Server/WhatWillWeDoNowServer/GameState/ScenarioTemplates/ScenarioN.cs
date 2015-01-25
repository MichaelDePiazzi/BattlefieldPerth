using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioN : Scenario
    {
        public ScenarioN()
        {
            Id = "N";
            Title = "Maylands Golf Course";
            ImageIndex = (int)GameState.ImageIndex.ScenarioN;
            Text = "Racing to the country club you hide inside looking for a place to fortify. The Aliens are closing in your position, and preparing to attack. The Aliens split into small squads, and begin closing in on the country club.";
            Choices = new[]
                {
                    "Fight with anything you can",
                    "Look for an escape outside",
                    "Search the club for a way out",
                    "Sit at the Bar"
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
                    (players.Count(p => p.SelectedChoice == Choice.A) >= players.Count(p => p.SelectedChoice == Choice.B) + players.Count(p => p.SelectedChoice == Choice.C)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "You look through the club rooms, and all you can find to defend yourself are golf clubs. Using the pool chalk from the bar, you paint your faces with blue war paint. You shout out loud as you rush them all with your improvised weapons, shouting something about lives and freedom. You are killed quickly afterwards, as your bodies are riddled with flechettes.";
                },
                NextScenarioKey = "X"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) >= players.Count(p => p.SelectedChoice == Choice.A) + players.Count(p => p.SelectedChoice == Choice.C)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "Racing outside, you see a large cruiser boat floating the river. You all race to it and dive aboard. Starting the engine, you send the boat up stream. Heading up the Swan River into the valley, you think you can find enough food to eat, places to hide and weapons to fight back.";
                },
                NextScenarioKey = "U"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) >= players.Count(p => p.SelectedChoice == Choice.A) + players.Count(p => p.SelectedChoice == Choice.B)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "You search through the locker rooms, and find many keys left in the lockers you bust open.  You head to the private car park and find sports cars, Humvees and other vehicles. Siphoning all the fuel you can, you all bundle into a car each, and head down the Freeway as fast as you can. Down south seems like a nice a place as anywhere to spend your…. Final Days.";
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
                    return "Sitting down and resigning to your fate, you pour out some drinks for anyone left alive. You all knock back the scotch, and start a game of pool, waiting for the invaders to come and find you.  A loud hissing noise fills the area, scanning the skies a swarm of flying insects fills the sky from the South, they swarm into the building hunting down anything they can. They fly into you tearing through your bodies, as you fall to the ground, they swarm all over you, and feast on your flesh.";
                },
                NextScenarioKey = "X"
            };
        }
    }
}

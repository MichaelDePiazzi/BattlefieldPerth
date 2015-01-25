using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioO : Scenario
    {
        public ScenarioO()
        {
            Id = "O";
            Title = "Maylands Golf Course";
            ImageIndex = (int)GameState.ImageIndex.ScenarioO;
            Text = "Walking down the café strip, the aliens seem to have hunted you down. The Aliens are closing in your position, and preparing to attack. The Aliens split into small squads, and begin closing in on the Balmoral, as you hide in the old pub.";
            Choices = new[]
                {
                    "Fight Back",
                    "Head further East",
                    "Head to Tonkin Highway",
                    "Drink at the Bar"
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
                    return "You look through the Bar rooms, and all you can find to defend yourself are pool cues. Using the pool chalk from the bar, you paint your faces with blue war paint. You shout out loud as you rush them all with your improvised weapons, shouting something about lives and freedom. You are killed quickly afterwards, as your bodies are riddled with flechettes.";
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
                    return "You continue East, fleeing from the invasion, You eventually come to the freight line, just as a diesel train is taking off from the station. Racing after it you climb aboard and wave to the driver. Breathing out, you think, Kalgoorlie might a bit safer, the Aliens seem to like the humidity.";
                },
                NextScenarioKey = "V"
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
                    return "Grabbing a ute from the back of the pub, you all pile into the back, and head up Tonkin towards the Swan Valley. You think you can find enough food to eat, places to hide and enough weapons to fight back.";
                },
                NextScenarioKey = "U"
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
                    return "Sitting down and resigning to your fate, you pour out some drinks for anyone left alive. You all knock back the scotch, and stat a game of pool, waiting for the invaders to come and find you.  A loud hissing noise fills the area, scanning the skies a swarm of flying insects fills the sky from the South, they swarm into the building hunting down anything they can. They fly into you tearing through your bodies, as you fall to the ground, they swarm all over you, and feast on your flesh.";
                },
                NextScenarioKey = "X"
            };
        }
    }
}

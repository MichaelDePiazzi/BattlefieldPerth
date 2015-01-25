using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioJ : Scenario
    {
        public ScenarioJ()
        {
            Id = "J";
            Title = "Carillon";
            ImageIndex = (int)GameState.ImageIndex.ScenarioJ;
            Text = "Approaching the arcade that runs between the malls, you head into the buildings before you are spotted by anything else. The lights in the mall flicker and make the whole area feel claustrophobic. You walk through the stores and everything seems quiet, possibly too quiet.";
            Choices = new[]
                {
                    "Walk carefully though",
                    "Rush through as fast as you can",
                    "Don't enter the mall",
                    "Face your fear and walk through"
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
                        return "You walk through the front of the mall, and slowly creep towards the other end. Jumping at shadows and flickering lights. The oppressive atmosphere makes you all sweat in terror, and the slightest noise makes you flinch. Surprisingly you make your way through the mall unscathed, except the bruises on your fragile psyche. You look up in front of you at Enex 100.";
                    },
                    NextScenarioKey = "M"
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
                    return "Preparing yourselves you break into a sprint, pushing through the fear and running through the mall, you race past shops and up and down stalled escalators. The run takes less than a minute, but feels like an hour. Sweat from sprinting and sweat from nerves drip from your pores. You race out the other side, and stop to catch your breath in front of Enex 100.";
                },
                NextScenarioKey = "M"
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
                    GameStateManager.DamagePlayers(players, player => true);
                    return "The fear is too much for you, and you turn away and head away from the plaza, and up the mall. Keeping to the sides, and as hidden as possible. While working towards the east, a hissing noise breaks the silence. A device flies out from the inside of a planter, and explodes with a concussive blast, knocking everyone into the walls. Shouts from alien guards can be heard from the direction you came from, you dust yourselves off and run from the noise. You duck into an underground car park. Before you are three motor bikes, in working condition. You saddle up, and race out, heading through Maylands, and towards the golf course.";
                },
                NextScenarioKey = "N"
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
                    return "Stepping up, you walk with your head held high, maybe you have accepted your fate, maybe you just don't care anymore. Fortunately, no aliens have taken up position in here, and you walk through without incident. You stand in front of Enex 100, and thank your stars, you don't think your luck will hold out like that again.";
                },
                NextScenarioKey = "M"
            };
        }
    }
}

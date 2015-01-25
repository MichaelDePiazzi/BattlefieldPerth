using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioT : Scenario
    {
        public ScenarioT()
        {
            Id = "T";
            Title = "Winning Like a Mini Boss";
            ImageIndex = (int)GameState.ImageIndex.ScenarioB;
            Text = "You sneeze and the aliens die. The end. Yes this was an old radio thing.";
            Choices = new[]
                {
                    "win",
                    "win",
                    "win",
                    "sleep"
                };
            Outcomes = new[]
                {
                    CreateOutcome1()
  
                };
        }

        // TO DO: initiate reset
        private static Outcome CreateOutcome1()
        {
            return new Outcome
                {
                    IsActive = players => true,
                    ActionOutcomeAndGetDisplayText = players =>
                    {
                        return "End";
                    },
                    NextScenarioKey = ""
                };
        }

     
   

    }
}

using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioU : Scenario
    {
        public ScenarioU()
        {
            Id = "U";
            Title = "You messed up bro. YOLO";
            ImageIndex = (int)GameState.ImageIndex.ScenarioB;
            Text = "Well that didn't work. Maybe next time. Pity the fool.";
            Choices = new[]
                {
                    "derp",
                    "derp",
                    "derp",
                    "derp"
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
                    
                };
        }

     
   

    }
}

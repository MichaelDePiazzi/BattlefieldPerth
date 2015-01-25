using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioS : Scenario
    {
        public ScenarioS()
        {
            Id = "S";
            Title = "Winning Like a Boss";
            ImageIndex = (int)GameState.ImageIndex.ScenarioS;
            Text = "You find some government issue steroids inject them and have the strength to rip a lazer of an alien craft. Using this lazer you take down their entire fleet.";
            Choices = new[]
                {
                    "WIN",
                    "WIN",
                    "WIN",
                    "WIN"
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

using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioV : Scenario
    {
        public ScenarioV()
        {
            Id = "V";
            IsGameOver = true;
            Title = "Well you made it to the end so at least you weren't a failure.";
            ImageIndex = (int)GameState.ImageIndex.ScenarioB;
            Text = "You suck give up at trying.";
            Choices = new[]
                {
                    "give up now",
                    "give up now",
                    "give up now",
                    "give up now"
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

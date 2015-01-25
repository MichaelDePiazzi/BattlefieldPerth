namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioT : Scenario
    {
        public ScenarioT()
        {
            Id = "T";
            IsGameOver = true;
            Title = "Margaret River";
            ImageIndex = (int)GameState.ImageIndex.None;
            Text = "";
            Choices = new[]
                {
                    "You",
                    "Have",
                    "Survived",
                    "Today"
                };
            Outcomes = new[]
                {
                    CreateOutcome1()
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome();
        }
    }
}

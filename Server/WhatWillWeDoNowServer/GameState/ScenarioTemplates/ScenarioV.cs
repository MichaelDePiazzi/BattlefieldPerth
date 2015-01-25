namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioV : Scenario
    {
        public ScenarioV()
        {
            Id = "V";
            IsGameOver = true;
            Title = "Kalgoorlie";
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

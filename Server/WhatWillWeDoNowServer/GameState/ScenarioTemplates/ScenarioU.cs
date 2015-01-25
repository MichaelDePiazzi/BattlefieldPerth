namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioU : Scenario
    {
        public ScenarioU()
        {
            Id = "U";
            IsGameOver = true;
            Title = "Swan Valley";
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

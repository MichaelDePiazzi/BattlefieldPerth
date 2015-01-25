namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioS : Scenario
    {
        public ScenarioS()
        {
            Id = "S";
            IsGameOver = true;
            Title = "Fortified in the City";
            ImageIndex = (int)GameState.ImageIndex.ScenarioM;
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

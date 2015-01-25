namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioX : Scenario
    {
        public ScenarioX()
        {
            Id = "X";
            IsGameOver = true;
            Title = "";
            ImageIndex = (int)GameState.ImageIndex.ScenarioX;
            Text = "";
            Choices = new[]
                {
                    "",
                    "",
                    "",
                    ""
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
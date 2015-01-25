using System.Collections.Generic;

namespace WhatWillWeDoNowServer.GameState
{
    public class Scenario
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int ImageIndex { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Choices { get; set; }
        public IEnumerable<Outcome> Outcomes { get; set; }
        public bool IsGameOver { get; set; }
    }
}
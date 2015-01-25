using System;
using System.Collections.Generic;

namespace WhatWillWeDoNowServer.GameState
{
    public class Outcome
    {
        public Func<IList<Player>, bool> IsActive { get; set; }
        public Func<IList<Player>, string> ActionOutcomeAndGetDisplayText { get; set; } 
        public string NextScenarioKey { get; set; }
    }
}
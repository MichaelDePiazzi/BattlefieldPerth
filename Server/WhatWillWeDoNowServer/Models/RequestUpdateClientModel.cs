using System.Collections.Generic;
using WhatWillWeDoNowServer.GameState;

namespace WhatWillWeDoNowServer.Models
{
    public class RequestUpdateClientModel
    {
        public string Title { get; set; }
        public IEnumerable<string> Choices { get; set; }
        public bool IsWaiting { get; set; }
        public string ScenarioId { get; set; }
        public bool IsAlive { get; set; }
        public bool IsReset { get; set; }   // Effectively means player is invalid

        private RequestUpdateClientModel()
        {
        }

        public RequestUpdateClientModel(Scenario scenario, Player player)
        {
            Title = scenario.Title;
            Choices = scenario.Choices;
            ScenarioId = scenario.Id;
            IsAlive = player.IsAlive;
        }

        public static RequestUpdateClientModel CreateWaiting()
        {
            return new RequestUpdateClientModel { IsWaiting = true };
        }

        public static RequestUpdateClientModel CreateInvalid()
        {
            return new RequestUpdateClientModel { IsReset = true };
        }
    }
}

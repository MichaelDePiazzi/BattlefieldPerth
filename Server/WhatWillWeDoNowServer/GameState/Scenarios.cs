using System.Collections.Generic;
using WhatWillWeDoNowServer.GameState.ScenarioTemplates;

namespace WhatWillWeDoNowServer.GameState
{
    class Scenarios
    {
        public static Dictionary<string, Scenario> CreateScenarios()
        {
            var dictionary = new Dictionary<string, Scenario>();
            AddScenario(dictionary, new ScenarioA());
            AddScenario(dictionary, new ScenarioB());
            AddScenario(dictionary, new ScenarioC());
            AddScenario(dictionary, new ScenarioD());
            AddScenario(dictionary, new ScenarioE());
            AddScenario(dictionary, new ScenarioF());
            AddScenario(dictionary, new ScenarioG());
            AddScenario(dictionary, new ScenarioH());
            AddScenario(dictionary, new ScenarioI());
            AddScenario(dictionary, new ScenarioJ());
            AddScenario(dictionary, new ScenarioK());
            AddScenario(dictionary, new ScenarioL());
            AddScenario(dictionary, new ScenarioM());
            AddScenario(dictionary, new ScenarioN());
            AddScenario(dictionary, new ScenarioO());
            AddScenario(dictionary, new ScenarioP());
            AddScenario(dictionary, new ScenarioQ());
            AddScenario(dictionary, new ScenarioR());

            // end game
            AddScenario(dictionary, new ScenarioS());
            AddScenario(dictionary, new ScenarioT());
            AddScenario(dictionary, new ScenarioU());
            AddScenario(dictionary, new ScenarioV());
            AddScenario(dictionary, new ScenarioX());


            return dictionary;
        }

        private static void AddScenario(Dictionary<string, Scenario> dictionary, Scenario scenario)
        {
            dictionary.Add(scenario.Id, scenario);
        }
    }
}

using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioL : Scenario
    {
        public ScenarioL()
        {
            Id = "L";
            Title = "Burswood";
            ImageIndex = (int)GameState.ImageIndex.ScenarioL;
            Text = "Walking through the streets of East Perth, you can hear a loud humming from somewhere unseen. As you approach the noise, the smell of ozone and burning metal fills the air. Rounding the corner, a massive triangular sculpture rises from a roundabout, filled with lightning, arcing off whenever anything gets close.";
            Choices = new[]
                {
                    "Try to disable the device",
                    "Try to sneak past",
                    "Turn around and leave",
                    "Hide Behind another Person"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4()
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => p.SelectedChoice == Choice.A) > (players.Count(p => p.SelectedChoice == Choice.B) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                    ActionOutcomeAndGetDisplayText = players =>
                    {
                        GameStateManager.DamagePlayers(players, player => (player.SelectedChoice == Choice.B));
                        return "You start to throw debris and other objects towards the sparking tower, and as they fly close to the triangle, giant flashes of energy streak out and vaporise the projectiles in the air. One of you gets the bright idea of driving a car into the base of the tower. Starting at the top of the hill, you start the wreck rolling down the hill, and manage to steer it towards the base of the tower. The driver bails out of the car and lands safely at the edge of the road. The car smashes into the base of the tower, as lightning strikes at it continuously. The impact causes the whole structure to topple and fall to the side, inactive. Anyone standing to close gets a shock from the residual energy burning them badly. The team walks north, and heads towards Mount Lawley.";
                    },
                    NextScenarioKey = "N"
                };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) > (players.Count(p => p.SelectedChoice == Choice.A) + (players.Count(p => p.SelectedChoice == Choice.C)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => player.SelectedChoice == Choice.A || player.SelectedChoice == Choice.C);

                    string extraText = "";
                    // if players were damaged, add more text
                    if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) + " stray too close and get a shock from the tower, causing painful burns.* After making it past, You head towards Mount Lawley";

                    return "Staying away from the energy tower seems like the best course of action, however your destination is past it. While experimenting with the range of the device, you think you can sneak around the device by staying out of range. Walking between buildings and around the alleyways, you make it most of the way around. The last hurdle is sliding along a wall, keeping as far as you can from the energy. " + extraText;
                },
                NextScenarioKey = "N"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) > (players.Count(p => p.SelectedChoice == Choice.A) + (players.Count(p => p.SelectedChoice == Choice.B)))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => player.SelectedChoice == Choice.A || player.SelectedChoice == Choice.B);

                     string extraText = "";
                    // if players were damaged, add more text
                     if (damagedPlayers.Count > 0)
                         extraText = GameStateManager.GetPlayerNames(damagedPlayers) + "stray too close during their investigations, and get shocked by the tower. ";

                     return "The lightning tower is an intimidating sight, and you decide it is insurmountable. " + extraText + "Everyone turns and walks back the up the hill and towards the WACA";
                },
                NextScenarioKey = "O"
            };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.A) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.B) == 0) &&
                    (players.Count(p => p.SelectedChoice == Choice.C) == 0),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "Everyone nervously looks around, hanging back, trying to stay further back than any other person. " + players[0].Name + " throws their hands in the air and exclaims, \"Screw this shit, let's go another way\". Nods of agreement start from the rest of the group, as you all turn and head towards the WACA.";
                },
                NextScenarioKey = "O"
            };
        }

    }
}

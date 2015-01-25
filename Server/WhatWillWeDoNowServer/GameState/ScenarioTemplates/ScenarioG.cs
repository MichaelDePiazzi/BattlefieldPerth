using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioG : Scenario
    {
        public ScenarioG()
        {
            Id = "G";
            Title = "Myer";
            ImageIndex = (int)GameState.ImageIndex.ScenarioG;
            Text = "The large department store is empty, the whole place feels like a hollow shell of what it once was. As you walk up the broken escalators to the second floor, you hear a quiet chirping and scratching noises. The second floor of the store is filled with an insectiod swarm. Small bugs fill the area, on the floor and suspended from webs strung from the ceiling. The small insects seem to ignore you as reach the top of the escalators.";
            Choices = new[]
                {
                    "Carefully walk through them",
                    "Crush as many as possible",
                    "Head down to parking lot",
                    "Look for things to loot"
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
                    (players.Count(p => p.SelectedChoice == Choice.A) >= (players.Count(p => p.SelectedChoice == Choice.B)) + (players.Count(p => p.SelectedChoice == Choice.C)))
                    ,
                ActionOutcomeAndGetDisplayText = players =>
                {
                     var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.B));
                        
                    string extraText = "";
                        if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) + " lag slightly behind the rest of the group, and the bugs catch up to them, and begin to spray a foul smelling acid, burning them painfully ";

                    return "Seeing the bugs, you decide not to flirt with danger, and make your way through without disturbing them. As you approach the exit of the store (1st Player) accidently brushes up against some of the strung up webbing. The bug all stop their mindless wandering and race towards you, " + extraText + "You get to the exit, and head towards Carillion";
                },
                NextScenarioKey = "J"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) >= players.Count(p => p.SelectedChoice == Choice.A) + players.Count(p => p.SelectedChoice == Choice.C)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                    player => true);
                    return "You rush up the stairs, with revenge and destruction on your mind. Stomping on the bugs, and tearing the webs down. The insects are initially confused, and flee the destruction. However after a few moments, then seem to mobilise and return with aggressive tactics in mind, spraying streams of acid toward everyone. You decide discretion is the better part of valour, and race towards the exit doors. Everybody has nasty chemical burns on their bodies";
                },
                NextScenarioKey = "J"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) >= players.Count(p => p.SelectedChoice == Choice.A) + players.Count(p => p.SelectedChoice == Choice.B)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayers = GameStateManager.DamagePlayers(players,
                        player => (player.SelectedChoice == Choice.A) || (player.SelectedChoice == Choice.B));
                    string extraText = "";
                    if (damagedPlayers.Count > 0)
                        extraText =  " However, " + GameStateManager.GetPlayerNames(damagedPlayers) + " get a chemical burn from acid sprayed by the bugs.";
                    return "Not knowing what these bugs are, or what they can do, you decide to avoid any confrontation. Heading down the escalators, you continue moving downwards until you reach the old parking lots. A station wagon is here with keys and a small amount of fuel. You bundle into the car, and begin driving away. As you are leaving the parking lots, bugs drop onto the car from above. The driver swerves madly and manages to dislodge them." +
                        extraText;
                },
                NextScenarioKey = "K"
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
                    //3 points of damage
                    GameStateManager.DamagePlayers(players,  player => true, 3);
                    return "Thinking greedily that this chance might never come up again, you head all the way up the stairs to the electronics section, picking up consoles, games, music players and other gadgets, you fill your backpacks and pockets with everything you can. As you all turn to leave, you see that while you were distracted and looting, bugs have begun following you up the stairs, and surrounding you. There is no way out, and nothing you can do to stop them. Waves of acid spray from the creatures, as they dissolve your corpses for nutrients.";
                },
                NextScenarioKey = "V"
            };
        }
    }
}

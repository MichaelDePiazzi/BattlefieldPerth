using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioA : Scenario
    {
        public ScenarioA()
        {
            Id = "A";
            Title = "Alien Holding Pen";
            ImageIndex = (int)GameState.ImageIndex.ScenarioA;
            Text = "The invasion started on the other side of the globe, it was fast and it was efficient. Major cities tumbled all across the globe - New York, London, Beijing, all occupied in the first days. The military was swift to respond, but were no match for The Invaders. We don't know why they captured some people, while others were just slaughtered. They found you alone, and took you to a pen, five claws of chitin tower upwards, erupting from under the ground, a thick membrane strung between them. Around you are five other prisoners, all with a spark of resistance in their eyes. A single invader stalks in a path around the pen, occasionally glancing at his captives. A section of the membrane looks like it was strung up hastily, and could be torn apart.";
            Choices = new[]
                {
                    "Make a break for it",
                    "Sneak past the guard",
                    "Attack the guard",
                    "Follow the crowd"
                };
            Outcomes = new[]
                {
                    CreateOutcome1(),
                    CreateOutcome2(),
                    CreateOutcome3(),
                    CreateOutcome4(),
                };
        }

        private static Outcome CreateOutcome1()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => p.SelectedChoice == Choice.D) == 6),
                    ActionOutcomeAndGetDisplayText = players =>
                        "Everyone sits, and waits, looking anxiously around at the other captives. Everyone wants someone to make the first move, but no one wants to step up and be the hero. The light slowly fades as the day draws to an end. A large carrier ship floats over the site, and comes to a halt above the pen. Six insectoid  appendages  burst from the bottom of the half biological half mechanical craft. They drop into the pen, hunting hungrily for anyone alive, they wrap around your bodies, and lift you into the carrier. Your fate is sealed, you will become a host for alien larvae, and will die in agony as the parasites eat your flesh for sustenance.",
                    NextScenarioKey = "X"
                };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => (p.SelectedChoice == Choice.A) || (p.SelectedChoice == Choice.B)) == 6),
                    ActionOutcomeAndGetDisplayText = players =>
                        "You all move to the weak section of the membrane, and wait for the guards attention to be focused on something else.  When the moment is right, you all push and tear at the weaker membrane and it eventually tears apart. You break out, and avoiding the guard you head away from the pen, heading into Northbridge.",
                    NextScenarioKey = "B"
                };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
                {
                    IsActive = players =>
                        (players.Count(p => (p.SelectedChoice == Choice.A) || (p.SelectedChoice == Choice.B)) >
                         players.Count(p => (p.SelectedChoice == Choice.C))),
                    ActionOutcomeAndGetDisplayText = players =>
                        {
                            var count = GameStateManager.DamagePlayers(players,
                                player => (player.SelectedChoice == Choice.C))
                                .Count;

                            //// DEBUG
                            //GameStateManager.DamagePlayers(players,
                            //    player => (player.SelectedChoice == Choice.C));
                            // GameStateManager.DamagePlayers(players,
                            //    player => (player.SelectedChoice == Choice.C));

                            return
                                "You all move to the weak section of the membrane, and wait for the guards attention to be focused on something else. As you start to sneak out, " +
                                count +
                                " turn and rush the guard, they eventually overpower the creature, but take some minor injuries in the tussle. After you have all calmed down, you head towards Northbridge.";
                        },
                    NextScenarioKey = "B"
                };
        }

        private static Outcome CreateOutcome4()
        {
            return new Outcome
            {
                IsActive = players => true,
                ActionOutcomeAndGetDisplayText = players =>
                    "You all move to the weak section of the membrane, and wait for the guards attention to be focused on something else. You rush down the guard, and overpower it as a group. You scan the area, and see a  hovering craft float across towards the north of the city. You turn and head the other way towards The Esplanade.",
                NextScenarioKey = "C"
            };
        }
    }
}

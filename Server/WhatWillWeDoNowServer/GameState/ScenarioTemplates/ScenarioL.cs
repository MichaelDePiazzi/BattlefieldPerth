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
            Text = "The trapezoidal building smokes from fires inside, and a pulsing tentacle bursts from the side of the building, and snakes towards the river. Walking through the car park you find a side entry into the building, and duck inside. There seems to be alien activity inside, as the whole atmosphere inside the building is wet, and leaves a stinging taste in your mouth. Smaller aliens scuttle through the halls, ignoring your presence.";
            Choices = new[]
                {
                    "Follow the small creatures",
                    "Head for the River",
                    "Go back outside",
                    "Find a stocked Bar"
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
                    (players.Count(p => p.SelectedChoice == Choice.A) >= players.Count(p => p.SelectedChoice == Choice.B) + players.Count(p => p.SelectedChoice == Choice.C)),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    return "Following the small creatures, you head down into the car park and the basement below. The air gets thicker and thicker as you progress. You round a corner, and find a large basement full of spires of gelatinous goo. You inspect a tower and see it has been built around the body of a person, swimming through the gel are numerous small creatures feeding off the flesh of the body. A loud splattering noise startles you from behind, as a spire bursts apart, and hundreds of larger creatures swarm towards you. More and more spires burst apart, and you are devoured as their first taste of warm flesh.";
                },
                NextScenarioKey = "X"
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
                    return "Heading towards the river you can see the large tentacle is pumping water into the casino for some nefarious purpose.  A speed boat is here and you all pile in. You take off up the Swan river, however after a few minutes of cruising, the engine starts to splutter, there is no fuel in the tanks. You drift to shore and head towards the Maylands golf course.";
                },
                NextScenarioKey = "N"
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
                    return "Something about this feels wrong, and you all agree, time to turn around and head the other way. As you all walk into Vic Park, a painfully loud screech comes from the casino, as swarms of bugs fly from the building, and head to the west.";
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
                    return "You turn away from the creature, and head for the high rollers room. It is empty, and unlocked. You all sit round a table, and pour out glasses of 30 year old scotch. You all sit back and prepare for the end. It comes swiftly as swarms of flying insects flood the room, and begin to feed on your flesh.";
                },
                NextScenarioKey = "X"
            };
        }
    }
}

using System.Linq;

namespace WhatWillWeDoNowServer.GameState.ScenarioTemplates
{
    public class ScenarioH : Scenario
    {
        public ScenarioH()
        {
            Id = "H";
            Title = "Mt Lawley";
            ImageIndex = (int)GameState.ImageIndex.ScenarioH;
            Text = "The intersection of Walcott and Beaufort street is clogged with wrecked cars, the burnt corpses of the drivers still behind the wheels. From here on you will have to progress on foot. One of you points to the roof of the Astor Theatre, and motions to be silent and hide. A single alien guard, with a weapon that you haven't seen before is scanning around looking for any signs of movement or resistance. His weapon is long and rests on his shoulder, it looks to be designed for long range shooting, and this guard is a sniper.";
            Choices = new[]
                {
                    "Try to take down the sniper",
                    "Stay low and sneak past",
                    "Turn back and find another way",
                    "Get a drink at the Scotsman"
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
                         player => (player.SelectedChoice == Choice.B) || (player.SelectedChoice == Choice.C));

                    var extraText = "";
                    if (damagedPlayers.Count > 0)
                        extraText = GameStateManager.GetPlayerNames(damagedPlayers) + " are too close to the blast, and are bunt badly. ";

                    return "Seeing the guard working alone, you think you can take him down. You sneak into the Astor, and find the stairs inside up to the roof. After a short countdown, you burst out of the door, and rush at the guard, it turns and makes a garbled noise in surprise as you all grab it and hoist it over the railing and drop it to the ground below. As the creature hits the ground, it's weapon smashes into two and begins to make a high pitched trilling noise, before it explodes in a cloud of crackling energy. " + extraText + "You take some supplies from the surrounding stores, and walk towards Maylands.";
                },
                NextScenarioKey = "K"
            };
        }

        private static Outcome CreateOutcome2()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.B) >= (players.Count(p => p.SelectedChoice == Choice.A)) + (players.Count(p => p.SelectedChoice == Choice.C)))
                    ,
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => (p.IsAlive) && (p.SelectedChoice == Choice.A))) ;
                    damagedPlayer.HitPoints -= 3;

                    return "Keeping low, and ducking from car to car, you try to sneak past the alien sniper. After a few heart pounding seconds, you race to the north, trying to put as much distance between you and the marksman. You make it past the intersection and up the street without being spotted. " + damagedPlayer + " stands up and walks out from behind a car, thinking they are far enough away. A high pitched whistle cuts through the air, as a flechette buries into their skull, followed by their head exploding in a mess of gore. Leaving the scene of the disaster behind, you continue towards Maylands";
                },
                NextScenarioKey = "K"
            };
        }

        private static Outcome CreateOutcome3()
        {
            return new Outcome
            {
                IsActive = players =>
                    (players.Count(p => p.SelectedChoice == Choice.C) >= (players.Count(p => p.SelectedChoice == Choice.A)) + (players.Count(p => p.SelectedChoice == Choice.B))),
                ActionOutcomeAndGetDisplayText = players =>
                {
                    var damagedPlayer = GameStateManager.GetRandomPlayer(players.Where(p => (p.IsAlive) && (p.SelectedChoice == Choice.A)));
                    damagedPlayer.HitPoints -= 3;
                    return "If there is one sniper there may be others unseen. Knowing that the direction you just came from is safer, you turn around and head towards the freeway.  " + damagedPlayer + " stands up and walks out from behind a car, thinking they are far enough away. A high pitched whistle cuts through the air, as a flechette buries into their skull, followed by their head exploding in a mess of gore. Once you get to the wide roads, you progress over the bridge, and take the exit towards the Burswood casino.";
                },
                NextScenarioKey = "L"
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
                    var randomPlayer = GameStateManager.GetRandomPlayer(players.Where(p => p.IsAlive));

                    return "Everyone turns, and shrugs while walking into the Flying Scotsman. The beer on tap is warm and flat, but the spirits on the back wall are still good. Shots of vodka are poured out, and everyone knocks it back. " + randomPlayer + " remarks, \"I bet there is better booze at the Casino, probably other goodies as well\". Everyone agrees, and packs up what they can and head towards the freeway. Once you get to the wide roads, you progress over the bridge, and take the exit towards the Burswood casino.";
                },
                NextScenarioKey = "L"
            };
        }

       
   
    }
}

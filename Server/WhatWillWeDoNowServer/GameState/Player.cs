using System;

namespace WhatWillWeDoNowServer.GameState
{
    public class Player
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public Choice SelectedChoice { get; set; }
        public DateTime LastRequest { get; set; }

        public bool IsAlive { get { return HitPoints > 0; } }
        public bool HasMadeChoice { get { return SelectedChoice != Choice.None; } }

        public Player(string name)
        {
            Name = name;
            SelectedChoice = Choice.None;
            HitPoints = 3;
            LastRequest = DateTime.Now;
        }
    }
}

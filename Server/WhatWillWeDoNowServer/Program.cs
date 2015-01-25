using System;
using Microsoft.Owin.Hosting;

namespace WhatWillWeDoNowServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //NOTE: This will throw an exception if you're not running as Administrator
            WebApp.Start<Startup>("http://+:9000/");

            Console.WriteLine("Server is running. Press any key to stop.");
            Console.ReadLine();
        }
    }
}

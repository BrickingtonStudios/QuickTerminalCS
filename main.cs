using System;
using System.Diagnostics;

namespace QuickTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "QuickTerminal";
            while (true)
            {
                Console.Write("Command: ");
                string cmd = Console.ReadLine();
                string[] command = cmd.Trim().ToLower().Split(' ');

                if (command[0] == "exit")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
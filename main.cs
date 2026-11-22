using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QuickTerminal
{
    class Command
    {
        public string Name { get; }
        public string Desc { get; }
        public string[] Aliases { get; }

        private readonly Action<string[]> _execute;

        public Command(string name, string description, Action<string[]> execute, params string[] aliases)
        {
            Name = name;
            Desc = description;
            _execute = execute;
            Aliases = aliases;
        }

        public virtual void Execute(string[] args)
        {
            _execute(args);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "QuickTerminal";
            Console.SetWindowSize(145, 40);

            string art = File.ReadAllText("resources/art.txt");
            Console.WriteLine(art);

            List<Command> commands = new List<Command>();

            commands.Add(new Command("exit", "Exits QuickTerminal", commandArgs =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            }, "quit", "close", "stop", "end", "terminate", "abandon"));

            commands.Add(new Command("clear", "Clear the terminal", commandArgs =>
            {
                Console.Clear();
            }, "cls", "clearscreen", "refresh"));

            commands.Add(new Command("help", "Shows this message", commandArgs =>
            {
                foreach (Command cmd in commands)
                    Console.WriteLine($"{cmd.Name} - {cmd.Desc}");
            }, "help"));

            while (true)
            {
                Console.Write("Command: ");
                string cmd = Console.ReadLine();
                string[] parts = cmd.Trim().ToLower().Split(' ');

                if (parts.Length == 0)
                    continue;

                string name = parts[0].ToLower();
                string[] commandArgs = parts.Skip(1).ToArray();

                Command selected = commands.Find(c => c.Name == name || c.Aliases.Contains(name));

                if (selected != null)
                    selected.Execute(commandArgs);
                else
                    Console.WriteLine("Invalid command");
            }
        }
    }
}
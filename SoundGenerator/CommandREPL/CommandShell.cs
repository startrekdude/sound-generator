using System;
using System.Collections.Generic;

namespace SoundGenerator.CommandREPL
{
    public delegate void CommandDelegate (String[] argv, CommandShell cmd, Command com);
    public class CommandShell
    {
        protected static class DefaultCommands
        {
            public static void Exit(String[] argv, CommandShell cmd, Command com)
            {
                cmd.Active = false;
            }

            public static void List(String[] argv, CommandShell cmd, Command com)
            {
                Console.Write((String)com.Data[0]);
                int cmd_index = 1;
                foreach (KeyValuePair<String, Command> single_command in cmd.Commands)
                {
                    Console.WriteLine("Command #" + cmd_index + ": -- " + single_command.Key + " ---- " + single_command.Value.Description + ".");
                    cmd_index += 1;
                }
            }
        }

        public Boolean Active { get; set; }
        public Dictionary<String, Command> Commands { get; protected set; }
        public String Lead { get; set; }
        public ConsoleColor BGColor { get; set; }
        public ConsoleColor FGColor { get; set; }
        public String Title { get; set; }
        public String InvalidInputMsg { get; set; }
        public String TopMsg { get; set; }

        public CommandShell()
        {
            Active = true;
            Commands = new Dictionary<String, Command>();
            Command exit = new Command(new CommandDelegate(DefaultCommands.Exit), "Exits the Command Shell", new Object[0]);
            Command list = new Command(new CommandDelegate(DefaultCommands.List), "Lists all Commands", new Object[] { "lst -- Lists all available commands.\n\nCommands: \n" });
            Commands.Add("exit", exit);
            Commands.Add("lst", list);
            Lead = " >>> ";
            BGColor = ConsoleColor.Black;
            FGColor = ConsoleColor.White;
            Title = ".NET Command Shell";
            InvalidInputMsg = "Unknown Command. Failing...";
            TopMsg = ".NET Command Shell v1.0 \n Type \"lst\" for help.\n\n";
        }

        public virtual void Run()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = FGColor;
            Console.Title = Title;
            Console.Write(TopMsg);
            while (this.Active)
            {
                Console.Write(Lead);
                String input = Console.ReadLine();
                TryExecute(input);
            }
            Exit();
        }

        public virtual void Exit()
        {
            Console.CursorVisible = true;
            Console.ResetColor();
        }

        public void TryExecute(String command)
        {
            Boolean found = false;
            foreach (String str in Commands.Keys)
            {
                if (command.Split(' ')[0] == str)
                {
                    Command cmd = Commands[str];
                    cmd.Method(command.Split(' '), this, cmd);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.Error.WriteLine(InvalidInputMsg);
            }
        }
    }
}

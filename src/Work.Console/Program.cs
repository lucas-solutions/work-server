using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    using Lucas.Solutions.Configuration;
    using Lucas.Solutions.IO;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            program.InitializeCommands();

            program.InputLoop();
        }

        private bool _running;
        private Dictionary<string, CommandDefinition> _commands;

        private Program()
        {
            _running = true;
            _commands = new Dictionary<string, CommandDefinition>();
        }

        void InputLoop()
        {
            Console.Write(Strings.PROMPT);

            do
            {
                var input = Console.ReadLine();

                try
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                    }
                    else if (TryPerform(input))
                    {
                    }
                    else
                    {
                        false.Assert(true, () => Strings.ERROR_UNKNOW_REQUEST);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(Strings.ERROR_PRINT + (e.InnerException != null ? e.InnerException.Message : e.Message));
                }
                finally
                {
                    Console.WriteLine();
                    Console.Write(Strings.PROMPT);
                }
            }
            while (_running);
        }

        void InitializeCommands()
        {
            Register(
                keyword: "status",
                syntax: () => Strings.CMD_STATUS_SYNTAX,
                summary: () => Strings.CMD_STATUS_SUMMARY,
                example: () => Strings.CMD_STATUS_EXAMPLE,
                perform: (string[] args) =>
                {
                    Console.WriteLine("Not implemented");
                });

            Register(
                keyword: "clear",
                syntax: () => Strings.CMD_CLEAR_SYNTAX,
                summary: () => Strings.CMD_CLEAR_SUMMARY,
                example: () => Strings.CMD_CLEAR_EXAMPLE,
                perform: (string[] args) =>
                {
                    args.Length
                        // Evaluate precondition. A circle must be contructed from 3 arguments
                        .Assert(0, () => string.Format(Strings.ERROR_ARGUMENT_COUNT, "clear", 0, args.Length));

                    Console.Clear();
                });

            Register(
                keyword: "scheduler",
                syntax: () => Strings.CMD_SCHEDULER_SYNTAX,
                summary: () => Strings.CMD_SCHEDULER_SUMMARY,
                example: () => Strings.CMD_SCHEDULER_EXAMPLE,
                perform: (string[] args) =>
                {
                    switch (args.Length)
                    {
                        case 0:
                            break;

                        case 1:
                            switch (args[0].ToUpperInvariant())
                            {
                                case "START":
                                    break;

                                case "STOP":
                                    break;

                                default:
                                    false.Assert(true, Strings.ERROR_UNKNOW_REQUEST);
                                    break;
                            }
                            break;
                        default:
                            false.Assert(true, () => string.Format(Strings.ERROR_ARGUMENT_COUNT, "scheduler", 1, args.Length));
                            break;
                    }
                });

            Register(
                keyword: "exit",
                syntax: () => Strings.CMD_EXIT_SYNTAXIS,
                summary: () => Strings.CMD_EXIT_SUMMARY,
                example: () => Strings.CMD_EXIT_EXAMPLE,
                perform: (string[] args) =>
                {
                    _running = false;
                });

            Register(
                keyword: "help",
                syntax: () => Strings.CMD_HELP_SYNTAX,
                summary: () => Strings.CMD_HELP_SUMMARY,
                example: () => Strings.CMD_HELP_EXAMPLE,
                perform: (string[] args) =>
                {
                    Action<string, CommandDefinition> PrintCommandDefinition = (string keyword, CommandDefinition cmd) =>
                        Console.WriteLine(string.Format(Strings.HELP_CMD_DEFINITION_FORMAT, keyword, cmd.Syntax(), cmd.Summary(), cmd.Example()).Replace("\\n", "\r\n").Replace("\\t", "    "));

                    if (args.Length > 0)
                    {
                        foreach (var arg in args)
                        {
                            CommandDefinition cmd;
                            if (_commands.TryGetValue(arg.ToLower(), out cmd))
                            {
                                Console.WriteLine();
                                PrintCommandDefinition(arg.ToLower(), cmd);
                            }
                        }
                    }
                    else
                    {
                        foreach (var cmd in _commands)
                        {
                            Console.WriteLine();
                            PrintCommandDefinition(cmd.Key, cmd.Value);
                        }
                    }
                });

            Register(
                keyword: "run",
                syntax: () => Strings.CMD_RUN_SYNTAX,
                summary: () => Strings.CMD_RUN_SUMMARY,
                example: () => Strings.CMD_RUN_EXAMPLE,
                perform: (string[] args) =>
                {
                    var container = new ConfigurationContainer();
                    var transfers = container.Transfers;

                    if (args.Length == 0)
                    {
                        args = transfers.Select(transfer => transfer.Name).ToArray();
                    }

                    foreach (var name in args)
                    {
                        var transfer = transfers.FirstOrDefault(tran => tran.Name == name);

                        var worker = new TransferWorker() { Transfer = transfer };
                        worker.Work();

                        if (transfer == null)
                        {
                            Console.WriteLine("Transfer \"{0}\" not found.", name);
                        }
                    }
                });

            Register(
                keyword: "master",
                syntax: () => Strings.CMD_MASTER_SYNTAX,
                summary: () => Strings.CMD_MASTER_SUMMARY,
                example: () => Strings.CMD_MASTER_EXAMPLE,
                perform: (string[] args) =>
                {
                    var container = new ConfigurationContainer();

                    if (args.Length == 0)
                    {
                        args = new[] { "hosts", "users", "roles", "transfers" };
                    }

                    foreach (var set in args)
                    {
                        switch (set.ToUpperInvariant())
                        {
                            case "HOSTS":
                                Console.WriteLine("Hosts:");
                                foreach (var host in container.Hosts)
                                {
                                    Console.WriteLine("* Host (Name: \"{0}\", Address: \"{1}\", Protocol: \"{2}\")", host.Name, host.Address, host.Protocol);
                                }
                                Console.WriteLine();
                                break;

                            case "USERS":
                                Console.WriteLine("Users:");
                                foreach (var user in container.Users)
                                {
                                    Console.WriteLine("* User (Email: \"{0}\", Roles: \"{1}\")", user.Email, string.Join(", ", user.Roles.Select(role => role.Name)));
                                }
                                Console.WriteLine();
                                break;

                            case "ROLES":
                                Console.WriteLine("Roles:");
                                foreach (var role in container.Roles)
                                {
                                    Console.WriteLine("* Role (Name: \"{0}\")", role.Name);
                                }
                                Console.WriteLine();
                                break;

                            case "TRANSFERS":
                                Console.WriteLine("Transfers:");
                                foreach (var transfer in container.Transfers)
                                {
                                    Console.WriteLine("* Transfer (Name: \"{0}\", Start: \"{1}\", Parties:", transfer.Name, transfer.Start);
                                    foreach (var party in transfer.Parties)
                                    {
                                        Console.WriteLine("    * Party (Name: \"{0}\", Direction: \"{1}\", Email: \"{2}\", Host: \"{3}\")", party.Name, party.Direction, party.Email, party.Host);
                                    }
                                    Console.WriteLine("  )");
                                }
                                Console.WriteLine();
                                break;

                            default:
                                Console.WriteLine("Unrecognized set \"{0}\"", set);
                                break;
                        }
                    }
                });
        }

        void Register(string keyword, Func<string> syntax, Func<string> summary, Func<string> example, Action<string[]> perform)
        {
            _commands[keyword.ToLower()] = new CommandDefinition
            {
                Perform = perform,
                Syntax = syntax,
                Summary = summary,
                Example = example
            };
        }

        void Masterdata()
        {
            var container = new ConfigurationContainer();

            Console.WriteLine("Hosts:");
            foreach (var host in container.Hosts)
            {
                Console.WriteLine("* Host (Name: {0}, Address: {1}, Port: {2}, Protocol: {3})", host.Name, host.Address, host.Port, host.Protocol);
            }
            Console.WriteLine();
            Console.WriteLine("Users:");
            foreach (var user in container.Users)
            {
                Console.WriteLine("* User (Email: {0}, Roles: {1})", user.Email, string.Join(", ", user.Roles.Select(role => role.Name)));
            }
            Console.WriteLine();
            Console.WriteLine("Roles:");
            foreach (var role in container.Roles)
            {
                Console.WriteLine("* Role (Name: {0})", role.Name);
            }
            Console.WriteLine();
            Console.WriteLine("Transfers:");
            foreach (var transfer in container.Transfers)
            {
                Console.WriteLine("* Transfer (Name: {0}, Start: {1}, Parties:", transfer.Name, transfer.Start);
                foreach (var party in transfer.Parties)
                {
                    Console.WriteLine("    * Party (Name: {0}, Direction: {1}, Email: {2}, Host: {3})", party.Name, party.Direction, party.Email, party.Host);
                }
                Console.WriteLine("  )");
            }
            Console.WriteLine();

            Console.WriteLine("Enter any key:");
            Console.ReadKey();
        }

        bool TryPerform(string s)
        {
            var args = s.Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);

            CommandDefinition cmd;
            if (_commands.TryGetValue(args[0].ToLower(), out cmd))
            {
                var action = cmd.Perform;

                if (action != null)
                {
                    action(args.Skip(1).ToArray());
                }

                return true;
            }

            return false;
        }
    }
}

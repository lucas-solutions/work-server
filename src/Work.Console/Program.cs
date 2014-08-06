using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    using Lucas.Solutions.Configuration;
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
            /*Register(
                keyword: "count",
                syntax: () => Strings.CMD_COUNT_SYNTAX,
                summary: () => Strings.CMD_COUNT_SUMMARY,
                example: () => Strings.CMD_COUNT_EXAMPLE,
                perform: (string[] args) =>
                {
                    if (args.Length > 0)
                    {
                        var count = _canvas.Keys.Select(key => new { Key = key, Value = _canvas[key] }).Where(o => o.Value != null && args.Contains(o.Value.GetType().Name.ToLower())).Count();
                        Console.WriteLine(Strings.SHAPE_COUNT_FORMAT, count);
                    }
                    else
                    {
                        Console.WriteLine(Strings.SHAPE_COUNT_FORMAT, _canvas.Count);
                    }
                });*/

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
            /*
            Register(
                keyword: "delete",
                syntax: () => Strings.CMD_DELETE_SYNTAX,
                summary: () => Strings.CMD_DELETE_SUMMARY,
                example: () => Strings.CMD_DELETE_EXAMPLE,
                perform: (string[] args) =>
                {
                    (args.Length > 0)
                        // Evaluate precondition. A circle must be contructed from 3 arguments
                        .Assert(true, () => string.Format(Strings.ERROR_ARGUMENT_COUNT, "delete", 1, args.Length));

                    if (args.Length.Equals(1) && args[0].Equals("all"))
                    {
                        _canvas.Clear();
                    }
                    else
                    {
                        for (var i = 0; i < args.Length; i++)
                        {
                            int key;
                            if (!args[i].TryParse(out key))
                            {
                                // Evaluate precondition. All arguments must be a double type
                                Console.WriteLine(Strings.ERROR_PRINT + Strings.ERROR_ARGUMENT_TYPE, "integer", args[i], i);
                                continue;
                            }

                            var shape = _canvas.Remove(key);

                            _pipeStream.Send("delete {0}", key);

                            if (shape != null)
                            {
                                Console.WriteLine(Strings.CMD_DELETE_SUCCESS, key, shape);
                            }
                            else
                            {
                                Console.WriteLine(Strings.ERROR_PRINT + Strings.ERROR_SHAPE_NOT_FOUND, key);
                            }
                        }
                    }
                });

            /*Register(
                keyword: "draw",
                syntax: () => Strings.CMD_DRAW_SYNTAX,
                summary: () => Strings.CMD_DRAW_SUMMARY,
                example: () => Strings.CMD_DRAW_EXAMPLE,
                perform: (string[] args) =>
                {
                    (args.Length > 0)
                        // Evaluate precondition. A circle must be contructed from 3 arguments
                        .Assert(true, () => string.Format(Strings.ERROR_ARGUMENT_COUNT, "draw", 1, args.Length));

                    for (var i = 0; i < args.Length; i++)
                    {
                        int key;

                        if (args[i].ToLower().Equals("auto"))
                        {
                            _autodraw = true;
                        }
                        else if (args[i].ToLower().Equals("off"))
                        {
                            _autodraw = false;
                        }
                        else if (int.TryParse(args[i], out key))
                        {
                            var shape = _canvas[key];

                            if (shape == null)
                            {
                                Console.WriteLine(Strings.ERROR_PRINT + Strings.ERROR_SHAPE_NOT_FOUND, key);
                                continue;
                            }

                            Console.Write(Strings.SHAPE_PRINT, key);
                            Console.WriteLine(shape.Print());

                            if (_autodraw)
                            {
                                _pipeStream.Send(string.Join(" ", key, shape.Stringify()));
                            }
                        }
                        else
                        {
                            Console.WriteLine(Strings.ERROR_PRINT + Strings.ERROR_ARGUMENT_TYPE, "integer", args[i], i);
                        }
                    }
                });*/

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
                keyword: "list",
                syntax: () => Strings.CMD_LIST_SYNTAX,
                summary: () => Strings.CMD_LIST_SUMMARY,
                example: () => Strings.CMD_LIST_EXAMPLE,
                perform: (string[] args) =>
                {
                    if (args.Length > 0)
                    {
                        /*var matches = _canvas.Keys.Select(key => new { Key = key, Value = _canvas[key] }).Where(o => o.Value != null && args.Contains(o.Value.GetType().Name.ToLower())).ToArray();
                        Console.WriteLine(Strings.SHAPE_COUNT_FORMAT, matches.Length);
                        foreach (var shape in matches)
                        {
                            Console.Write(Strings.SHAPE_PRINT, shape.Key);
                            Console.WriteLine(shape.Value.Print());
                        }*/
                    }
                    else
                    {
                        /*Console.WriteLine(Strings.SHAPE_COUNT_FORMAT, _canvas.Count);
                        foreach (var key in _canvas.Keys)
                        {
                            var shape = _canvas[key];
                            Console.Write(Strings.SHAPE_PRINT, key);
                            Console.WriteLine(shape.Print());
                        }*/
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

        void Run()
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

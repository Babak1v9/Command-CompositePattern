using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public class Player {

        private List<ICommand> _commands = new List<ICommand>();
        private int _count = 0;
        protected ICommand _command;

        public string username;
        public List<Game> games = new List<Game>();

        public Player(string name) {
            username = name;
            Console.WriteLine($"Player {username} created.");
        }

        public void ExecuteCommand(string input, Game currentGame) {

            _command = input switch {
                "Buy" => new BuyCommand(currentGame),
                "Download" => new DownloadCommand(currentGame),
                "Install" => new InstallCommand(currentGame),
                "Start" => new StartCommand(currentGame),
                "Deinstall" => new DeinstallCommand(currentGame),
                "Update" => new UpdateCommand(currentGame),
                "Play" => new PlayedCommand(currentGame),
                "Lent" => new LentCommand(currentGame),
                "Lend" => new LendCommand(currentGame),
                "RequestBack" => new RequestBackCommand(currentGame),
                "Return" => new ReturnCommand(currentGame),
                _ => null,
            };

            _command.Execute();

            //Add command to undo list
            _commands.Add(_command);
            _count++;
        }

        public void Undo(int levels) {
            Console.WriteLine("\n---- Undo {0} levels ", levels);
            // Perform undo operations
            for (int i = 0; i < levels; i++) {
                if (_count > 0) {
                    ICommand command = _commands[--_count] as ICommand;
                    command.UnExecute();
                }
            }
        }

        public void Redo(int levels) {
            Console.WriteLine("\n---- Redo {0} levels ", levels);
            // Perform redo operations
            for (int i = 0; i < levels; i++) {
                if (_count < _commands.Count - 1) {
                    ICommand command = _commands[_count++];
                    command.Execute();
                }
            }
        }
    }

}


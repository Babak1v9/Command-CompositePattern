using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public class MacroCommand : ICommand {
        private Game _game;
        protected List<ICommand> children = new List<ICommand>();

        public MacroCommand(Game game) {
            _game = game;
        }
        public void Execute() {

            foreach (var child in children) {
                child.Execute();
            }
        }

        public void UnExecute() {
        }

        public void AddChild(ICommand child) {
            children.Add(child);
        }

        public void DeleteChild() {
            children.RemoveAt(children.Count - 1);
        }
    }

    public class OneClickPlay : MacroCommand {
        private Game _game;

        public OneClickPlay(Game game) : base(game) {
            _game = game;

            if (_game._state.GetType() == typeof(Started)) {
                Console.WriteLine("Game is already running.");
            } else if (_game._state.GetType() == typeof(Created)) {
                AddChild(new BuyCommand(_game));
                AddChild(new DownloadCommand(_game));
                AddChild(new InstallCommand(_game));
                AddChild(new StartCommand(_game));
            } else if (_game._state.GetType() == typeof(Bought)) {
                AddChild(new DownloadCommand(_game));
                AddChild(new InstallCommand(_game));
                AddChild(new StartCommand(_game));
            } else if (_game._state.GetType() == typeof(Downloaded)) {
                AddChild(new InstallCommand(_game));
                AddChild(new StartCommand(_game));
            } else if (_game._state.GetType() == typeof(Installed)) {
                AddChild(new StartCommand(_game));
            } else {
                Console.WriteLine("Unable to execute 1-Click-Play in current state");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {

    //concrete commands
    public class BuyCommand : ICommand {
        private Game _game;
        public BuyCommand(Game game){
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Buy");
        }

        public void UnExecute() {
        }
    }

    public class DownloadCommand : ICommand {
        private Game _game;
        public DownloadCommand(Game game){
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Download");
        }
        public void UnExecute() {

        }
    }

    public class InstallCommand : ICommand {
        private Game _game;
        public InstallCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Install");
        }
        public void UnExecute() {
        }
    }

    public class StartCommand : ICommand {
        private Game _game;
        public StartCommand(Game game){
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Start");
        }
        public void UnExecute() {
        }
    }

    public class PlayedCommand : ICommand {
        private Game _game;
        public PlayedCommand(Game game){
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Play");
        }
        public void UnExecute() {
        }
    }

    public class DeinstallCommand : ICommand {
        private Game _game;
        public DeinstallCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Deinstall");

        }
        public void UnExecute() {
        }
    }

    public class UpdateCommand : ICommand {
        private Game _game;
        public UpdateCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Update");

        }
        public void UnExecute() {
        }
    }

    public class LentCommand : ICommand {
        private Game _game;
        public LentCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Lent");

        }
        public void UnExecute() {
        }
    }

    public class LendCommand : ICommand {
        private Game _game;
        public LendCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Lend");

        }
        public void UnExecute() {
        }
    }

    public class RequestBackCommand : ICommand {
        private Game _game;
        public RequestBackCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("RequestBack");

        }
        public void UnExecute() {
        }
    }

    public class ReturnCommand : ICommand {
        private Game _game;
        public ReturnCommand(Game game) {
            _game = game;
        }
        public void Execute() {
            _game.ChangeState("Return");

        }
        public void UnExecute() {
        }
    }


}



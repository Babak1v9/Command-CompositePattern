using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public class TradeHandler {
        private static TradeHandler instance = null;
        private static readonly object padlock = new object();
        public List<Player> activePlayers = new List<Player>();

        TradeHandler() {
        }

        public static TradeHandler Instance {
            get {
                lock (padlock) {
                    if (instance == null) {
                        instance = new TradeHandler();
                    }
                    return instance;
                }
            }
        }

        void SortPlayerList() {
            activePlayers.Sort(delegate (Player x, Player y) {
                return x.username.CompareTo(y.username);
            });
        }

        public void ReturnActivePlayers() {

            SortPlayerList();
            int i = 1;
            foreach (var player in activePlayers) {
                    Console.WriteLine($"ID: {i} - Name: {player.username,-20}");
                i++;
            }
        }
        
        public void ReturnPlayerGames(Player playerForTrade) {
            var games = playerForTrade.games;

            Console.WriteLine($"Player {playerForTrade.username} selected.");


            int i = 1;
            foreach (var game in games) {
                if (game._state.ToString().Substring(6).Equals("Created")) {
                    Console.WriteLine($"ID: {i} - Name: {game.name,-20}");
                } else {
                    Console.WriteLine($"ID: {i} - Name: {game.name,-20} Status: {game._state.ToString().Substring(22)}");
                }
                i++;
            }
        }

        public void IncomingRequest(Game game, Player currentPlayer, Player player2) {
            var userInput = "";

            Console.WriteLine($"Do you want to lend your game {game.name} to {player2.username}? (Y/N)");

            userInput = Console.ReadLine();

            if (userInput.ToLower().Equals("y") || userInput.ToLower().Equals("yes")) {
                game.lent = true;
                game.lentTo = player2.username;
                game.lentFrom = currentPlayer.username;
                game.stateBeforeLend = game._state.GetType();
                currentPlayer.ExecuteCommand("Lent", game);
                Console.WriteLine($"Game {game.name} lend successfully to Player {player2.username}");
            }
    
        }

        public void LendGame(Game game, Player currentPlayer, Player player2) {
            game.lent = false;
            game.lentTo = currentPlayer.username;
            game.lentFrom = player2.username;
            game.stateBeforeLend = game._state.GetType();

            currentPlayer.ExecuteCommand("Lend", game);
            Console.WriteLine($"Game {game.name} lent successfully from Player {player2.username}");
        }
    }    
}

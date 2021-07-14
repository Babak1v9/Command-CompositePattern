using System;
using System.Collections.Generic;

namespace Command_and_Composite {
    class Program {
        static void Main(string[] args) {

            var userInput = "";
            int tmp;
            int ocpCount = 0;

            Console.WriteLine("Please enter your username:");
            string playerName = Console.ReadLine();

            Player currentPlayer = new Player(playerName);
            Player randomPlayer = new Player("LernhardBueger");
            Player bestPlayer = new Player("HaoulRolzer");

            //init tradeHandler
            TradeHandler tr = TradeHandler.Instance;
            //add players to list
            tr.activePlayers.Add(randomPlayer);
            tr.activePlayers.Add(bestPlayer);

            //init games
            List<Game> games = new List<Game>();

            var lol = new Game(new Created(), "League of Legends", "League of Legends ist ein teambasiertes Strategiespiel\n, in dem zwei Teams mit je fünf starken Champions gegeneinander antreten, um die jeweils andere Basis zu zerstören");
            var d3 = new Game(new Created(), "Diablo 3", "Diablo III ist ein Action-Rollenspiel des US-amerikanischen Spielentwicklers Blizzard Entertainment\n und die Fortsetzung von Diablo II");
            var aoe = new Game(new Created(), "Age of Empires", "Age of Empires ist ein Echtzeit-Strategiespiel,\n das von Ensemble Studios für die Microsoft-Game-Studios entwickelt wurde");

            games.Add(lol);
            games.Add(d3);
            games.Add(aoe);
            randomPlayer.games.Add(lol);
            bestPlayer.games.Add(d3);


            // sort list
            games.Sort(delegate (Game x, Game y) {
                return x.name.CompareTo(y.name);
            });

            //other player lends game from currentplayer
            currentPlayer.ExecuteCommand("Buy", aoe);
            tr.IncomingRequest(aoe, currentPlayer, randomPlayer);

            do {

                //ask user for input
                Console.WriteLine("\nGames:");

                int i = 1;
                foreach (var game in games) {
                    if (game._state.ToString().Substring(22).Equals("Created")) {
                        Console.WriteLine($"ID: {i} - Name: {game.name,-20} Status: Not Owned");
                    } else if (game._state.ToString().Substring(22).Equals("Lend")) {
                        Console.WriteLine($"ID: {i} - Name: {game.name,-20} Status: {game._state.ToString().Substring(22)} from {game.lentFrom}");
                    } else {
                            Console.WriteLine($"ID: {i} - Name: {game.name,-20} Status: {game._state.ToString().Substring(22)}");
                    }
                    i++;
                }

                Console.WriteLine("\nWhich Game do you want to buy/select: ");
                Console.WriteLine("Choose via ID, enter 'Trade' for trading or quit with 'exit'!");

                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out tmp)) {

                    userInput = tmp.ToString();

                    if (Convert.ToInt32(userInput) > games.Count) {
                        Console.WriteLine("There is no game at index " + userInput + ". Please open your eyes.");
                    } else {

                        var chosenGame = games[Convert.ToInt32(userInput) - 1];

                        Console.WriteLine(Environment.NewLine + "You chose:\n");
                        chosenGame.GetGameInfo();

                        do {
                            //offer for 1-Click-Play Macro
                            if (ocpCount < 1) {
                                Console.WriteLine("Would you like to use your 1-Click-Play Macro? (Y/N)");
                                userInput = Console.ReadLine();

                                if (userInput.ToLower().Equals("y") || userInput.ToLower().Equals("yes")) {
                                    OneClickPlay ocp = new OneClickPlay(chosenGame);
                                    ocp.Execute();     
                                }
                                ocpCount++;
                            }


                            Console.WriteLine(Environment.NewLine + "Available actions:\n");

                            //get all possible actions for current state
                            List<string> availableActions = chosenGame.GetAvailableActions();

                            if (availableActions.Count == 1) {
                                if (availableActions[0] == "Play") {
                                    Console.WriteLine("Do you want to quit this Game?");
                                } else if (availableActions[0] == "Buy") {
                                    Console.WriteLine("Do you want to " + availableActions[0] + " this Game?");
                                } else {
                                    Console.WriteLine("Do you want to " + availableActions[0] + " this Game?");
                                }

                                Console.WriteLine("Enter (Y/N), quit with 'exit' or go back to the Menu with 'menu'!");
                                userInput = Console.ReadLine();

                                if (userInput.ToLower().Equals("y") || userInput.ToLower().Equals("yes")) {
                                    if (availableActions[0] == "Buy") {
                                        currentPlayer.games.Add(chosenGame);
                                        Console.WriteLine("Added Game to List.");
                                    }
                                    currentPlayer.ExecuteCommand(availableActions[0], chosenGame);
                                }
                            } else {
                                i = 1;
                                foreach (var action in availableActions) {
                                    Console.WriteLine(i + " - " + action);
                                    i++;
                                }

                                Console.WriteLine("Enter Action via ID, quit with 'exit' or go back to the Menu with 'menu'!");

                                userInput = Console.ReadLine();
                                if (int.TryParse(userInput, out tmp)) {

                                    int userAction = tmp;
                                    if (userAction > availableActions.Count) {
                                        Console.WriteLine("\nThere is no action at index " + userAction + ". Please open your eyes.");
                                        System.Environment.Exit(1);
                                    }

                                    var stateAction = availableActions[userAction - 1];
                                    Console.WriteLine("You chose to " + stateAction + " this game.");
                                    currentPlayer.ExecuteCommand(stateAction, chosenGame);

                                }
                            }
                        } while (!userInput.ToLower().Equals("exit") && !userInput.ToLower().Equals("menu") && !userInput.ToLower().Equals("n"));
                    }
                } else if (userInput.ToLower().Equals("trade")){

                    if (userInput.ToLower().Equals("trade")) {
                        Console.WriteLine("Active Players:");
                        tr.ReturnActivePlayers();
                        Console.WriteLine("\nChoose Player for Trade via ID, enter 'Menu' to return or quit with 'Exit'!");
                        userInput = Console.ReadLine();

                        if (int.TryParse(userInput, out tmp)) {
                            var playerForTrade = tr.activePlayers[tmp-1];
                            tr.ReturnPlayerGames(playerForTrade);
                            Console.WriteLine("Choose via ID");
                            userInput = Console.ReadLine();

                            if (int.TryParse(userInput, out tmp)) {
                                var gameToLend = playerForTrade.games[tmp - 1];
                                Console.WriteLine("Request sent.\n Request Pending...");
                                System.Threading.Thread.Sleep(3000);
                                Console.WriteLine("Request accepted.");
                                tr.LendGame(gameToLend, currentPlayer, playerForTrade);
                            }
                        }
                    }
                } else {
                    if (!userInput.ToLower().Equals("exit")) {
                        Console.WriteLine("That was neither a number nor 'exit'. Please try again.");
                    }
                }
            } while (!userInput.ToLower().Equals("exit"));
                Console.WriteLine("Bye!");
                System.Environment.Exit(1);
        }
    }
}

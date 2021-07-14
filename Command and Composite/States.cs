using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {

    class Created : State {

        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Buy"
        };

    public override void ChangeState() {
        this._state.TransitionTo(new Bought());
    }
    public override void ChangeState(string newState) {
            switch(newState) {
                case "Buy":
                    this._state.TransitionTo(new Bought());
                    break;
                case "Lend":
                    this._state.TransitionTo(new Lend());
                break;
                default:
                break;
            }
        }
}

    class Bought : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Download",
        };

        public override void ChangeState() {
            this._state.TransitionTo(new Downloaded());
        }
        public override void ChangeState(string newState) {
            switch (newState) {
                case "Download":
                    this._state.TransitionTo(new Downloaded());
                    break;
                case "Lent":
                    this._state.TransitionTo(new Lent());
                    break;
                default:
                    break;
            }
        }
    }

    class Downloaded : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Install", 
        };

        public override void ChangeState() {
            this._state.TransitionTo(new Installed());
        }

        public override void ChangeState(string newState) {
            switch (newState) {
                case "Install":
                    this._state.TransitionTo(new Installed());
                    break;
                case "Lent":
                    this._state.TransitionTo(new Lent());
                    break;
                default:
                    break;
            }
        }
    }

    class Installed : State {

        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Start","Deinstall","Update", 
        };

        public override void ChangeState() {
            if (!this._state.lent) {
                this._state.TransitionTo(new Started());
            } else {
                Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
            }
        }

        public override void ChangeState(string newState) {
            switch (newState) {
                case "Start":
                    if (!this._state.lent) {
                        this._state.TransitionTo(new Started());
                    } else {
                        Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
                    }
                    break;
                case "Deinstall":
                    this._state.TransitionTo(new Deinstalled());
                    break;
                case "Update":
                    this._state.TransitionTo(new Updated());
                    break;
                case "Lent":
                    this._state.TransitionTo(new Lent());
                    break;
                default:
                    break;
            }
        }
    }

    class Started : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Play"
        };

        public override void ChangeState() {
            this._state.TransitionTo(new Played());
        }
        public override void ChangeState(string newState) {
            this._state.TransitionTo(new Played());
        }
    }

    class Played : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Start","Update","Deinstall"
        };

        public override void ChangeState() {
            this._state.TransitionTo(new Updated());
        }
        public override void ChangeState(string newState) {
            switch (newState) {
                case "Start":
                    if (!this._state.lent) {
                        this._state.TransitionTo(new Started());
                    } else {
                        Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
                    }
                    break;
                case "Update":
                    this._state.TransitionTo(new Updated());
                    break;
                case "Deinstall":
                    this._state.TransitionTo(new Deinstalled());
                    break;
                default:
                    break;
            }
        }
    }

    class Deinstalled : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Install"
        };

        public override void ChangeState() {
            this._state.TransitionTo(new Installed());
        }
        public override void ChangeState(string newState) {
            this._state.TransitionTo(new Installed());
        }
    }

    class Updated : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Start","Deinstall"
        };

        public override void ChangeState() {
            if (!this._state.lent) {
                this._state.TransitionTo(new Started());
            } else {
                Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
            }
        }
        public override void ChangeState(string newState) {
            switch (newState) {
                case "Start":
                    if (!this._state.lent) {
                        this._state.TransitionTo(new Started());
                    } else {
                        Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
                    }
                    break;
                case "Deinstall":
                    this._state.TransitionTo(new Deinstalled());
                    break;
                default:
                    break;
            }
        }
    }

    class Lent : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "RequestBack", "Download", "Install", "Deinstall", "Update"
        };

        public override void ChangeState() {
            if (!this._state.lent) {
                this._state.TransitionTo(new Started());
            } else {
                Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
            }
        }
        public override void ChangeState(string newState) {

            switch (newState) {
                case "RequestBack":

                    if (this._state.GetType() != typeof(Started)) {
                        this._state.lent = false;
                        this._state.lentFrom = null;
                        this._state.lentTo = null;
                        Console.WriteLine(" in if");
                        Console.WriteLine(this._state.stateBeforeLend);
                        Console.WriteLine(typeof(Bought));
                        if (this._state.stateBeforeLend == typeof(Bought)) {
                            this._state.TransitionTo(new Bought());
                        } else if (this._state.stateBeforeLend == typeof(Installed)) {
                            this._state.TransitionTo(new Installed());
                        } else if (this._state.stateBeforeLend == typeof(Downloaded)) {
                            this._state.TransitionTo(new Downloaded());
                        }
                    } else {
                        Console.WriteLine("Game is currently being played.");
                        //maybe use interface INotfiyPropertyChanged
                        //this._state.PropertyChanged += listener.DependentObjectChangeHandler;
                        //transition back
                    }
                    break;
                case "Download":
                    this._state.TransitionTo(new Downloaded());
                    break;
                case "Install":
                    this._state.TransitionTo(new Installed());
                    break;
                case "Deinstall":
                    this._state.TransitionTo(new Deinstalled());
                    break;
                case "Update":
                    this._state.TransitionTo(new Updated());
                    break;
                default:
                    break;
            }
        }
    }

    class Lend : State {
        public override List<string> AvailableActions { get; set; } = new List<string>() {
            "Start","Return"
        };

        public override void ChangeState() {
            if (!this._state.lent) {
                this._state.TransitionTo(new Started());
            } else {
                Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
            }
        }
        public override void ChangeState(string newState) {
            switch (newState) {
                case "Start":
                    if (!this._state.lent) {
                        this._state.TransitionTo(new Started());
                    } else {
                        Console.WriteLine($"Sorry, game is currently lent to {this._state.lentTo}");
                    }
                    break;
                case "Return":
                    this._state.lent = false;
                    this._state.lentFrom = null;
                    this._state.lentTo = null;
                    this._state.TransitionTo(new Created());
                    break;
                default:
                    break;
            }
        }
    }
}

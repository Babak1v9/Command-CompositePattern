using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public class Game {


        public State _state = null; // A reference to the current state of the Game.
        public string name;
        public string description;
        public bool lent;
        public string lentTo;
        public string lentFrom;
        public System.Type stateBeforeLend;

        public Game(State state, string name, string description) {

            this.name = name;
            this.description = description;
            Console.WriteLine($"Game {this.name} initialized.");
            this.TransitionTo(state);
        }

        // allows changing the State object at runtime.
        public void TransitionTo(State state) {
            Console.WriteLine($"Game {this.name} Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // delegate part of behavior to the current State object
        public void ChangeState() {
            this._state.ChangeState();
        }
        public void ChangeState(string newState) {
            this._state.ChangeState(newState);
        }

        public void GetGameInfo() {
            Console.WriteLine($"Name: {this.name}{Environment.NewLine}Description: {this.description} {Environment.NewLine}");
        }

        public List<string> GetAvailableActions() {
            return this._state.AvailableActions;
        }
    }
}

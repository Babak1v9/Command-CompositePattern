using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public abstract class State {
        public Game _state;

        public void SetContext(Game game) {
            this._state = game;
        }

        public abstract void ChangeState();
        public abstract void ChangeState(string newState);

        public abstract List<string> AvailableActions { get; set; }
    }
}

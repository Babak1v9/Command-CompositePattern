using System;
using System.Collections.Generic;
using System.Text;

namespace Command_and_Composite {
    public interface ICommand { //declares an interface for the execution of an operation

        public abstract void Execute();
        public abstract void UnExecute();

    }
}

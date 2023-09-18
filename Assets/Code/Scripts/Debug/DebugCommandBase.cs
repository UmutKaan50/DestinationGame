using System;


namespace Destination.Debug { 
    public class DebugCommandBase {
        string commandId;
        string commandDescription;
        string commandFormat;

        public string CommandId {
            get { return commandId; }
        }

        public string CommandDescription {
            get { return commandDescription; }
        }

        public string CommandFormat {
            get { return commandFormat; }
        }

        public DebugCommandBase(string commandId, string commandDescription, string commandFormat) {
            this.commandId = commandId;
            this.commandDescription = commandDescription;
            this.commandFormat = commandFormat;
        }
    }

    public class DebugCommand : DebugCommandBase {
        Action Command;

        public DebugCommand(string id, string description, string format, Action command) : base(id, description,
            format) {
            this.Command = command;
        }

        public void InvokeCommand() {
            Command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase {
        Action<T1> Command;

        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description,
            format) {
            this.Command = command;
        }

        public void InvokeCommand(T1 value) {
            Command.Invoke(value);
        }
    }
}
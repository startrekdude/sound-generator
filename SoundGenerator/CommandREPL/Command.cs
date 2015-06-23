using System;

namespace SoundGenerator.CommandREPL
{
    public struct Command
    {
        private CommandDelegate _method;
        private String _description;
        private Object[] _data;

        public CommandDelegate Method { get { return _method; } }
        public String Description { get { return _description; } set { _description = value; } }
        public Object[] Data { get { return _data; } set { _data = value; } }

        public Command(CommandDelegate method, String description, Object[] data)
        {
            _description = description;
            _data = data;
            _method = method;
        }
    }
}

using System;
using System.IO;
using System.Text;

namespace Gibbo.Engine.Windows
{
    class MyTextWriterArgs : EventArgs
    {
        public String Text { get; set; }
    }

    class MyTextWriter : TextWriter
    {
        public event EventHandler ConsoleOutput;
        private string curLine = string.Empty;

        public override void Write(char value)
        {
            base.Write(value);

            curLine += value.ToString();

            if (value.Equals('\n') || value.Equals('\r') || value.ToString().Equals(Environment.NewLine))
            {
                if (ConsoleOutput != null)
                {
                    // Call the Event
                    ConsoleOutput(this, new MyTextWriterArgs() { Text = curLine });
                }

                curLine = string.Empty;
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}

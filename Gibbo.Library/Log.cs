using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Library
{
    public static class Log
    {
        #region fields

        private static Queue<string> messages = new Queue<string>();
        private static int outputTimeout = 1000;
        private static float outputYield = 0;

        #endregion

        #region properties

        /// <summary>
        /// Gets or Sets the Output Timeout. Default: 1000 = 1 second
        /// </summary>
        public static int OutputTimeout
        {
            get { return outputTimeout; }
            set { outputTimeout = value; }
        }

        #endregion

        #region methods

        public static void WriteLine(string message, bool immediate = false) {
            message += Environment.NewLine;
            Write(message, immediate);
        }

        public static void Write(string message, bool immediate = false)
        {
            if (!immediate)
                messages.Enqueue(message);
            else
                Console.Write(message);
        }

        internal static void Update(GameTime gameTime)
        {
            outputYield -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (outputYield <= 0)
            {
                outputYield = outputTimeout;
                while (messages.Count != 0)
                {
                    Console.Write(messages.Dequeue());
                }
            }
        }

        #endregion
    }
}

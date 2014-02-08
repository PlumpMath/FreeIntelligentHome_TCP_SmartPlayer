using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCP_SmartPlayer
{
    /** Singelton **/
    public class CmdConsole
    {
        private CmdConsole() { }

        private static CmdConsole uniqueInstance;
        private Queue<string> consoleMessagesQueue = new Queue<string>();

        public static CmdConsole instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CmdConsole();
            return uniqueInstance;
        }

        public void newMessages(string consoleMessage)
        {
            if (consoleMessage != null && consoleMessage != "")
                 consoleMessagesQueue.Enqueue(consoleMessage);
        }
        public string getMessage()
        {
            if (consoleMessagesQueue.Count > 0)
            {
                return consoleMessagesQueue.Dequeue();
            }
            else
                return null;
        }
    }
}

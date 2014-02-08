using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TCP_SmartPlayer
{
    static class Program
    {
        private static NotifyIcon notico;
        public static Form theForm = new Form1();

        private static TcpServer tcpServer = new TcpServer();
        private static MessageFilter filter;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        public static void Main()
        {

            //Message filter to get keyboard presses
            filter = new MessageFilter();

            ContextMenu cm;
            MenuItem miCurr;
            int iIndex = 0;

            // Kontextmenü erzeugen
            cm = new ContextMenu();

            // Kontextmenüeinträge erzeuchen
            miCurr = new MenuItem();
            miCurr.Index = iIndex++;
            miCurr.Text = "&Beenden";
            miCurr.Click += new System.EventHandler(ExitClick);
            cm.MenuItems.Add(miCurr);

             // NotifyIcon selbst erzeugen
             notico = new NotifyIcon ();
             notico.Icon = new Icon("tray_ico.ico"); // Eigenes Icon einsetzen
             notico.Text = "Doppelklick mich!";   // Eigenen Text einsetzen
             notico.Visible = true;
             notico.ContextMenu = cm;
             notico.DoubleClick += new EventHandler (NotifyIconDoubleClick);

            //Message Filter
            Application.AddMessageFilter(filter);

            // Ohne Appplication.Run geht es nicht
            Application.EnableVisualStyles();
            Application.Run();
        }

        //==========================================================================
        private static void ExitClick(Object sender, EventArgs e)
        {
            closeProgram();
        }

        //==========================================================================
        private static void Action1Click(Object sender, EventArgs e)
        {
            //Starte form
            if (theForm.IsDisposed)
                theForm = new Form1();
            theForm.Focus();
            theForm.Show();
        }

        //==========================================================================
        private static void NotifyIconDoubleClick(Object sender, EventArgs e)
        {
            //Starte form
            if (theForm.IsDisposed)
                theForm = new Form1();
            theForm.Show();
            theForm.Focus();
        }

        public static void closeProgram()
        {
            Application.RemoveMessageFilter(filter);
            tcpServer.stopServer();
            notico.Dispose();
            Application.Exit();
        }
    }

    public class MessageFilter : IMessageFilter
    {
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public bool PreFilterMessage(ref Message m)
        {
            Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;
            if (m.Msg == WM_KEYDOWN)
            {
                CmdConsole.instance().newMessages(keyCode.ToString());
                if (keyCode.ToString() == "V")
                    return false;   //weitergeben der Taste
                return true;   //Nicht weitergeben der Taste
            }
            if (m.Msg == WM_KEYUP)
            {
                return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;

namespace TCP_SmartPlayer
{
    public class TcpServer
    {
        // Der Listener
        private static TcpListener listener = null;
        // Die Liste der laufenden Server-Threads
        private static ArrayList threads = new ArrayList();
        CmdConsole cmd = CmdConsole.instance();
        Thread th;

        public TcpServer()
        {
            // Listener initialisieren und starten
            //listener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 6040);
            listener = new TcpListener(6040);
            listener.Start();
            // Haupt-Server-Thread initialisieren und starten
            th = new Thread(new ThreadStart(Run));
            th.IsBackground = true;
            th.Start();
        }

        public void stopServer()
        {
            // Alle Server-Threads stoppen
            for (IEnumerator e = threads.GetEnumerator(); e.MoveNext(); )
            {
                // Nächsten Server-Thread holen
                ServerThread st = (ServerThread)e.Current;
                // und stoppen
                st.Stop();

                while (st.running)
                {
                    Thread.Sleep(1000);                 
                }
            }

            // Haupt-Server-Thread stoppen
            stopServerThread();
            //th.Abort();

            // Listener stoppen
            listener.Stop();
        }

        // Hauptthread des Servers
        // Nimmt die Verbindungswünsche von Clients entgegen
        // und startet die Server-Threads für die Clients
        static bool serverRunning = true;
        public static void Run()
        {
            while (serverRunning)
            {
                try
                {
                    // Wartet auf eingehenden Verbindungswunsch
                    TcpClient c = listener.AcceptTcpClient();
                    // Initialisiert und startet einen Server-Thread
                    // und fügt ihn zur Liste der Server-Threads hinzu
                    ServerThread tmpThread = new ServerThread(c);
                    threads.Add(tmpThread);
                }
                catch (Exception)
                {
                }
            }
        }
        public static void stopServerThread()
        {
            serverRunning = false;
        }
    }

    class ServerThread
    {
        // Stop-Flag
        public bool stop = false;
        // Flag für "Thread läuft"
        public bool running = false;
        // Die Verbindung zum Client
        private TcpClient connection = null;
        // Speichert die Verbindung zum Client und startet den Thread
        public ServerThread(TcpClient connection)
        {
            // Speichert die Verbindung zu Client,
            // um sie später schließen zu können
            this.connection = connection;
            // Initialisiert und startet den Thread IsBackgroundThread
            Thread tmp = new Thread(new ThreadStart(Run));
            //tmp.IsBackground = true;
            tmp.Start();
        }
        // Der eigentliche ThreaStreamReader streamReaderd
        StreamReader streamReader;
        Stream theStream;
        public void Run()
        {
            // Setze Flag für "Thread läuft"
            this.running = true;
            // Hole den Stream für's schreiben
            theStream = this.connection.GetStream();

            // Stream zum lesen holen
            streamReader = new StreamReader(theStream);
            while (!stop)
            {
                try
                {
                    // Hole die Daten ab und schreibe sie in die "Console" 
                    String data = streamReader.ReadLine();
                    if(data!=null && data != "")
                        CmdConsole.instance().newMessages(data);
                }
                catch (Exception)
                {
                    // bis ein Fehler aufgetreten ist
                    stop = true;
                }
            }
            // Setze das Flag "Thread läuft" zurück
            this.running = false;
        }
        public void Stop()
        {
            stop = true;
            // Schließe die Verbindung zum Client
            try
            {
                if(streamReader!=null)
                 this.streamReader.Close();
                if(theStream!=null)
                 this.theStream.Close();
                if(connection!=null)
                    this.connection.Close();
            }
            catch (Exception) { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Org.Mentalis.Utilities;

namespace TCP_SmartPlayer
{
    public partial class Form1 : Form
    {
        CmdConsole cmd = CmdConsole.instance();

        //State of vlc
        bool vlcIsFullsrcreen = false;

        //Shutdown after track
        bool shutdownAfterTrack = false;

        public Form1()
        {
            InitializeComponent();
        }

        private bool formJustStarted = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Set to HDMI Audio-Device
            System.Diagnostics.Process.Start("EndPointController.exe", "0");

            //Set Checkbox for shutdown or hibernate
            if (Properties.Settings.Default.ShutDown_Hib)
            {
                chkShutdownHib.Checked = true;
            }

            //Check download dir
            while (!Directory.Exists(Properties.Settings.Default.DownloadDir))
            {
                FolderBrowserDialog theDialog = new FolderBrowserDialog();
                theDialog.Description = "Wähle Download Ordner";
                if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.DownloadDir = theDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }

            //Check archiv dir
            while (!Directory.Exists(Properties.Settings.Default.ArchivDir))
            {
                FolderBrowserDialog theDialog = new FolderBrowserDialog();
                theDialog.Description = "Wähle Archiv Ordner";
                if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.ArchivDir = theDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
            updatePlayable();
            formJustStarted = true;
        }

        private void addPlayableToPlaylist()
        {
            axVLC.playlistClear();
            foreach (object tmp in lstPlayable.Items)
            {
                playableData tmpPlayable = (playableData)tmp;
                var uri = new Uri(tmpPlayable.getFilePath());
                axVLC.addTarget(uri.AbsoluteUri, null, AXVLC.VLCPlaylistMode.VLCPlayListAppend, -1);
            }
        }

        public void updatePlayable()
        {
            //Save selected to set after refresh
            object tmpSelectedItem = null;
            if (lstPlayable.SelectedItem != null)
            {
                tmpSelectedItem = lstPlayable.SelectedItem;
            }

            //For multiDirectories show just one playable not all single files, for this save the added rootDirs
            LinkedList<string> addedRootDirs = new LinkedList<string>();

            //Read all playable files in download directory
            String[] mkvFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir, "*.mkv", System.IO.SearchOption.AllDirectories);
            String[] aviFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir, "*.avi", System.IO.SearchOption.AllDirectories);
            lstPlayable.Items.Clear();

            foreach (String tmp in mkvFiles)
            {
                playableData tmpPlayable = new playableData(tmp);
                //Kleinere Dateien als 100MB ignorieren, dies sind sehr wahrscheinlich nur samples
                if (tmpPlayable.getFileSizeMb() >= 100)
                {
                    //Root dir handling, nur ein root dir hinzufügen bei multiDirs
                    if(tmpPlayable.getMultiDir())
                    {
                        if (!addedRootDirs.Contains(tmpPlayable.getRootDir()))
                        {
                            tmpPlayable.setIsRootDir(true);
                            addedRootDirs.AddLast(tmpPlayable.getRootDir());
                            lstPlayable.Items.Add((object)tmpPlayable);
                        }
                    }
                    else
                        lstPlayable.Items.Add((object)tmpPlayable);
                }
            }
            foreach (String tmp in aviFiles)
            {
                playableData tmpPlayable = new playableData(tmp);
                //Kleinere Dateien als 100MB ignorieren, dies sind sehr wahrscheinlich nur samples
                if (tmpPlayable.getFileSizeMb() >= 100)
                {
                    //Root dir handling, nur ein root dir hinzufügen bei multiDirs
                    if (tmpPlayable.getMultiDir())
                    {
                        if (!addedRootDirs.Contains(tmpPlayable.getRootDir()))
                        {
                            tmpPlayable.setIsRootDir(true);
                            addedRootDirs.AddLast(tmpPlayable.getRootDir());
                            lstPlayable.Items.Add((object)tmpPlayable);
                        }
                    }
                    else
                        lstPlayable.Items.Add((object)tmpPlayable);
                }
            }

            //Zweiten optionalen Ordner mit abprüfen
            if (Directory.Exists(Properties.Settings.Default.OptDir))
            {
                //Read all playable files in download directory
                String[] mkvFiles2 = System.IO.Directory.GetFiles(Properties.Settings.Default.OptDir, "*.mkv", System.IO.SearchOption.AllDirectories);
                String[] aviFiles2 = System.IO.Directory.GetFiles(Properties.Settings.Default.OptDir, "*.avi", System.IO.SearchOption.AllDirectories);

                foreach (String tmp in mkvFiles2)
                {
                    playableData tmpPlayable = new playableData(tmp);
                    //Kleinere Dateien als 100MB ignorieren, dies sind sehr wahrscheinlich nur samples
                    if (tmpPlayable.getFileSizeMb() >= 100)
                    {
                        //Root dir handling, nur ein root dir hinzufügen bei multiDirs
                        if (tmpPlayable.getMultiDir())
                        {
                            if (!addedRootDirs.Contains(tmpPlayable.getRootDir()))
                            {
                                tmpPlayable.setIsRootDir(true);
                                addedRootDirs.AddLast(tmpPlayable.getRootDir());
                                lstPlayable.Items.Add((object)tmpPlayable);
                            }
                        }
                        else
                            lstPlayable.Items.Add((object)tmpPlayable);
                    }
                }
                foreach (String tmp in aviFiles2)
                {
                    playableData tmpPlayable = new playableData(tmp);
                    //Kleinere Dateien als 100MB ignorieren, dies sind sehr wahrscheinlich nur samples
                    if (tmpPlayable.getFileSizeMb() >= 100)
                    {
                        //Root dir handling, nur ein root dir hinzufügen bei multiDirs
                        if (tmpPlayable.getMultiDir())
                        {
                            if (!addedRootDirs.Contains(tmpPlayable.getRootDir()))
                            {
                                tmpPlayable.setIsRootDir(true);
                                addedRootDirs.AddLast(tmpPlayable.getRootDir());
                                lstPlayable.Items.Add((object)tmpPlayable);
                            }
                        }
                        else
                            lstPlayable.Items.Add((object)tmpPlayable);
                    }
                }
            }

            lstPlayable.Refresh();

            //Set to one item
            if (tmpSelectedItem != null)
                lstPlayable.SelectedItem = tmpSelectedItem;
            else if (lstPlayable.Items.Count >= 1)
            {
                lstPlayable.SelectedIndex = 0;
            }

        }

        //Öffne Datei
        private void button1_Click(object sender, EventArgs e)
        {
            string mrl = String.Empty;
            OpenFileDialog theDialog = new OpenFileDialog();
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                playableData tmp = new playableData(theDialog.FileName);
                lstPlayable.Items.Add(tmp);
                lstPlayable.SelectedItem = tmp;
            }
        }

        private void playSelected()
        {
            if(lstPlayable.Items.Count>=1)
            {
                bool newMessage = false;
                axVLC.pause();
                fullscreenSet(false);
                if (lstPlayable.SelectedItem == null || lstPlayable.SelectedIndex == -1)
                    lstPlayable.SelectedIndex = 0;
                //Wait 2,5 seconds for a next message otheriwse play it
                for (int i = 0; i < 50; i++)
                {
                    newMessage = getTcpMessages();
                    if (!newMessage)
                        Thread.Sleep(50);
                    else
                        break;
                }

                if (!newMessage)
                {
                    string mrl = ((playableData)(lstPlayable.SelectedItem)).getFilePath();
                    var uri = new Uri(mrl);
                    axVLC.playlistClear();
                    axVLC.addTarget(uri.AbsoluteUri, null, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, -1);
                    //try   //Bug im activeX vlc plugin
                    //{
                    //    object tmp = axVLC.getVariable("key-subtitle-track");
                    //    axVLC.setVariable(@"1", tmp);
                    //}
                    //catch (Exception)
                    //{

                    //}
                    fullscreenSet(true);
                    axVLC.play();
                }
            }
        }

        int countRefreshGUI = 0;
        //alle 10ms
        private void timerCmd_Tick(object sender, EventArgs e)
        {
            getTcpMessages();
            if (countRefreshGUI == 100)
            {
                countRefreshGUI = 0;
                tmpLabel.Text = ((axVLC.Length / 60000) * axVLC.Position).ToString() + " Minuten";
            }
            countRefreshGUI++;

            //Do Actions after played a complete track
            if (axVLC.Position >= 0.9999243f)
            {
                axVLC.stop();

                //Shutdown if activated
                if (shutdownAfterTrack)
                {
                    shutdownAfterTrack = false;
                    if (chkShutdownHib.Checked)
                        WindowsController.ExitWindows(RestartOptions.ShutDown, false);      //Herunterfahren
                    else
                        WindowsController.ExitWindows(RestartOptions.Suspend, false);       //Schlaffmodus
                }
                else //play next track
                {
                    doActions("Next");
                }
            }
        }

        private void archiveTrack()
        {
            //Stop vlc
            axVLC.stop();

            //Copy then delete directory or file from download dir to archive dir
            playableData tmpData = (playableData)lstPlayable.SelectedItem;
            if (tmpData != null)
            {
                tmpData.archiveFile();
            }
            updatePlayable();
        }

        private bool getTcpMessages()
        {
            string tmpMsg = cmd.getMessage();
            if (tmpMsg != null)
            {
                lstCmd.Items.Add("Empfangen: " + tmpMsg);
                doActions(tmpMsg);
                return true;
            }
            return false;
        }

        private void startForm()
        {
            //Starte form
            Program.theForm.Show();
            Program.theForm.Focus();
            Program.theForm.WindowState = FormWindowState.Normal;
            this.Refresh();
        }

        private void fullscreenSet(bool state)
        {
            if (state == vlcIsFullsrcreen)
                return;
            else
            {
                fullscreenToggle();
                vlcIsFullsrcreen = state;
            }
        }

        private void fullscreenToggle()
        {
            vlcIsFullsrcreen = !vlcIsFullsrcreen;
            axVLC.fullscreen();
        }


        //Actions direkt vom Nutzer über Buttons oder TCP Verbindung
        private void doActions(string theMsg)
        {
            switch (theMsg)
            {
                case "Lauter":
                    axVLC.Volume = axVLC.Volume + 10;
                    break;
                case "Leiser":
                    axVLC.Volume = axVLC.Volume - 10;
                    break;
                case "Space":
                case "PlayPause":
                case "MediaPlayPause":
                    startForm();
                    if (lstPlayable.SelectedIndex == -1)
                        updatePlayable();
                    if (axVLC.Playing)
                        axVLC.pause();
                    else
                    {
                        if (formJustStarted)
                        {
                            formJustStarted = false;
                            playSelected();
                        }
                        else
                        {
                            axVLC.play();
                        }
                    }
                    break;
                case "Right":
                case "Next":
                    startForm();
                    if (lstPlayable.SelectedIndex == -1)
                        updatePlayable();
                    //Check for empty list
                    if (lstPlayable.Items.Count != 0)
                    {
                        //Select next index or first one when it was the last one
                        if (lstPlayable.SelectedIndex == lstPlayable.Items.Count - 1)
                            lstPlayable.SelectedIndex = 0;
                        else
                            lstPlayable.SelectedIndex++;
                        lstPlayable.SetSelected(lstPlayable.SelectedIndex, true);
                    }
                    break;
                case "Left":
                case "Prev":
                    startForm();
                    if (lstPlayable.SelectedIndex == -1)
                        updatePlayable();
                    //Check for empty list
                    if (lstPlayable.Items.Count != 0)
                    {
                        //Select prev index or last one when it was the first one
                        if (lstPlayable.SelectedIndex == 0)
                            lstPlayable.SelectedIndex = lstPlayable.Items.Count - 1;
                        else
                            lstPlayable.SelectedIndex--;
                        lstPlayable.SetSelected(lstPlayable.SelectedIndex, true);
                    }
                    break;
                case "Escape":
                case "ToggleFullscreen":
                    startForm();
                    fullscreenToggle();
                    break;
                case "FullscreenOff":
                    fullscreenSet(false);
                    break;
                case "FullscreenOn":
                    fullscreenSet(true);
                    break;
                case "SchrittVor":
                    if (axVLC.Playing)
                    {
                        int tmp = axVLC.Length;                     //Länge des aktuellen tracks in ms
                        float step = (3 * 60000) / (float)tmp;      //Ein schritt X minuten in % f
                        axVLC.Position += step;
                    }
                    break;
                case "SchrittZurueck":
                    if (axVLC.Playing)
                    {
                        int tmp = axVLC.Length;                     //Länge des aktuellen tracks in ms
                        float step = (3 * 60000) / (float)tmp;      //Ein schritt X (3) minuten in % f
                        axVLC.Position -= step;
                    }
                    break;
                case "ToggleShutdown":
                    bool tmpWasPlaying = false;
                    fullscreenSet(false);
                    if(axVLC.Playing)
                    {
                        tmpWasPlaying=true;
                        axVLC.pause();
                    }
                    shutdownAfterTrack = !shutdownAfterTrack;
                    if (shutdownAfterTrack)
                        lblShutdownState.Text = "Aktiviert";
                    else
                        lblShutdownState.Text = "Deaktiviert";
                    lblShutdownState.Refresh();
                    Thread.Sleep(3000);
                    if (tmpWasPlaying)
                        axVLC.play();
                    fullscreenSet(true);
                    break;
                case "archivieren":
                    //Nur wenn vlc nicht im Vollbildmodus und nicht während dem Abspielen
                    if (!vlcIsFullsrcreen && !axVLC.Playing)
                    {
                        if(lstPlayable.SelectedIndex!=-1)
                        {
                            archiveTrack();
                            updatePlayable();
                        }
                    }
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Set AudioDevice back to USB Headset
            System.Diagnostics.Process.Start("EndPointController.exe", "1");
            Program.closeProgram();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState.ToString() == "Minimized")
            {
                Program.theForm.Hide();
                axVLC.pause();
                //Dispose();      //Close to tray
            }
        }

        private void cmdChooseDownloadDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();
            theDialog.Description = "Wähle Download Ordner";
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.DownloadDir = theDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
            theDialog.Dispose();
            updatePlayable();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            doActions("Next");
        }

        private void cmdPlayPause_Click(object sender, EventArgs e)
        {
            doActions("PlayPause");
        }

        private void cmdLauter_Click(object sender, EventArgs e)
        {
            doActions("Lauter");
        }

        private void cmdLeiser_Click(object sender, EventArgs e)
        {
            doActions("Leiser");
        }

        private void cmdSchrittVor_Click(object sender, EventArgs e)
        {
            doActions("SchrittVor");
        }

        private void cmdSchrittZurueck_Click(object sender, EventArgs e)
        {
            doActions("SchrittZurueck");
        }

        private void cmdChooseOptDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();
            theDialog.Description = "Wähle optionalen Medien Ordner";
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.OptDir = theDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
            theDialog.Dispose();
            updatePlayable();
        }

        private void chkShutdownHib_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShutdownHib.Checked)
            {
                if (!Properties.Settings.Default.ShutDown_Hib)
                {
                    Properties.Settings.Default.ShutDown_Hib = true;
                    Properties.Settings.Default.Save();
                }
            }
            else if (Properties.Settings.Default.ShutDown_Hib)
            {
                Properties.Settings.Default.ShutDown_Hib = false;
                Properties.Settings.Default.Save();
            }

        }

        private void cmdToggleShutdown_Click(object sender, EventArgs e)
        {
            doActions("ToggleShutdown");
        }

        private void cmdChooseArchiveDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();
            theDialog.Description = "Wähle Archiv Ordner";
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.ArchivDir = theDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
            theDialog.Dispose();
        }

        private void lstPlayable_DoubleClick(object sender, EventArgs e)
        {
            playSelected();
        }

        private void cmdArchivieren_Click(object sender, EventArgs e)
        {
            doActions("archivieren");
        }
    }
}

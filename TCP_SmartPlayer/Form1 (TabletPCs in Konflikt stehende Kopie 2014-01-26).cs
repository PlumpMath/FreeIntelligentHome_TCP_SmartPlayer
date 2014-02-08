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

namespace Tastatureingaben
{
    public partial class Form1 : Form
    {
        CmdConsole cmd = CmdConsole.instance();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Check download dir
            while (!Directory.Exists(Properties.Settings.Default.DownloadDir))
            {
                FolderBrowserDialog theDialog = new FolderBrowserDialog();
                if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.DownloadDir = theDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
            updatePlayable();
            if (lstPlayable.SelectedIndex == -1)
                lstPlayable.SelectedIndex = 0;
            addPlayable(lstPlayable.SelectedItem);
            showTheNextPlayingItem();
           // axVLC.play();
            doActions("ToggleFullscreen");
        }

        private void addPlayable(object theItem)
        {
            playableData tmpPlayable = (playableData)theItem;
            var uri = new Uri(tmpPlayable.getFilePath());
            axVLC.addTarget(uri.AbsoluteUri, null, AXVLC.VLCPlaylistMode.VLCPlayListAppend, -1);
        }

        private void showTheNextPlayingItem()
        {
            lbl_actual_playing.Text = ((playableData)(lstPlayable.SelectedItem)).ToString();
            Thread.Sleep(2000);
        }

        public void updatePlayable()
        {

            //Read all playable files in download directory
            String[] mkvFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir, "*.mkv", System.IO.SearchOption.AllDirectories);
            String[] aviFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir, "*.avi", System.IO.SearchOption.AllDirectories);
            lstPlayable.Items.Clear();
            foreach (String tmp in mkvFiles)
            {
                playableData tmpPlayable = new playableData(tmp);
                lstPlayable.Items.Add((object)tmpPlayable);
            }
            foreach (String tmp in aviFiles)
            {
                playableData tmpPlayable = new playableData(tmp);
                lstPlayable.Items.Add((object)tmpPlayable);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mrl = String.Empty;
            OpenFileDialog theDialog = new OpenFileDialog();
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mrl = theDialog.FileName;
                var uri = new Uri(mrl);
                axVLC.addTarget(uri.AbsoluteUri, null, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, -1);
                doActions("ToggleFullscreen");
                axVLC.play();
            }
        }

        private void playFilePath(string theFilePath)
        {
            string mrl = theFilePath;
            var uri = new Uri(mrl);
            axVLC.playlistClear();
            axVLC.addTarget(uri.AbsoluteUri, null, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, -1);
            axVLC.play();
            doActions("ToggleFullscreen");
        }

        private void timerCmd_Tick(object sender, EventArgs e)
        {
            string tmpMsg = cmd.getMessage();
            if (tmpMsg != null)
            {
                lstCmd.Items.Add("Empfangen: " + tmpMsg);
                doActions(tmpMsg);
            }
        }

        private void doActions(string theMsg)
        {
            switch (theMsg)
            {
                // pause or play vlc
                case "Space":
                case "PlayPause":
                case "MediaPlayPause":
                    if (axVLC.Playing)
                        axVLC.pause();
                    else
                        axVLC.play();
                    break;
                case "Right":
                case "Next":
                    axVLC.playlistNext();
                    break;
                case "Left":
                case "Prev":
                    axVLC.playlistPrev();
                    break;
                case "Escape":
                case "ToggleFullscreen":
                    axVLC.fullscreen();
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.closeProgram();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState.ToString() == "Minimized")
            {
                Dispose();      //Close to tray
            }
        }

        private void cmdChooseDownloadDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.DownloadDir = theDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
            theDialog.Dispose();

        }

        private void lstPlayable_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlayable.SelectedIndex != -1)
            {
                playableData tmp = (playableData)lstPlayable.SelectedItem;
                playFilePath(tmp.getFilePath());
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            axVLC.playlistNext();
        }

        private void cmdPlayPause_Click(object sender, EventArgs e)
        {
            doActions("PlayPause");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            doActions(e.KeyCode.ToString());
        }
    }
}

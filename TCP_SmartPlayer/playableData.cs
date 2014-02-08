using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TCP_SmartPlayer
{
    public class playableData
    {
        private string filePath = null;
        private string fileName = null;
        private FileInfo fileInfo = null;
        private bool isMultiDirectory = false;
        private string downloadRootDir = null;
        private bool isJustARootForMultiDir = false;

        public playableData(string theFilePath)
        {
            this.fileName = Path.GetFileName(theFilePath);
            this.filePath = theFilePath;
            fileInfo = new FileInfo(theFilePath);

            //Check for other files in same directory to avoid archive all files in once
            string[] tmpFilePath = (this.filePath.Split('\\'));
            string[] tmpDir = Properties.Settings.Default.DownloadDir.Split('\\');
            int i = 0;
            //Überspringe Download dir
            while (tmpFilePath[i] == tmpDir[i])
            {
                i++;
                if (tmpDir.Length == i)
                    break;
            }

            //Dont check for multiDir if there is no directory to file in download dir
            if ((tmpFilePath.Length-1) != i)
            {
                //Save root directory
                downloadRootDir = tmpFilePath[i];

                //Read all playable files in actual directory
                String[] mkvFiles2 = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir + "//" + tmpFilePath[i], "*.mkv", System.IO.SearchOption.AllDirectories);
                String[] aviFiles2 = System.IO.Directory.GetFiles(Properties.Settings.Default.DownloadDir + "//" + tmpFilePath[i], "*.avi", System.IO.SearchOption.AllDirectories);
                if (aviFiles2.Length > 1 || mkvFiles2.Length > 1)
                {
                    isMultiDirectory = true;
                }
            }
        }

        public override string ToString()
        {
            string[] tmp = (this.filePath.Split('\\'));
            string[] tmpDir = Properties.Settings.Default.DownloadDir.Split('\\');
            int i=0;
            //Überspringe Download dir
            while (tmp[i] == tmpDir[i])
            {
                i++;
                if (tmpDir.Length == i)
                    break;
            }
            System.Globalization.NumberFormatInfo numberFormatInfo = new System.Globalization.NumberFormatInfo();
            numberFormatInfo.NumberDecimalDigits = 1;
            string tmpOutput=null;
            if(isJustARootForMultiDir)
                tmpOutput = tmp[i];
            else
                tmpOutput = tmp[i] + "\\" + fileName;
            //Filter bestimmt Wörter die sehr oft vorkommen einfach raus
            tmpOutput = tmpOutput.Replace("info", "");
            tmpOutput = tmpOutput.Replace("DVD", "");
            tmpOutput = tmpOutput.Replace("revolution.", "");
            tmpOutput = tmpOutput.Replace("Revolution", "");
            tmpOutput = tmpOutput.Replace(".", " ");
            tmpOutput = tmpOutput.Replace("-", " ");
            tmpOutput = tmpOutput.Replace("  ", " ");
            tmpOutput = tmpOutput.Replace("Usenet", "");
            tmpOutput = tmpOutput.Replace("1080p", "");
            tmpOutput = tmpOutput.Replace("1080", "");
            tmpOutput = tmpOutput.Replace("720p", "");
            tmpOutput = tmpOutput.Replace("720", "");
            tmpOutput = tmpOutput.Replace("TV", "");
            tmpOutput = tmpOutput.Replace("AC3", "");
            tmpOutput = tmpOutput.Replace("WS", "");
            tmpOutput = tmpOutput.Replace("RIP", "");
            tmpOutput = tmpOutput.Replace("DTS", "");
            tmpOutput = tmpOutput.Replace("XVID", "");
            tmpOutput = tmpOutput.Replace("MKV", "");
            tmpOutput = tmpOutput.Replace("Blue", "");
            tmpOutput = tmpOutput.Replace("Ray", "");
            tmpOutput = tmpOutput.Replace("x264", "");
            tmpOutput = tmpOutput.Replace("LeetHD", "");
            tmpOutput = tmpOutput.Replace("German", "");
            tmpOutput = tmpOutput.Replace("german", "");
            tmpOutput = tmpOutput.Replace("euhd", "");
            tmpOutput = tmpOutput.Replace("mkv", "");
            tmpOutput = tmpOutput.Replace("English", "");
            tmpOutput = tmpOutput.Replace("english", "");
            tmpOutput = tmpOutput.Replace("EXTENDED", "");
            tmpOutput = tmpOutput.Replace("Blu", "");
            tmpOutput = tmpOutput.Replace("X264", "");
            tmpOutput = tmpOutput.Replace("REWARD", "");
            tmpOutput = tmpOutput.Replace("REPACK", "");
            tmpOutput = tmpOutput.Replace("repack", "");
            tmpOutput = tmpOutput.Replace("HD", "");
            tmpOutput = tmpOutput.Replace("Videomann", "");
            tmpOutput = tmpOutput.Replace("DL", "");
            tmpOutput = tmpOutput.Replace("RiP", "");
            tmpOutput = tmpOutput.Replace("XViD", "");
            tmpOutput = tmpOutput.Replace("nfo", "");
            tmpOutput = tmpOutput.Replace("rhd", "");
            tmpOutput = tmpOutput.Replace("tvs", "");
            tmpOutput = tmpOutput.Replace("dd51", "");
            tmpOutput = tmpOutput.Replace("ded", "");
            tmpOutput = tmpOutput.Replace("dl", "");
            tmpOutput = tmpOutput.Replace("7p", "");
            tmpOutput = tmpOutput.Replace("bd", "");
            tmpOutput = tmpOutput.Replace("eu", "");
            tmpOutput = tmpOutput.Replace("ahs", "");
            tmpOutput = tmpOutput.Replace("ithd", "");
            tmpOutput = tmpOutput.Replace("QRC", "");
            tmpOutput = tmpOutput.Replace("avc", "");
            tmpOutput = tmpOutput.Replace("{{", "");
            tmpOutput = tmpOutput.Replace("}}", "");
            tmpOutput = tmpOutput.Replace("   ", " ");
            tmpOutput = tmpOutput.Replace("  ", " ");

            if(isJustARootForMultiDir)
                return tmpOutput + " - " + "Multi Ordner";
            else
                return tmpOutput + " - " + (getFileSizeMb() / 1024).ToString("F", numberFormatInfo) + " GB";
        }

        public void setIsRootDir(bool isJustARootDir)
        {
            isJustARootForMultiDir = isJustARootDir;
        }
        public bool getMultiDir()
        {
            return isMultiDirectory;
        }

        public string getRootDir()
        {
            return downloadRootDir;
        }

        public bool getIsRootDir()
        {
            return isJustARootForMultiDir;
        }

        public string getFilePath()
        {
            return filePath;
        }

        public string getFileName()
        {
            return fileName;
        }

        public void archiveFile()
        {
            //Root dirs können nicht archiviert werden
            if (!isJustARootForMultiDir)
            {
                //Die Datei wird archiviert, dazu wird der Rootordner in den Archiv Ordner verschoben
                //Sind mehrer Dateien im gleichen ordner verschiebe nicht den gesamten Ordner
                //Es wird nur das niedrigste Verzeichnis, dass keine anderen abspielbaren Dateien enthält verschoben
                //Sind keine anderen Dateien mehr vorhanden wird der Ordner mit allen anderen nicht abspielbaren Dateien/Ordnern verschoben
                string sourceFile = filePath;
                string sourceRootDir = null;
                string destRootDir = null;
                if (downloadRootDir != null)
                {
                    destRootDir = Properties.Settings.Default.ArchivDir + "\\" + downloadRootDir + "\\";
                    sourceRootDir = Properties.Settings.Default.DownloadDir + "\\" + downloadRootDir + "\\";
                }
                string destFile = getDestinationFilePath();

                try
                {
                    //Erster Fall: kein Ordner im Downloadverzweichnis sondern nur einzelne Datei
                    //->Verschiebe diese einzelne Datei in den Archiv Ordner
                    if (sourceRootDir == null)
                    {
                        //Wenn Datei bereits vorhanden, einfach source löschen da Doppelung
                        if (File.Exists(destFile))
                            File.Delete(sourceFile);
                        else
                            File.Move(sourceFile, destFile);
                    }

                    //Zweiter Fall: Kein multiDir und Zieldirectory nicht vorhanden
                    //->Verschiebe den gesamten Ordner ins Archiv mit gesamten Inhalt
                    else if (!isMultiDirectory && !Directory.Exists(destRootDir))
                    {
                        Directory.Move(sourceRootDir, destRootDir);
                    }

                    //Dritter Fall: Kein multiDir aber Zielordner ist bereits vorhanden, es war evtl. ein multiDir
                    //->Prüfe jede Ordner-Ebene bis zur Datei und verschiebe alle vorhandenen Ordner und Dateien
                    else if (!isMultiDirectory && Directory.Exists(destRootDir))
                    {
                        if (mergeDirectory(sourceRootDir, destRootDir))
                        {
                            Directory.Delete(sourceRootDir, true);
                        }
                    }

                    //Vierter Fall: multiDir
                    //->Belasse die anderen abspielbaren Dateien im Ordner aber verschiebe die maximale Ordnerebene mit nur einer abspielbaren Datei
                    else if (isMultiDirectory)
                    {
                        if (!Directory.Exists(destRootDir))
                            Directory.CreateDirectory(destRootDir);
                        string tmpDestFile = destFile.Remove(0, destRootDir.Length);
                        string[] destDirectorys = tmpDestFile.Split('\\');

                        //Verschiebe die Datei da es keine weiteren Ordner gibt
                        if (destDirectorys.Length == 1)
                            File.Move(sourceFile, destFile);
                        //Gehe jeden weiteren Ordner durch und prüfe ob noch andere abspielbare Dateien vorhanden sind
                        //Sobald keine mehr vorhanden sind, verschiebe den Ordner oder Datei
                        else
                        {
                            string actualDestDir = destRootDir;
                            string actualSourceDir = sourceRootDir;

                            for (int i = 0; i < destDirectorys.Length - 1; i++)
                            {
                                //Speicher aktuelle Ordner Ebene ab
                                actualDestDir += destDirectorys[i] + "\\";
                                actualSourceDir += destDirectorys[i] + "\\";

                                //Erstelle Zielordner falls nicht vorhanden
                                if (!Directory.Exists(actualDestDir))
                                    Directory.CreateDirectory(actualDestDir);

                                //Suche andere Dateien
                                String[] mkvFiles = System.IO.Directory.GetFiles(actualDestDir, "*.mkv", System.IO.SearchOption.AllDirectories);
                                String[] aviFiles = System.IO.Directory.GetFiles(actualDestDir, "*.avi", System.IO.SearchOption.AllDirectories);

                                //Sind keine anderen Dateien vorhanden verschiebe den ganzen Ordner
                                if (mkvFiles.Length == 0 && aviFiles.Length == 0)
                                {
                                    if (mergeDirectory(actualSourceDir, actualDestDir))
                                    {
                                        Directory.Delete(actualSourceDir);
                                    }
                                }
                                //Wenn Dateien vorhanden sind aber es keinen weiteren Ordner gibt, verschiebe nur die einzelne Datei
                                else if (i + 2 == destDirectorys.Length)
                                {
                                    File.Move(sourceFile, destFile);
                                }

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    CmdConsole.instance().newMessages("Fehler beim kopieren ins Archiv " + ex.Message);
                }
            }
        }

        // Verschiebe alle Ordner und Dateien in das destDir
        private bool mergeDirectory(string sourceDir, string destDir)
        {
            bool successful = true;
            try
            {
                DirectoryInfo source = new DirectoryInfo(sourceDir);
                DirectoryInfo target = new DirectoryInfo(destDir);

                if (source.FullName.ToLower() == target.FullName.ToLower())
                {
                    return false;
                }

                // Check if the target directory exists, if not, create it.
                if (Directory.Exists(target.FullName) == false)
                {
                    Directory.CreateDirectory(target.FullName);
                }

                // Copy each file into it's new directory.
                foreach (FileInfo fi in source.GetFiles())
                {
                    if(!File.Exists(Path.Combine(target.ToString(), fi.Name)))
                        fi.MoveTo(Path.Combine(target.ToString(), fi.Name));
                }

                // Copy each subdirectory using recursion.
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                    successful = mergeDirectory(sourceDir + diSourceSubDir.ToString() + "\\", nextTargetSubDir.ToString());
                }
            }
            catch (Exception ex)
            {
                CmdConsole.instance().newMessages("Fehler beim verschieben: " + ex.Message);
                successful = false;
            }

            return successful;
        }

        public string getDestinationFilePath()
        {
            string[] tmpFilePath = (this.filePath.Split('\\'));
            string[] tmpDir = Properties.Settings.Default.DownloadDir.Split('\\');

            //return just the filepath to archive because there is no directory to this file in the download directory
            if (tmpFilePath.Length == tmpDir.Length+1)
            {
                return Properties.Settings.Default.ArchivDir + "\\" + fileName;
            }
            else
            {
                int i = 0;
                //Überspringe Download dir
                while (tmpFilePath[i] == tmpDir[i])
                {
                    i++;
                    if (tmpDir.Length == i)
                    {
                        break;
                    }
                }
                //Füge alle Ordner nach dem DownloadDir dem Zielpfad hinzu
                string tmpReturn = Properties.Settings.Default.ArchivDir;
                for (; i < (tmpFilePath.Length); i++)
                {
                
                    tmpReturn = tmpReturn + "\\" + tmpFilePath[i];
                }
                return tmpReturn;
            }
        }

        public double getFileSizeMb()
        {
            double tmpFileSizeMb;
            tmpFileSizeMb = (fileInfo.Length / 1024) / 1024;
            return tmpFileSizeMb;
        }
    }
}

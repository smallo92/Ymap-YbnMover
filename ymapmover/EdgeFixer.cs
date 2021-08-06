using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CodeWalker.GameFiles;
using YmapYbnMover;

namespace ymapmover
{
    public partial class EdgeFixer : Form
    {
        public bool cancelLoop = false;
        public System.Threading.Timer watch = null;
        public DateTime timerTime;
        public long timerStartTime = 0;

        public EdgeFixer()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EdgeFixer_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Types|*.rpf;*.ybn;*.ydr;*.ydd;*.yft;" + "|RPF Files|*.rpf|YBN Files|*.ybn|YDD Files|*.ydd|YDR Files|*.ydr|YFT Files|*.yft";
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
            CurrentList.AllowDrop = true;
            CurrentList.DragDrop += CurrentList_DragDrop;
            CurrentList.DragEnter += CurrentList_DragEnter;
        }

        private void CurrentList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void CurrentList_DragDrop(object sender, DragEventArgs e)
        {
            watch = new System.Threading.Timer(Tick, null, 0, 10);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (file.EndsWith("ybn") || file.EndsWith("ydr") || file.EndsWith("yft") || file.EndsWith("ydd"))
                    {
                        if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    }
                    else if (file.EndsWith("rpf"))
                    {
                        TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                        RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                        try
                        {
                            rpf.ScanStructure(null, null);
                            var fileTypes = new List<string>() { ".ybn", ".ymap" };
                            RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error can't read " + file + ".\nThis file has been skipped.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                watch.Dispose();
            }).Start();
        }

        private void addItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    foreach (string file in openFileDialog1.FileNames)
                    {
                        if (file.EndsWith(".rpf"))
                        {
                            TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                            RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                            try
                            {
                                rpf.ScanStructure(null, null);
                                var fileTypes = new List<string>() { ".ybn", ".ydr", ".yft", ".ydd" };
                                RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error can't read " + file + ".\nThis file has been skipped.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        } else
                        {
                            if (!StringFunctions.DoesItemExist(CurrentList, file))
                            {
                                CurrentList.Items.Add(file);
                            }
                        }
                    }
                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        public void Tick(object info)
        {
            DateTime currentTime = DateTime.Now;
            TimeLabel.Text = $"Time Elapsed: {StringFunctions.ConvertMillisecondsToSeconds((currentTime - timerTime).TotalMilliseconds)}";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timerTime = DateTime.Now;
            watch = new System.Threading.Timer(Tick, null, 0, 10);
            cancelButton.Enabled = true;
            var errorFiles = new List<string>() { };

            new Thread(() =>
            {
                while (cancelLoop)
                {
                    Thread.CurrentThread.IsBackground = true;
                    for (var j = 0; j < CurrentList.Items.Count; j++)
                    {
                        string filename = CurrentList.Items[j].ToString();
                        FilesAddedLabel.Text = "Processing " + Path.GetFileName(filename) + " (" + (j + 1) + " of " + CurrentList.Items.Count + ")";

                        if (filename.EndsWith(".ybn"))
                        {
                            YbnFile ybn = new YbnFile();
                            RpfDirectoryEntry RPFFilesDirectory;
                            byte[] oldData;
                            if (filename.Contains(".rpf"))
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                if (File.Exists(filename))
                                {
                                    RPFFilesDirectory = null;
                                    oldData = File.ReadAllBytes(filename);
                                }
                                else
                                {
                                    RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                                    TopRPF.ScanStructure(null, null);
                                    (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                                }
                            }
                            else
                            {
                                RPFFilesDirectory = null;
                                oldData = File.ReadAllBytes(filename);
                            }
                            try
                            {
                                ybn.Load(oldData);
                                byte[] newData = ybn.Save();
                                if (filename.Contains(".rpf"))
                                {
                                    if (File.Exists(filename))
                                    {
                                        RPFFilesDirectory = null;
                                        oldData = File.ReadAllBytes(filename);
                                    }
                                    else RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                                }
                                else File.WriteAllBytes(filename, newData);
                            }
                            catch (Exception)
                            {
                                errorFiles.Add(filename);
                            }
                        }
                        else if (filename.EndsWith(".ydr"))
                        {
                            YdrFile ydr = new YdrFile();
                            RpfDirectoryEntry RPFFilesDirectory;
                            byte[] oldData;
                            if (filename.Contains(".rpf"))
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                if (File.Exists(filename))
                                {
                                    RPFFilesDirectory = null;
                                    oldData = File.ReadAllBytes(filename);
                                }
                                else
                                {
                                    RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                                    TopRPF.ScanStructure(null, null);
                                    (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                                }
                            }
                            else
                            {
                                RPFFilesDirectory = null;
                                oldData = File.ReadAllBytes(filename);
                            }
                            try
                            {
                                RpfFile.LoadResourceFile(ydr, oldData, 165);
                                byte[] newData = ydr.Save();
                                if (filename.Contains(".rpf"))
                                {
                                    if (File.Exists(filename))
                                    {
                                        RPFFilesDirectory = null;
                                        oldData = File.ReadAllBytes(filename);
                                    }
                                    else RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                                }
                                else File.WriteAllBytes(filename, newData);
                            }
                            catch (Exception)
                            {
                                errorFiles.Add(filename);
                            }
                        }
                        else if (filename.EndsWith(".ydd"))
                        {
                            YddFile ydd = new YddFile();
                            RpfDirectoryEntry RPFFilesDirectory;
                            byte[] oldData;
                            if (filename.Contains(".rpf"))
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                if (File.Exists(filename))
                                {
                                    RPFFilesDirectory = null;
                                    oldData = File.ReadAllBytes(filename);
                                }
                                else
                                {
                                    RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                                    TopRPF.ScanStructure(null, null);
                                    (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                                }
                            }
                            else
                            {
                                RPFFilesDirectory = null;
                                oldData = File.ReadAllBytes(filename);
                            }
                            try
                            {
                                RpfFile.LoadResourceFile(ydd, oldData, 165);
                                byte[] newData = ydd.Save();
                                if (filename.Contains(".rpf"))
                                {
                                    if (File.Exists(filename))
                                    {
                                        RPFFilesDirectory = null;
                                        oldData = File.ReadAllBytes(filename);
                                    }
                                    else RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                                }
                                else File.WriteAllBytes(filename, newData);
                            }
                            catch (Exception)
                            {
                                errorFiles.Add(filename);
                            }
                        }
                        else if (filename.EndsWith(".yft"))
                        {
                            YftFile yft = new YftFile();
                            RpfDirectoryEntry RPFFilesDirectory;
                            byte[] oldData;
                            if (filename.Contains(".rpf"))
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                if (File.Exists(filename))
                                {
                                    RPFFilesDirectory = null;
                                    oldData = File.ReadAllBytes(filename);
                                }
                                else
                                {
                                    RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                                    TopRPF.ScanStructure(null, null);
                                    (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                                }
                            }
                            else
                            {
                                RPFFilesDirectory = null;
                                oldData = File.ReadAllBytes(filename);
                            }
                            try
                            {
                                RpfFile.LoadResourceFile(yft, oldData, 162);
                                byte[] newData = yft.Save();
                                if (filename.Contains(".rpf"))
                                {
                                    if (File.Exists(filename))
                                    {
                                        RPFFilesDirectory = null;
                                        oldData = File.ReadAllBytes(filename);
                                    }
                                    else RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                                }
                                else File.WriteAllBytes(filename, newData);
                            }
                            catch (Exception)
                            {
                                errorFiles.Add(filename);
                            }
                        }
                    }
                    if (errorFiles.Count != 0)
                    {
                        string message = "The following file(s) were corrupted and were not edited.\n\n";
                        foreach (string item in errorFiles) message = message + item + "\n";

                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    FilesAddedLabel.Text = "Complete";
                    cancelButton.Enabled = false;
                    watch.Dispose();
                }
            }).Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelLoop = true;
            watch.Dispose();
            FilesAddedLabel.Text = "Cancelled";
            cancelLoop = false;
            cancelButton.Enabled = false;
        }

        private void clearAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentList.Items.Clear();
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void clearSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = CurrentList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = CurrentList.SelectedIndices[x];
                CurrentList.Items.RemoveAt(idx);
            }

            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void clearAllYDRsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".ydr", CurrentList, FilesAddedLabel, startButton);
        private void clearAllYBNsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".ybn", CurrentList, FilesAddedLabel, startButton);
        private void clearAllYFTsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".yft", CurrentList, FilesAddedLabel, startButton);
        private void clearAllYDDsToolStripMenuItem_Click_1(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".ydd", CurrentList, FilesAddedLabel, startButton);
        private void clearAllRPFsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".rpf", CurrentList, FilesAddedLabel, startButton);

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                    string[] ydrFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ydr", SearchOption.AllDirectories);
                    string[] yftFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.yft", SearchOption.AllDirectories);

                    foreach (string file in ybnFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    foreach (string file in ydrFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    foreach (string file in yftFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    foreach (string file in rpfFiles)
                    {
                        TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                        RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                        try
                        {
                            rpf.ScanStructure(null, null);
                            var fileTypes = new List<string>() { ".ybn", ".ydr", ".yft", ".ydd" };
                            RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error can't read " + file + ".\nThis file has been skipped.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }
    }
}

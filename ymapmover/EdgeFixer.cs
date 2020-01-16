using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CodeWalker.GameFiles;

namespace ymapmover
{
    public partial class EdgeFixer : Form
    {
        public bool CancelLoop = false;
        public EdgeFixer()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EdgeFixer_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Types|*.ybn;*.ydr;" + "|YBN Files|*.ybn|YDR Files|*.ydr";
            CountItems();
        }

        private void addItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    CountItems();
                }).Start();
            }
        }
        private string ConvertMillisecondsToSeconds(double milliseconds)
        {
            if (TimeSpan.FromMilliseconds(milliseconds).TotalSeconds > 60)
            {
                return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes.ToString("0.00") + "m";
            }
            return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds.ToString("0.00") + "s";
        }
        private void CountItems()
        {
            int listCount = CurrentList.Items.Count;
            FilesAddedLabel.Text = listCount + " Item(s) Added";
            if (listCount == 0)
            {
                startButton.Enabled = false;
            }
            else
            {
                startButton.Enabled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            TimeLabel.Text = "Time Elapsed: 0ms";
            cancelButton.Enabled = true;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                for (var j = 0; j < CurrentList.Items.Count; j++)
                {
                    if (CancelLoop)
                    {
                        FilesAddedLabel.Text = "Cancelled";
                        CancelLoop = false;
                        return;
                    }
                    string filename = CurrentList.Items[j].ToString();
                    FilesAddedLabel.Text = "Processing " + Path.GetFileName(filename);
                    
                    if (filename.EndsWith(".ybn"))
                    {
                        YbnFile ybn = new YbnFile();
                        byte[] oldData = File.ReadAllBytes(filename);
                        RpfFile.LoadResourceFile<YbnFile>(ybn, oldData, 43);
                        byte[] newData = ybn.Save();
                        File.WriteAllBytes(filename, newData);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    else if (filename.EndsWith(".ydr"))
                    {
                        YdrFile ydr = new YdrFile();
                        byte[] oldData = File.ReadAllBytes(filename);
                        RpfFile.LoadResourceFile<YdrFile>(ydr, oldData, 165);
                        byte[] newData = ydr.Save();
                        File.WriteAllBytes(filename, newData);
                    }
                }
                FilesAddedLabel.Text = "Complete";
                var elapsedMs = watch.ElapsedMilliseconds;
                TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
                cancelButton.Enabled = false;
            }).Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelLoop = true;
        }

        private void clearAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentList.Items.Clear();
            CountItems();
            TimeLabel.Text = "Time Elapsed: 0ms";
        }

        private void clearSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = CurrentList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = CurrentList.SelectedIndices[x];
                CurrentList.Items.RemoveAt(idx);
            }
            CountItems();
            TimeLabel.Text = "Time Elapsed: 0ms";
        }

        private void clearAllYDRsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ydr")) { CurrentList.Items.RemoveAt(i); }
            }
            CountItems();
        }

        private void clearAllYBNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ybn")) { CurrentList.Items.RemoveAt(i); }
            }
            CountItems();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                    string[] ydrFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ydr", SearchOption.AllDirectories);
                    foreach (String file in ybnFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    foreach (String file in ydrFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    CountItems();
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

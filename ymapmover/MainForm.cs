using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CodeWalker.GameFiles;
using SharpDX;

namespace ymapmover
{
    public partial class MainForm : Form
    {
        public bool CancelLoop = false;
        public CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        public MainForm()
        {
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm MainF = new MainForm();
            MainF.Close();
            Application.Exit();
            Application.ExitThread();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ymapFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ymap", SearchOption.AllDirectories);
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                    foreach (String file in ymapFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    foreach (String file in ybnFiles)
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

        private void addItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Supported Types|*.ymap;*.ybn" + "|ymap|*.ymap|ybn|*.ybn";
            CountItems();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
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

        private void clearAllYBNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ybn")) { CurrentList.Items.RemoveAt(i); }
            }
            CountItems();
        }

        private void clearAllYMAPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ymap")) { CurrentList.Items.RemoveAt(i); }
            }
            CountItems();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
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
                    
                    Vector3 moveVec = new Vector3(float.Parse(xMove.Text, NumberStyles.Any, ci), float.Parse(yMove.Text, NumberStyles.Any, ci), float.Parse(zMove.Text, NumberStyles.Any, ci));

                    if (filename.Contains(".ybn"))
                    {
                        YbnFile ybn = new YbnFile();
                        byte[] oldData = File.ReadAllBytes(filename);
                        RpfFile.LoadResourceFile<YbnFile>(ybn, oldData, 43);

                        if (ybn.Bounds != null)
                        {
                            ybn.Bounds.BoundingBoxCenter = ybn.Bounds.BoundingBoxCenter + moveVec;
                            ybn.Bounds.BoundingBoxMax = ybn.Bounds.BoundingBoxMax + moveVec;
                            ybn.Bounds.BoundingBoxMin = ybn.Bounds.BoundingBoxMin + moveVec;
                            ybn.Bounds.Center = ybn.Bounds.Center + moveVec;

                            BoundComposite boundcomp = ybn.Bounds as BoundComposite;
                            var compchilds = boundcomp.Children?.data_items;
                            if (compchilds != null)
                            {
                                for (int i = 0; i < compchilds.Length; i++)
                                {
                                    compchilds[i].BoundingBoxCenter = compchilds[i].BoundingBoxCenter + moveVec;
                                    compchilds[i].BoundingBoxMax = compchilds[i].BoundingBoxMax + moveVec;
                                    compchilds[i].BoundingBoxMin = compchilds[i].BoundingBoxMin + moveVec;
                                    compchilds[i].Center = compchilds[i].Center + moveVec;
                                    BoundBVH bgeom = compchilds[i] as BoundBVH;
                                    if (bgeom != null)
                                    {
                                        bgeom.CenterGeom = bgeom.CenterGeom + moveVec;
                                    }
                                }
                            }
                        }

                        byte[] newData = ybn.Save();
                        File.WriteAllBytes(filename, newData);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    } else
                    {
                        YmapFile ymap = new YmapFile();
                        byte[] oldData = File.ReadAllBytes(filename);
                        ymap.Load(oldData);

                        if (ymap.GrassInstanceBatches != null)
                        {
                            foreach (YmapGrassInstanceBatch yEnts in ymap.GrassInstanceBatches)
                            {
                                yEnts.Position = yEnts.Position + moveVec;
                                yEnts.AABBMin = yEnts.AABBMin + moveVec;
                                yEnts.AABBMax = yEnts.AABBMax + moveVec;
                            }
                        }
                        if (ymap.CarGenerators != null)
                        {
                            foreach (YmapCarGen yEnts in ymap.CarGenerators) { yEnts.SetPosition(yEnts.Position + moveVec); }
                        }

                        if (ymap.AllEntities != null)
                        {
                            foreach (YmapEntityDef yEnts in ymap.AllEntities) { yEnts.SetPosition(yEnts.Position + moveVec); }
                        }

                        ymap._CMapData.streamingExtentsMax = ymap.CMapData.streamingExtentsMax + moveVec;
                        ymap._CMapData.streamingExtentsMin = ymap.CMapData.streamingExtentsMin + moveVec;
                        ymap._CMapData.entitiesExtentsMax = ymap.CMapData.entitiesExtentsMax + moveVec;
                        ymap._CMapData.entitiesExtentsMin = ymap.CMapData.entitiesExtentsMin + moveVec;

                        byte[] newData = ymap.Save();
                        File.WriteAllBytes(filename, newData);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                }
                FilesAddedLabel.Text = "Complete";
                var elapsedMs = watch.ElapsedMilliseconds;
                TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
                cancelButton.Enabled = false;
            }).Start();
        }

        private void xMove_TextChanged(object sender, EventArgs e)
        {
            FloatOnly(xMove);
        }

        private void yMove_TextChanged(object sender, EventArgs e)
        {
            FloatOnly(yMove);
        }

        private void zMove_TextChanged(object sender, EventArgs e)
        {
            FloatOnly(zMove);
        }

        private void FloatOnly(TextBox textBox)
        {
            float isFloat;
            if (!float.TryParse(textBox.Text, NumberStyles.Any, ci, out isFloat))
            {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.+-]", "");
            }
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

        private string ConvertMillisecondsToSeconds(double milliseconds)
        {
            if (TimeSpan.FromMilliseconds(milliseconds).TotalSeconds > 60)
            {
                return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes.ToString("0.00") + "m";
            }
            return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds.ToString("0.00") + "s";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelLoop = true;
        }
    }
}

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
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

        private void yBNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
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

        private void yMAPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ymapFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ymap", SearchOption.AllDirectories);
                    foreach (String file in ymapFiles)
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

        private void yFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string VersionNum = fvi.FileVersion.ToString();
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://fivem.xpl.wtf/ymapybnmover/version.txt");
            StreamReader reader = new StreamReader(stream);
            string VersionCheck = reader.ReadToEnd().Trim();
            if (VersionCheck != VersionNum)
            {
                string message = "YMAP & YBN mover is outdated\n\nYou are running v" + VersionNum + "\nThe latest version is v" + VersionCheck + "\n\nWould you like to download the update now?";
                if (MessageBox.Show(message, "Update Check", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    Process.Start("http://fivem.xpl.wtf/ymapybnmover/update.zip");
                    MainForm MainF = new MainForm();
                    MainF.Close();
                    Application.Exit();
                    Application.ExitThread();
                }
                outdatedLabel.Text = "This version is outdated";
            }

            Form.CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Types|*.ymap;*.ybn" + "|YMAP Files|*.ymap|YBN Files|*.ybn";
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
                            var compchilds = boundcomp?.Children?.data_items;
                            if (boundcomp.BVH != null)
                            {
                                Vector3 boundcompBBC = ConvertToVec3(boundcomp.BVH.BoundingBoxCenter);
                                Vector3 boundcompBBMax = ConvertToVec3(boundcomp.BVH.BoundingBoxMax);
                                Vector3 boundcompBBMin = ConvertToVec3(boundcomp.BVH.BoundingBoxMin);
                                boundcomp.BVH.BoundingBoxCenter = new Vector4(boundcompBBC + moveVec, boundcomp.BVH.BoundingBoxCenter.W);
                                boundcomp.BVH.BoundingBoxMax = new Vector4(boundcompBBMax + moveVec, boundcomp.BVH.BoundingBoxMax.W);
                                boundcomp.BVH.BoundingBoxMin = new Vector4(boundcompBBMin + moveVec, boundcomp.BVH.BoundingBoxMin.W);
                            }
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
                                        if (bgeom.BVH != null)
                                        {
                                            Vector3 bgeomBBC = ConvertToVec3(bgeom.BVH.BoundingBoxCenter);
                                            Vector3 bgeomBBMax = ConvertToVec3(bgeom.BVH.BoundingBoxMax);
                                            Vector3 bgeomBBMin = ConvertToVec3(bgeom.BVH.BoundingBoxMin);
                                            bgeom.BVH.BoundingBoxCenter = new Vector4(bgeomBBC + moveVec, bgeom.BVH.BoundingBoxCenter.W);
                                            bgeom.BVH.BoundingBoxMax = new Vector4(bgeomBBMax + moveVec, bgeom.BVH.BoundingBoxMax.W);
                                            bgeom.BVH.BoundingBoxMin = new Vector4(bgeomBBMin + moveVec, bgeom.BVH.BoundingBoxMin.W);
                                        }
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

        private Vector3 ConvertToVec3(Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
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

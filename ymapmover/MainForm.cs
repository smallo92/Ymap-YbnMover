using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CodeWalker.GameFiles;
using SharpDX;
using YmapYbnMover;

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
            Close();
            Application.Exit();
            Application.ExitThread();
        }

        public string xMoveBox
        {
            get { return xMove.Text; }
            set { xMove.Text = value; }
        }

        public string yMoveBox
        {
            get { return yMove.Text; }
            set { yMove.Text = value; }
        }

        public string zMoveBox
        {
            get { return zMove.Text; }
            set { zMove.Text = value; }
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
                    foreach (string file in ybnFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
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
                    foreach (string file in ymapFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
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
                    string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);
                    foreach (string file in ymapFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    foreach (string file in ybnFiles)
                    {
                        CurrentList.Items.Add(file);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    foreach (string file in rpfFiles)
                    {
                        TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                        RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                        rpf.ScanStructure(null, null);
                        var fileTypes = new List<string>() { ".ybn", ".ymap" };
                        RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
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
                    foreach (string file in openFileDialog1.FileNames)
                    {
                        if (file.EndsWith(".rpf"))
                        {
                            TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                            RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                            rpf.ScanStructure(null, null);
                            var fileTypes = new List<string>() { ".ybn", ".ymap" };
                            RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                        }
                        else
                        {
                            CurrentList.Items.Add(file);
                        }
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
                }).Start();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Types|*.rpf;*.ybn;*.ymap;" + "|RPF Files|*.rpf|YBN Files|*.ybn|YMAP Files|*.ymap";
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void ClearAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentList.Items.Clear();
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
            TimeLabel.Text = "Time Elapsed: 0ms";
        }

        private void ClearSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = CurrentList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = CurrentList.SelectedIndices[x];
                CurrentList.Items.RemoveAt(idx);
            }
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
            TimeLabel.Text = "Time Elapsed: 0ms";
        }

        private void ClearAllYBNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ybn")) { CurrentList.Items.RemoveAt(i); }
            }
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void ClearAllYMAPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".ymap")) { CurrentList.Items.RemoveAt(i); }
            }
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void clearAllRPFsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = CurrentList.Items.Count - 1; i >= 0; --i)
            {
                if (CurrentList.Items[i].ToString().Contains(".rpf")) { CurrentList.Items.RemoveAt(i); }
            }
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
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
                    FilesAddedLabel.Text = "Processing " + Path.GetFileName(filename) + " (" + (j + 1) + " of " + CurrentList.Items.Count + ")";
                    Vector3 moveVec = new Vector3(float.Parse(xMove.Text, NumberStyles.Any, ci), float.Parse(yMove.Text, NumberStyles.Any, ci), float.Parse(zMove.Text, NumberStyles.Any, ci));

                    if (filename.EndsWith(".ybn"))
                    {
                        YbnFile ybn = new YbnFile();
                        RpfDirectoryEntry RPFFilesDirectory;
                        byte[] oldData;
                        if (filename.Contains(".rpf"))
                        {
                            string fileDirectory = StringFunctions.TopMostRPF(filename);
                            RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                            TopRPF.ScanStructure(null, null);
                            (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                        }
                        else
                        {
                            RPFFilesDirectory = null;
                            oldData = File.ReadAllBytes(filename);
                        }

                        ybn.Load(oldData);

                        if (backupFilesToolStripMenuItem.Checked)
                        {
                            if (!filename.Contains(".rpf"))
                            {
                                string backupFilename = Path.Combine(CurrentList.Items[j].ToString(), ".old");
                                byte[] newDataBackup = ybn.Save();
                                File.WriteAllBytes(backupFilename, newDataBackup);
                            }
                            else
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                File.Copy(fileDirectory, Path.Combine(fileDirectory, ".old"));
                            }
                        }

                        if (ybn.Bounds != null)
                        {
                            ybn.Bounds.BoxCenter += moveVec;
                            ybn.Bounds.BoxMax += moveVec;
                            ybn.Bounds.BoxMin += moveVec;
                            ybn.Bounds.SphereCenter += moveVec;

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
                                    compchilds[i].BoxCenter += moveVec;
                                    compchilds[i].BoxMax += moveVec;
                                    compchilds[i].BoxMin += moveVec;
                                    compchilds[i].SphereCenter += moveVec;
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

                        if (filename.Contains(".rpf"))
                        {
                            RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                        } else
                        {
                            File.WriteAllBytes(filename, newData);
                        }

                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    else if (filename.EndsWith(".ymap"))
                    {
                        YmapFile ymap = new YmapFile();
                        RpfDirectoryEntry RPFFilesDirectory;
                        byte[] oldData;
                        if (filename.Contains(".rpf"))
                        {
                            string fileDirectory = StringFunctions.TopMostRPF(filename);
                            RpfFile TopRPF = new RpfFile(fileDirectory, fileDirectory);
                            TopRPF.ScanStructure(null, null);
                            (RPFFilesDirectory, oldData) = RPFFunctions.GetFileData(TopRPF, Path.GetFileName(filename));
                        }
                        else
                        {
                            RPFFilesDirectory = null;
                            oldData = File.ReadAllBytes(filename);
                        }

                        ymap.Load(oldData);

                        if (backupFilesToolStripMenuItem.Checked)
                        {
                            if (!filename.Contains(".rpf"))
                            {
                                string backupFilename = Path.Combine(CurrentList.Items[j].ToString(), ".old");
                                byte[] newDataBackup = ymap.Save();
                                File.WriteAllBytes(backupFilename, newDataBackup);
                            } else
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                File.Copy(fileDirectory, Path.Combine(fileDirectory, ".old"));
                            }
                        }

                        if (ymap.GrassInstanceBatches != null)
                        {
                            foreach (YmapGrassInstanceBatch yEnts in ymap.GrassInstanceBatches)
                            {
                                yEnts.Position += moveVec;
                                yEnts.AABBMin += moveVec;
                                yEnts.AABBMax += moveVec;
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

                        if (filename.Contains(".rpf"))
                        {
                            RPFFunctions.AddFileBackToRPF(RPFFilesDirectory, filename, newData);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, newData);
                        }

                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                }
                FilesAddedLabel.Text = "Complete";
                var elapsedMs = watch.ElapsedMilliseconds;
                TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
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
            if (!float.TryParse(textBox.Text, NumberStyles.Any, ci, out _))
            {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.+-]", "");
            }
        }

        private Vector3 ConvertToVec3(Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelLoop = true;
        }

        private void calculateVectorDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculateVectorDifference calculateVectorForm = new CalculateVectorDifference(this);
            calculateVectorForm.ShowDialog();
        }

        private void howToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YmapYbnHowTo ymapYbnHowToForm = new YmapYbnHowTo();
            ymapYbnHowToForm.ShowDialog();
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changelog changeLog = new changelog();
            changeLog.ShowDialog();
        }
        private void jSONToYMAPAndYTYPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void polyEdgeFixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EdgeFixer EdgeFixer = new EdgeFixer();
            EdgeFixer.ShowDialog();
        }

        private void rPFFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);
                    foreach (string file in rpfFiles)
                    {
                        TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                        RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                        rpf.ScanStructure(null, null);
                        var fileTypes = new List<string>() { ".ybn", ".ymap" };
                        RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + StringFunctions.ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
                }).Start();
            }
        }

    }
}
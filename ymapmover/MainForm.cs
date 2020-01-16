using System;
using System.Diagnostics;
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
            this.Close();
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
                    //string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);
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
                    //foreach (String file in rpfFiles)
                    //{
                    //    TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                    //    RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                    //    rpf.ScanStructure(UpdateStatus, UpdateErrorLog);
                    //    SearchRPF(rpf, file);
                    //    var elapsedMss = watch.ElapsedMilliseconds;
                    //    TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    //}
                    var elapsedMs = watch.ElapsedMilliseconds;
                    TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
                    CountItems();
                }).Start();
            }
        }

        //public void UpdateStatus(string text)
        //{
        //    try
        //    {
        //        if (InvokeRequired)
        //        {
        //            BeginInvoke(new Action(() => { UpdateStatus(text); }));
        //        }
        //    }
        //    catch { }
        //}
        //public void UpdateErrorLog(string text)
        //{
        //    try
        //    {
        //        if (InvokeRequired)
        //        {
        //            BeginInvoke(new Action(() => { UpdateErrorLog(text); }));
        //        }
        //    }
        //    catch { }
        //}

        //private bool IsFileLocked(FileInfo file)
        //{
        //    FileStream stream = null;
        //    try
        //    {
        //        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        //    }
        //    catch (IOException)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //            stream.Close();
        //    }
        //    return false;
        //}

        //private bool WaitForFile(string filename)
        //{
        //    var WrittenRpf = new FileInfo(filename);
        //    while (IsFileLocked(WrittenRpf)) { }
        //    return true;
        //}

        //public void SearchRPF(RpfFile rpf, string file)
        //{
        //    foreach (RpfEntry RPFFile in rpf.AllEntries)
        //    {
        //        if (RPFFile.Name.EndsWith(".ybn") || RPFFile.Name.EndsWith(".ymap"))
        //        {
        //            CurrentList.Items.Add(file + "\\" + rpf.Name + "\\" + RPFFile.Name);
        //        } else if (RPFFile.Name.EndsWith(".rpf"))
        //        {
        //            byte[] data;
        //            RpfBinaryFileEntry RecursiveRpf = (RpfBinaryFileEntry) RPFFile;
        //            data = rpf.ExtractFile(RecursiveRpf);
        //            string newFile = Path.GetDirectoryName(file) + "\\" + RPFFile.Name;
        //            File.WriteAllBytes(newFile, data);

        //            if (WaitForFile(newFile))
        //            {
        //                RpfFile ExportedRpf = new RpfFile(newFile, Path.GetDirectoryName(file));
        //                ExportedRpf.ScanStructure(UpdateStatus, UpdateErrorLog);
        //                SearchRPF(ExportedRpf, file);
        //            }
        //        }
        //    }
        //}

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
                        //if (file.EndsWith(".rpf"))
                        //{
                        //    TimeLabel.Text = "Scanning RPFs and Extracting Files ...";
                        //    RpfFile rpf = new RpfFile(file, Path.GetDirectoryName(file));
                        //    rpf.ScanStructure(UpdateStatus, UpdateErrorLog);
                        //    SearchRPF(rpf, file);
                        //} else
                        //{
                        //    CurrentList.Items.Add(file);
                        //}
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
            //openFileDialog1.Filter = "All Types|*.ymap;*.ybn;*.rpf;" + "|YMAP Files|*.ymap|YBN Files|*.ybn|RPF Files|*.rpf";
            openFileDialog1.Filter = "All Types|*.ymap;*.ybn;" + "|YMAP Files|*.ymap|YBN Files|*.ybn";
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

        //private string removeRPFsFromPath(string path)
        //{
        //    DirectoryInfo parentDir = new DirectoryInfo(path);
        //    while (parentDir.ToString().Contains(".rpf"))
        //    {
        //        try
        //        {
        //            if (parentDir.ToString().Contains(".rpf"))
        //            {
        //                parentDir = Directory.GetParent(parentDir.ToString());
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        catch { }
        //    }
        //    return parentDir.ToString();
        //}

        //private void ExtractRPFFiles(string filename, string type)
        //{
            
        //    string[] words = filename.Split('\\');
        //    string RPF = "";
        //    foreach (string word in words)
        //    {
        //        if (word.Contains(".rpf"))
        //        {
        //            RPF = word;
        //        }
        //    }
        //    RpfFile CurrentRpf = new RpfFile(RPF, Path.GetDirectoryName(filename));
        //    CurrentRpf.ScanStructure(UpdateStatus, UpdateErrorLog);
        //    foreach (RpfEntry RPFFile in CurrentRpf.AllEntries)
        //    {
        //        if (RPFFile.Name == Path.GetFileName(filename))
        //        {
        //            RpfResourceFileEntry InternalFile = (RpfResourceFileEntry)RPFFile;
        //            byte[] data = CurrentRpf.ExtractFile(InternalFile);
        //            string path = Path.GetDirectoryName(filename);
        //            string parentDir = removeRPFsFromPath(path);
        //            string currentFile = parentDir + "\\" + RPFFile.Name;

        //            data = ResourceBuilder.Compress(data);
        //            RpfResourceFileEntry rrfe = InternalFile as RpfResourceFileEntry;
        //            if (rrfe != null)
        //            {
        //                data = ResourceBuilder.AddResourceHeader(rrfe, data);
        //            }
        //            byte[] saveData = null;
        //            if (type == "ybn")
        //            {
        //                YbnFile newYbn = new YbnFile();
        //                RpfFile.LoadResourceFile<YbnFile>(newYbn, data, 43);
        //                saveData = newYbn.Save();
        //            } else
        //            {
        //                YmapFile newYmap = new YmapFile();
        //                newYmap.Load(data);
        //                saveData = newYmap.Save();
        //            }
        //            File.WriteAllBytes(currentFile, saveData);
        //        }
        //    }
        //}

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

                    if (filename.EndsWith(".ybn"))
                    {
                        //if (filename.Contains(".rpf"))
                        //{
                        //    ExtractRPFFiles(filename, "ybn");
                        //}
                        string newFile = filename;
                        //if (newFile.Contains(".rpf"))
                        //{
                        //    string parentDir = removeRPFsFromPath(newFile);
                        //    newFile = parentDir + "\\" + Path.GetFileName(filename);
                        //}
                        YbnFile ybn = new YbnFile();
                        byte[] oldData = File.ReadAllBytes(newFile);
                        RpfFile.LoadResourceFile<YbnFile>(ybn, oldData, 43);

                        if (backupFilesToolStripMenuItem.Checked)
                        {
                            //if (filename.Contains(".rpf")) { } else
                            //{
                                string backupFilename = CurrentList.Items[j].ToString() + ".old";
                                byte[] newDataBackup = ybn.Save();
                                File.WriteAllBytes(backupFilename, newDataBackup);
                            //}
                        }

                        if (ybn.Bounds != null)
                        {
                            ybn.Bounds.BoxCenter = ybn.Bounds.BoxCenter + moveVec;
                            ybn.Bounds.BoxMax = ybn.Bounds.BoxMax + moveVec;
                            ybn.Bounds.BoxMin = ybn.Bounds.BoxMin + moveVec;
                            ybn.Bounds.SphereCenter = ybn.Bounds.SphereCenter + moveVec;

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
                                    compchilds[i].BoxCenter = compchilds[i].BoxCenter + moveVec;
                                    compchilds[i].BoxMax = compchilds[i].BoxMax + moveVec;
                                    compchilds[i].BoxMin = compchilds[i].BoxMin + moveVec;
                                    compchilds[i].SphereCenter = compchilds[i].SphereCenter + moveVec;
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
                        File.WriteAllBytes(newFile, newData);
                        var elapsedMss = watch.ElapsedMilliseconds;
                        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
                    }
                    else if (filename.EndsWith(".ymap"))
                    {
                        //if (filename.Contains(".rpf"))
                        //{
                        //    ExtractRPFFiles(filename, "ymap");
                        //}
                        string newFile = filename;
                        //if (newFile.Contains(".rpf"))
                        //{
                        //    string parentDir = removeRPFsFromPath(newFile);
                        //    newFile = parentDir + "\\" + Path.GetFileName(filename);
                        //}
                        YmapFile ymap = new YmapFile();
                        byte[] oldData = File.ReadAllBytes(newFile);
                        ymap.Load(oldData);

                        if (backupFilesToolStripMenuItem.Checked)
                        {
                            //if (filename.Contains(".rpf")) { } else
                            //{
                                string backupFilename = CurrentList.Items[j].ToString() + ".old";
                                byte[] newDataBackup = ymap.Save();
                                File.WriteAllBytes(backupFilename, newDataBackup);
                            //}
                        }

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
                        File.WriteAllBytes(newFile, newData);
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
            //var watch = Stopwatch.StartNew();
            //if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            //{
            //    new Thread(() =>
            //    {
            //        Thread.CurrentThread.IsBackground = true;
            //        string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);
            //        foreach (String file in rpfFiles)
            //        {
            //            CurrentList.Items.Add(file);
            //            var elapsedMss = watch.ElapsedMilliseconds;
            //            TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMss).ToString();
            //        }
            //        var elapsedMs = watch.ElapsedMilliseconds;
            //        TimeLabel.Text = "Time Elapsed: " + ConvertMillisecondsToSeconds(elapsedMs).ToString();
            //        CountItems();
            //    }).Start();
            //}
        }
    }
}
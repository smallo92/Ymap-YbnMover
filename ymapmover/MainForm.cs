using CodeWalker.GameFiles;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using YmapYbnMover;

namespace ymapmover
{
    public partial class MainForm : Form
    {
        public bool cancelLoop = false;
        public System.Threading.Timer watch = null;
        public DateTime timerTime;
        public long timerStartTime = 0;

        public MainForm() => InitializeComponent();

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
        public string CurrentListBox
        {
            get { return CurrentList.SelectedIndex != -1 ? CurrentList.SelectedItem.ToString() : ""; }
        }

        private void yBNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                timerTime = DateTime.Now;
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                    foreach (string file in ybnFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);

                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void yMAPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                timerTime = DateTime.Now;
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ymapFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ymap", SearchOption.AllDirectories);
                    foreach (string file in ymapFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);

                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void yFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                timerTime = DateTime.Now;
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    string[] ymapFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ymap", SearchOption.AllDirectories);
                    string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                    string[] rpfFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.rpf", SearchOption.AllDirectories);

                    foreach (string file in ymapFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    foreach (string file in ybnFiles) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                    foreach (string file in rpfFiles)
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
                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void addItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                timerTime = DateTime.Now;
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
                                var fileTypes = new List<string>() { ".ybn", ".ymap" };
                                RPFFunctions.SearchRPF(rpf, file, CurrentList, fileTypes);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error can't read " + file + ".\nThis file has been skipped.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
                        }
                    }
                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.Filter = "All Types|*.rpf;*.ybn;*.ymap;" + "|RPF Files|*.rpf|YBN Files|*.ybn|YMAP Files|*.ymap";
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
            new Thread(() =>
            {
                timerTime = DateTime.Now;
                watch = new System.Threading.Timer(Tick, null, 0, 10);
                Thread.CurrentThread.IsBackground = true;
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (file.EndsWith("ybn") || file.EndsWith("ymap")) if (!StringFunctions.DoesItemExist(CurrentList, file)) CurrentList.Items.Add(file);
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

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void ClearAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentList.Items.Clear();
            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void ClearSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = CurrentList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = CurrentList.SelectedIndices[x];
                CurrentList.Items.RemoveAt(idx);
            }

            StringFunctions.CountItems(CurrentList, FilesAddedLabel, startButton);
        }

        private void ClearAllYBNsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".ybn", CurrentList, FilesAddedLabel, startButton);
        private void ClearAllYMAPsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".ymap", CurrentList, FilesAddedLabel, startButton);
        private void clearAllRPFsToolStripMenuItem_Click(object sender, EventArgs e) => StringFunctions.ClearItemsFromListBox(".rpf", CurrentList, FilesAddedLabel, startButton);

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
            var errorFiles = new List<string>(){};

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                //while (cancelLoop)
                //{
                    for (var j = 0; j < CurrentList.Items.Count; j++)
                    {
                        string filename = CurrentList.Items[j].ToString();
                        FilesAddedLabel.Text = "Processing " + Path.GetFileName(filename) + " (" + (j + 1) + " of " + CurrentList.Items.Count + ")";

                        Vector3 moveVec = new Vector3(float.Parse(xMove.Text), float.Parse(yMove.Text), float.Parse(zMove.Text));
                        //Quaternion rotVec = Quaternion.RotationYawPitchRoll(float.Parse(xRotate.Text), float.Parse(yRotate.Text), float.Parse(zRotate.Text));

                        if (backupFilesToolStripMenuItem.Checked)
                        {
                            if (!filename.Contains(".rpf")) File.Copy(filename, $"{filename}.old", true);
                            else
                            {
                                string fileDirectory = StringFunctions.TopMostRPF(filename);
                                File.Copy(fileDirectory, Path.Combine(fileDirectory, ".old"));
                            }
                        }

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
                                        Vector3 boundcompBBC = MathFunctions.ConvertToVec3(boundcomp.BVH.BoundingBoxCenter);
                                        Vector3 boundcompBBMax = MathFunctions.ConvertToVec3(boundcomp.BVH.BoundingBoxMax);
                                        Vector3 boundcompBBMin = MathFunctions.ConvertToVec3(boundcomp.BVH.BoundingBoxMin);
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
                                                    Vector3 bgeomBBC = MathFunctions.ConvertToVec3(bgeom.BVH.BoundingBoxCenter);
                                                    Vector3 bgeomBBMax = MathFunctions.ConvertToVec3(bgeom.BVH.BoundingBoxMax);
                                                    Vector3 bgeomBBMin = MathFunctions.ConvertToVec3(bgeom.BVH.BoundingBoxMin);
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
                        else if (filename.EndsWith(".ymap"))
                        {
                            YmapFile ymap = new YmapFile();
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
                                ymap.Load(oldData);
                                if (ymap.CarGenerators != null)
                                {
                                    foreach (YmapCarGen yEnts in ymap.CarGenerators)
                                    {
                                        yEnts.SetPosition(yEnts.Position + moveVec);
                                        //yEnts.Orientation = Quaternion.Add(yEnts.Orientation, rotVec);
                                    }
                                }
                                if (ymap.AllEntities != null)
                                {
                                    foreach (YmapEntityDef yEnts in ymap.AllEntities)
                                    {
                                        yEnts.SetPosition(yEnts.Position + moveVec);
                                        //yEnts.Orientation = Quaternion.Add(yEnts.Orientation, rotVec);
                                    }
                                }
                                if (ymap.DistantLODLights != null)
                                {
                                    int lightCount = ymap._CMapData.DistantLODLightsSOA.position.Count1;
                                    for (int i = 0; i < lightCount; i++)
                                    {
                                        Vector3 vector3 = ymap.DistantLODLights.positions[i].ToVector3() + moveVec;
                                        MetaVECTOR3 metaVec = new MetaVECTOR3 { x = vector3.X, y = vector3.Y, z = vector3.Z };
                                        ymap.DistantLODLights.positions[i] = metaVec;
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

                                ymap._CMapData.streamingExtentsMax = ymap.CMapData.streamingExtentsMax + moveVec;
                                ymap._CMapData.streamingExtentsMin = ymap.CMapData.streamingExtentsMin + moveVec;
                                ymap._CMapData.entitiesExtentsMax = ymap.CMapData.entitiesExtentsMax + moveVec;
                                ymap._CMapData.entitiesExtentsMin = ymap.CMapData.entitiesExtentsMin + moveVec;

                                byte[] newData = ymap.Save();
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
                //}
            }).Start();
        }

        private void xMove_TextChanged(object sender, EventArgs e) => MathFunctions.FloatOnly(xMove);
        private void yMove_TextChanged(object sender, EventArgs e) => MathFunctions.FloatOnly(yMove);
        private void zMove_TextChanged(object sender, EventArgs e) => MathFunctions.FloatOnly(zMove);

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelLoop = true;
            watch.Dispose();
            FilesAddedLabel.Text = "Cancelled";
            cancelLoop = false;
            cancelButton.Enabled = false;
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
            timerTime = DateTime.Now;
            watch = new System.Threading.Timer(Tick, null, 0, 10);
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
                    StringFunctions.SetCount(CurrentList, FilesAddedLabel, startButton);
                    watch.Dispose();
                }).Start();
            }
        }

        private void audioOcclusionHashGenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AudioOcclusionForm AudioOcclusionForm = new AudioOcclusionForm();
            AudioOcclusionForm.ShowDialog();
        }

        private void SourcecodeToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/smallo92/Ymap-YbnMover");
    }
}
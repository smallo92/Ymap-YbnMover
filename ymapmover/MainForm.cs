using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CodeWalker.Core;
using CodeWalker.GameFiles;
using SharpDX;

namespace ymapmover
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
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
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string[] ymapFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ymap", SearchOption.AllDirectories);
                string[] ybnFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ybn", SearchOption.AllDirectories);
                foreach (String file in ymapFiles) { CurrentList.Items.Add(file); }
                foreach (String file in ybnFiles) { CurrentList.Items.Add(file); }
            }
            CountItems();
        }

        private void addItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames) { CurrentList.Items.Add(file); }
            }
            CountItems();
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
        }

        private void clearSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = CurrentList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = CurrentList.SelectedIndices[x];
                CurrentList.Items.RemoveAt(idx);
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
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                for (var j = 0; j < CurrentList.Items.Count; j++)
                {
                    string filename = CurrentList.Items[j].ToString();
                    toolStripStatusLabel1.Text = "Processing " + Path.GetFileName(filename);
                    
                    Vector3 moveVec = new Vector3(float.Parse(xMove.Text), float.Parse(yMove.Text), float.Parse(zMove.Text));

                    if (filename.Contains(".ybn"))
                    {
                        YbnFile ybn = new YbnFile();
                        byte[] oldData = File.ReadAllBytes(filename);
                        RpfFile.LoadResourceFile<YbnFile>(ybn, oldData, 43);

                        if (ybn.Bounds != null)
                        {
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
                                }
                            }
                        }

                        byte[] newData = ybn.Save();
                        File.WriteAllBytes(filename, newData);

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
                        ymap._CMapData.streamingExtentsMin = ymap.CMapData.streamingExtentsMin.X + moveVec;
                        ymap._CMapData.entitiesExtentsMax = ymap.CMapData.entitiesExtentsMax.X + moveVec;
                        ymap._CMapData.entitiesExtentsMin = ymap.CMapData.entitiesExtentsMin.X + moveVec;

                        byte[] newData = ymap.Save();
                        File.WriteAllBytes(filename, newData);
                    }
                }
                toolStripStatusLabel1.Text = "Complete";
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
            if (!float.TryParse(textBox.Text, out isFloat))
            {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.+-]", "");
            }
        }

        private void CountItems()
        {
            int listCount = CurrentList.Items.Count;
            toolStripStatusLabel1.Text = listCount + " Item(s) Added";
            if (listCount == 0)
            {
                startButton.Enabled = false;
            }
            else
            {
                startButton.Enabled = true;
            }
        }
    }
}

﻿using SharpDX;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class Startup : Form
    {
        private string website = "http://fivem.fail/ymapybnmover/";

        public Startup()
        {
            InitializeComponent();
        }

        private void Startup_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string VersionNum = fvi.FileVersion.ToString();
            WebClient client = new WebClient();
            string VersionCheck = VersionNum;
            string ChangeLogText = "";
            Stream stream;
            StreamReader reader;
            try
            {
                stream = client.OpenRead($"{website}version.txt");
                reader = new StreamReader(stream);
                VersionCheck = reader.ReadToEnd().Trim();
            }
            catch (Exception) { }
            if (VersionCheck != VersionNum)
            {
                try
                {
                    stream = client.OpenRead($"{website}changelog.txt");
                    reader = new StreamReader(stream);
                    ChangeLogText = reader.ReadToEnd().Trim();
                }
                catch (Exception) { }
                string firstLine = "You're running ";
                string secondLine = "The latest version is";
                string thirdLine = "Would you like to download the update now?";
                string fourthLine = "Changelog:";
                var rtf = string.Format(@"{{\rtf1\ansi {0}\b {1}\b0 \line {2} \b {3}\b0\line\line {4}\line\line \b\ul {5}\ul0\b0\line {6}}}",
                    firstLine,
                    "v" + VersionNum,
                    secondLine,
                    "v" + VersionCheck,
                    thirdLine,
                    fourthLine,
                    ChangeLogText);
                mainTextbox.Rtf = rtf;

                Size rtbSize = TextRenderer.MeasureText(mainTextbox.Text, mainTextbox.Font, mainTextbox.Size, TextFormatFlags.WordBreak);
                int currentHeight = mainTextbox.Height;
                mainTextbox.Height = rtbSize.Height + (rtbSize.Height / mainTextbox.Lines.Count());
                int differenceHeight = (rtbSize.Height - currentHeight) + (rtbSize.Height / mainTextbox.Lines.Count());

                mainGroupBox.Height += differenceHeight;
                Height += differenceHeight;
                yesButton.Location = (System.Drawing.Point)new Size(yesButton.Location.X, yesButton.Location.Y + differenceHeight);
                noButton.Location = (System.Drawing.Point)new Size(noButton.Location.X, noButton.Location.Y + differenceHeight);
            } else
            {
                Program.OpenDetailFormOnClose = true;
                Close();
            }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            Process.Start($"{website}update.zip");
            Close();
            Application.Exit();
            Application.ExitThread();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            Program.OpenDetailFormOnClose = true;
            Close();
        }
    }
}

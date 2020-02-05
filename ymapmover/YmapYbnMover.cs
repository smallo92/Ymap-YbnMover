using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace YmapYbnMover
{
    public class StringFunctions
    {
        public static bool DoesItemExist(ListBox CurrentList, string file)
        {
            for (var j = 0; j < CurrentList.Items.Count; j++)
            {
                if (CurrentList.Items[j].ToString() == file)
                {
                    return true;
                }
            }
            return false;
        }

        public static string TopMostRPF(string filename)
        {
            string[] words = filename.Split('\\');
            string fileDirectory = null;
            foreach (string word in words)
            {
                if (fileDirectory == null)
                {
                    fileDirectory = word;
                }
                else
                {
                    fileDirectory = Path.Combine(fileDirectory, word);
                }
                if (word.Contains(".rpf"))
                {
                    return fileDirectory;
                }
            }
            return null;
        }

        public static string ConvertMillisecondsToSeconds(double milliseconds)
        {
            if (TimeSpan.FromMilliseconds(milliseconds).TotalSeconds > 60)
            {
                return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes.ToString("0.00") + "m";
            }
            return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds.ToString("0.00") + "s";
        }

        public static void CountItems(ListBox CurrentList, ToolStripStatusLabel FilesAddedLabel, Button startButton)
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
    }

    public class RPFFunctions
    {
        public static (RpfDirectoryEntry, byte[]) GetFileData(RpfFile CurrentRPF, string file)
        {
            foreach (RpfEntry RPFFile in CurrentRPF.AllEntries)
            {
                if (RPFFile.Name == file)
                {
                    byte[] oldData = SaveResourceAsBytes(RPFFile);
                    if (oldData != null)
                    {
                        return (RPFFile.Parent, oldData);
                    }
                }
            }
            foreach (RpfFile RPFs in CurrentRPF.Children)
            {
                RpfDirectoryEntry ChildRpfParent;
                byte[] finalFile;
                (ChildRpfParent, finalFile) = GetFileData(RPFs, file);
                if (finalFile != null)
                {
                    return (ChildRpfParent, finalFile);
                }
            }
            return (null, null);
        }

        public static void AddFileBackToRPF(RpfDirectoryEntry RPFDirectory, string filename, byte[] newData)
        {
            if (Regex.Matches(filename, ".rpf").Count > 1)
            {
                string topRPF = StringFunctions.TopMostRPF(filename);
                RpfFile CurrentRpf = new RpfFile(topRPF, topRPF);
                CurrentRpf.ScanStructure(null, null);
                DefragmentChildren(CurrentRpf, filename, newData, RPFDirectory);
            }
            else
            {
                RpfFile.CreateFile(RPFDirectory, Path.GetFileName(filename), newData, true);
                RpfFile CurrentRpf = new RpfFile(Path.GetDirectoryName(filename), Path.GetDirectoryName(filename));
                RpfFile.Defragment(CurrentRpf);
            }
        }

        public static void DefragmentChildren(RpfFile CurrentRpf, string file, byte[] newData, RpfDirectoryEntry RPFDirectory)
        {
            foreach (RpfEntry RPFFile in CurrentRpf.AllEntries)
            {
                if (RPFFile.Name == Path.GetFileName(file))
                {
                    RpfFile.CreateFile(RPFDirectory, Path.GetFileName(file), newData, true);
                    RpfFile.Defragment(CurrentRpf);
                    break;
                }
            }
            foreach (RpfFile RPFs in CurrentRpf.Children)
            {
                DefragmentChildren(RPFs, file, newData, RPFDirectory);
            }
        }

        public static void SearchRPF(RpfFile rpf, string file, ListBox CurrentList, List<string> FileTypes)
        {
            foreach (RpfEntry RPFFile in rpf.AllEntries)
            {
                foreach (string item in FileTypes)
                {
                    if (RPFFile.Name.EndsWith(item))
                    {
                        if (!StringFunctions.DoesItemExist(CurrentList, Path.Combine(file, RPFFile.Name)))
                        {
                            CurrentList.Items.Add(Path.Combine(file, RPFFile.Name));
                        }
                    }
                }
            }
            foreach (RpfFile RPFs in rpf.Children)
            {
                SearchRPF(RPFs, file + "\\" + RPFs.Name, CurrentList, FileTypes);
            }
        }

        public static byte[] SaveResourceAsBytes(RpfEntry RPFFile)
        {
            RpfFileEntry InternalFile = (RpfFileEntry)RPFFile;
            byte[] data = InternalFile.Parent.File.ExtractFile(InternalFile);
            RpfResourceFileEntry rrfe = InternalFile as RpfResourceFileEntry;
            if (rrfe != null)
            {
                data = ResourceBuilder.Compress(data);
                data = ResourceBuilder.AddResourceHeader(rrfe, data);
            }
            return data;
        }
    }
}
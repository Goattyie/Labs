using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4lab
{
    class Catalog
    {
        private List<string> tree_catalogs = new List<string>();
        private List<string> files = new List<string>();
        private DateTime time = new DateTime();
        private FileInfo fi;
        public Catalog()
        {
            this.time = DateTime.Now;
        }

        
        public void FindFolders(string path)
        {
            StreamWriter sw = new StreamWriter("log.txt");
            tree_catalogs.Add(path);
            int count = 0;
            while (count < tree_catalogs.Count())
            {
                for (int i = 0; i < Directory.GetDirectories(tree_catalogs[count]).Length; i++)
                {
                    tree_catalogs.Add(Directory.GetDirectories(tree_catalogs[count])[i]);
                }
                for (int j = 0; j < Directory.GetFiles(tree_catalogs[count]).Length; j++)
                {
                    if(j == 0)
                    {
                        string Folder = Directory.GetFiles(tree_catalogs[count])[j].Split(path)
                            [Directory.GetFiles(tree_catalogs[count])[j].Split(path).Length-1];
                        string path2 = Path.GetFileName(Directory.GetFiles(tree_catalogs[count])[j]);
                        Folder = Folder.Replace(path2, " ");
                        sw.WriteLine("Папка: " + Folder);
                    }
                    string path3 = Path.GetFileName(Directory.GetFiles(tree_catalogs[count])[j]);
                    sw.WriteLine(path3);
                    files.Add(Directory.GetFiles(tree_catalogs[count])[j]);
                }
                count++;
            }
            FileProcess();
            sw.Close();
        }
        private void FileProcess()
        {
            for(int i = 0; i < files.Count(); i++)
            {
                fi = new FileInfo(files[i]);
                if (this.time <= fi.CreationTime)
                {
                    try {
                        string name = fi.Name;
                        name = name.Insert(name.Length - fi.Extension.Length, "_fut");
                        File.Move(files[i], fi.Directory + "\\" + name);

                        var attr = File.GetAttributes(fi.Directory + "\\" + name);
                        if (fi.IsReadOnly == true)
                        {
                            attr = attr & ~FileAttributes.ReadOnly;
                            File.SetAttributes(fi.Directory + "\\" + name, attr);
                        }
                        else
                        {
                            attr = attr | FileAttributes.ReadOnly;
                            File.SetAttributes(fi.Directory + "\\" + name, attr);
                        }

                        File.SetCreationTime(fi.Directory + "\\" + name, DateTime.Now);
                        File.SetLastWriteTime(fi.Directory + "\\" + name, DateTime.Now);
                        
                    } catch (Exception e)
                    {
                        continue;
                    }
                }
            }
            
        }
        public DateTime GetTime() { return this.time; }
    }
}

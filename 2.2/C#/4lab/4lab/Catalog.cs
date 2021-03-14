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
        private StreamWriter sw = new StreamWriter("log.txt");
        private string main_path;
        public void FindFolders(string path)
        {
            this.main_path = path;
            tree_catalogs.Add(path);
            Recursion(path);
            sw.Close();
        }
        private void Recursion(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] Folder = path.Split(main_path);
            sw.WriteLine("Папка: " + Folder[Folder.Length-1]);
            for (int j = 0; j < files.Length; j++)
            {
                FileProcess(files[j]);
            }
            for (int i = 0; i < Directory.GetDirectories(path).Length; i++)
            {
                Recursion(Directory.GetDirectories(path)[i]);
            }
        }
        private void FileProcess(string filename)
        {
                fi = new FileInfo(filename);
                if (this.time <= fi.CreationTime)
                {
                    try {
                        string name = fi.Name;
                        File.SetLastWriteTime(fi.Directory + "\\" + name, DateTime.Now);
                        File.SetCreationTime(fi.Directory + "\\" + name, DateTime.Now);
                       

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
                    string new_name = name.Insert(name.Length - fi.Extension.Length, "_fut");
                    File.Move(filename, fi.Directory + "\\" + new_name);
                    sw.WriteLine(new_name + "\t True");
                        
                    } catch (Exception e) { 
                    sw.WriteLine(fi.Name + "\t False \t Файл с новым именем уже существует"); 
                }
                return;
                }
            
            sw.WriteLine(fi.Name + "\t False \t Файл из прошлого");
        }
        public DateTime GetTime() { return this.time; }
    }
}

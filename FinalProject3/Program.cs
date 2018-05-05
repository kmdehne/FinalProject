using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject3
{
    public class Program
    {
        static public string dir;
        static void Main(string[] args)
        {
            //Enter directory or use current directory
            Console.WriteLine("Please enter desired directory or type 'dir' to use current directory");
            string filepath = Console.ReadLine();
            if (filepath != null)
            {
                if (filepath == "dir")
                {
                    dir = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(dir);
                    Console.WriteLine("Current directory is: " + Directory.GetCurrentDirectory());
                }
                else if (Directory.Exists(filepath))
                {
                    dir = filepath;
                    Directory.SetCurrentDirectory(dir);
                    Console.WriteLine("Current directory is: " + Directory.GetCurrentDirectory());
                }
                else
                {
                    Console.WriteLine("Directory not found");
                }
            }
            Console.WriteLine("Counting Files...");
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(dir);
            var filecount = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
            var filelist = Directory.GetFiles(dir);
            Console.WriteLine(filecount.Length + " files found");

            //Finding duplicate files
            List<Duplicate> details = new List<Duplicate>();
            List<string> delete = new List<string>();
            foreach(var item in filelist)
            {
                using (var fs = new FileStream(item, FileMode.Open, FileAccess.Read))
                {
                    details.Add(new Duplicate()
                    {
                        FileName = item,
                        FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(fs)),
                    });
                }
            }


            var DuplicateList = details.GroupBy(f => f.FileHash).Select(g =>
            new { FileHash = g.Key, Files = g.Select(z => z.FileName).ToList() });



            delete.AddRange(DuplicateList.SelectMany(f => f.Files.Skip(1)).ToList());
            Console.WriteLine("Total files - {0}", delete.Count);

        }
    }
}

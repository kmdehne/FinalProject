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
            ConsoleKeyInfo keyinfo;
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

            //Finding duplicate files from hash values
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


            //Delete duplicate files
            if(delete.Count > 0)
            {
                Console.WriteLine("Press D to delete all duplicate files or press Q to quit\n");
                do
                {
                    keyinfo = Console.ReadKey();
                    if (keyinfo.Key == ConsoleKey.D)
                    {
                        Console.WriteLine("\nDeleting duplicate files...");
                        delete.ForEach(File.Delete);
                        Console.WriteLine("Duplicate files successfully deleted!");
                    }
                    Console.WriteLine("Press Q to quit\n");

                }
                while (keyinfo.Key != ConsoleKey.Q);
            }
            else
            {
                Console.WriteLine("No duplicate files to be deleted");
                Console.ReadLine();
            }
        }
    }
}

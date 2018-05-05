using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject3
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory;
            Console.WriteLine("Please enter desired directory or type 'dir' to use current directory");
            string filepath = Console.ReadLine();
            if (filepath != null)
            {
                if (filepath == "dir")
                {
                    directory = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(directory);
                    Console.WriteLine("Current directory is: " + Directory.GetCurrentDirectory());
                }
                else if (Directory.Exists(filepath))
                {
                    directory = filepath;
                    Directory.SetCurrentDirectory(directory);
                    Console.WriteLine("Current directory is: " + Directory.GetCurrentDirectory());
                }
                else
                {
                    Console.WriteLine("Directory not found");
                }
            }
            Console.WriteLine("Press any key to continue...");
            var duplicates{ }
        }
    }
}

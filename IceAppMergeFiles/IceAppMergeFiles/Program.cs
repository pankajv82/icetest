using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceAppMergeFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var basepath = AppDomain.CurrentDomain.BaseDirectory;
            basepath = basepath.Replace(@"\Debug\", "").Replace(@"\Release\", "");

            Console.WriteLine("Testing Merge Case of String type");
            string file1 = basepath + @"\test-data\string\file1.txt";
            string file2 = basepath + @"\test-data\string\file2.txt";
            string outputFile = basepath + @"\test-data\string\out.txt";
            Console.WriteLine("file1:" + file1);
            Console.WriteLine("file2:" + file2);
            FileManager.MergeFiles(file1, file2, outputFile);
            Console.WriteLine("Merged file is here:" + outputFile);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Testing Merge Case of Decimal type");
            file1 = basepath + @"\test-data\decimal\file1.txt";
            file2 = basepath + @"\test-data\decimal\file2.txt";
            outputFile = basepath + @"\test-data\decimal\out.txt";
            Console.WriteLine("file1:" + file1);
            Console.WriteLine("file2:" + file2);
            FileManager.MergeFiles(file1, file2, outputFile);
            Console.WriteLine("Merged file is here:" + outputFile);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Testing Merge Case of Datetime type");

            file1 = basepath + @"\test-data\datetime\file1.txt";
            file2 = basepath + @"\test-data\datetime\file2.txt";
            outputFile = basepath + @"\test-data\datetime\out.txt";
            Console.WriteLine("file1:" + file1);
            Console.WriteLine("file2:" + file2);
            FileManager.MergeFiles(file1, file2, outputFile);
            Console.WriteLine("Merged file is here:" + outputFile);

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }


}

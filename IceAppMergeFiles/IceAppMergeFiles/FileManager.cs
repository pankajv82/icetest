using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceAppMergeFiles
{
    class FileManager
    {
        /// <summary>
        /// Given two sorted files, write a C# program to merge them while preserving sort order.
        /// The program should determine what data type is used in input files (DateTime, Double, String in that sequence) and merge them accordingly.
        /// DO NOT assume either of these files can fit in memory.
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        public static void MergeFiles(string file1, string file2, string outFile)
        {
            string line1 = null;
            string line2 = null;

            bool isAscendingOrder = IsAscending(file1); // Check if Files are ascending order ot not

            if (!File.Exists(file1) || !File.Exists(file2))
            {
                throw new Exception("Input files does't exist");
            }

            using (FileStream fs1 = File.Open(file1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs1 = new BufferedStream(fs1))
            using (StreamReader sr1 = new StreamReader(bs1))
            {
                using (FileStream fs2 = File.Open(file2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs2 = new BufferedStream(fs2))
                using (StreamReader sr2 = new StreamReader(bs2))
                {
                    using (FileStream fStream = new FileStream(outFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        BufferedStream bStream = new BufferedStream(fStream);
                        TextWriter sw = new StreamWriter(bStream);

                        line1 = sr1.ReadLine();
                        line2 = sr2.ReadLine();

                        while (line1 != null && line2 != null) // Check if line1 is smaller than line2
                        {
                            if (isAscendingOrder)
                            {
                                if (IsFirstSmaller(line1, line2))
                                {
                                    sw.WriteLine(line1);
                                    line1 = sr1.ReadLine();
                                }
                                else
                                {
                                    sw.WriteLine(line2);
                                    line2 = sr2.ReadLine();
                                }
                            }
                            else
                            {
                                if (IsFirstSmaller(line1, line2))
                                {
                                    sw.WriteLine(line2);
                                    line2 = sr2.ReadLine();
                                }
                                else
                                {
                                    sw.WriteLine(line1);
                                    line1 = sr1.ReadLine();
                                }
                            }
                        }
                        while (line1 != null)
                        {
                            sw.WriteLine(line1);
                            line1 = sr1.ReadLine();
                        }
                        while (line2 != null)
                        {
                            sw.WriteLine(line2);
                            line2 = sr2.ReadLine();
                        }
                        bStream.Flush();
                        sw.Flush(); // empty buffer;
                        fStream.Flush();
                    }
                }
            }
        }
        private static bool IsFirstSmaller(string one, string two)
        {
            DateTime dt1, dt2;
            Decimal dc1, dc2;

            if (Decimal.TryParse(one, out dc1) && Decimal.TryParse(two, out dc2))
            {
                return dc1 < dc2;
            }

            if (DateTime.TryParse(one, out dt1) && DateTime.TryParse(two, out dt2))
            {
                return dt1 < dt2;
            }
            return String.Compare(one, two, StringComparison.InvariantCulture) < 0;
        }

        private static bool IsAscending(string file1)
        {
            using (FileStream fs = File.Open(file1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                return IsFirstSmaller(sr.ReadLine(), sr.ReadLine());
            }
        }
    }
}

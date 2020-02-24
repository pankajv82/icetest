using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusipPrices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cusip prices data processing started!");
            Console.WriteLine("input data file : CusipPricesfile.txt , output data file : CusipPricesResult.txt {both files are in bin folder}");
            CusipDataProcessing cdp = new CusipDataProcessing();
            try
            {
                var basepath = AppDomain.CurrentDomain.BaseDirectory;
                basepath = basepath.Replace(@"\Debug\", "").Replace(@"\Release\", "");
                string inputDatafile = basepath + @"\CusipPricesfile.txt";
                string outputDatafile = basepath + @"\CusipPricesResult.txt";

                cdp.ProcessData(inputDatafile, outputDatafile);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in program -" + e.Message);
            }
            Console.WriteLine("Cusip prices data processing End!! Enjoy your day.");
            Console.ReadKey();
        }
    }
}

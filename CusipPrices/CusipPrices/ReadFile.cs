using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CusipPrices
{
    class CusipPriceStatus
    {
        internal string cusipID;
        internal decimal highestPrice;
        internal decimal lowestPrice;
        internal decimal closingPrice;
        internal decimal openingPrice;
        public override string ToString()
        {
            return cusipID.ToString() + "# opening:" + openingPrice + ", lowest:" + lowestPrice + ", highest:" + highestPrice + ", closing:" + closingPrice;
        }
    }
    class CusipDataProcessing
    {
        Regex r = new Regex(@"[a-zA-Z]");
        internal void ProcessData(string inputDatafile, string outputDatafile)
        {
            string line;
            if (!File.Exists(inputDatafile))
            {
                throw new Exception("Input files does't exist");
            }

            CusipPriceStatus oCusipPriceStatus = null;
            List<decimal> prices = new List<decimal>();

            using (FileStream fs = File.Open(inputDatafile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader file = new StreamReader(bs))
            {
                using (FileStream fStream = new FileStream(outputDatafile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    BufferedStream bStream = new BufferedStream(fStream);
                    StreamWriter sw = new StreamWriter(bStream);

                    while ((line = file.ReadLine()) != null)
                    {
                        if (String.IsNullOrEmpty(line))
                            continue;

                        if (r.IsMatch(line))
                        {
                            //write current cusip values
                            if (oCusipPriceStatus != null)
                                WriteInFile(sw, oCusipPriceStatus, prices);

                            //create new 
                            oCusipPriceStatus = new CusipPriceStatus() { cusipID = line };
                            prices.Clear();
                            continue;
                        }
                        prices.Add(Convert.ToDecimal(line));
                    }

                    //last object write current cusip values
                    if (oCusipPriceStatus != null)
                        WriteInFile(sw, oCusipPriceStatus, prices);
                }
            }
        }

        private void WriteInFile(StreamWriter sw, CusipPriceStatus oCusipPriceStatus, List<decimal> prices)
        {
            if (prices != null && prices.Count > 0)
            {
                oCusipPriceStatus.openingPrice = prices[0];
                oCusipPriceStatus.closingPrice = prices[prices.Count - 1];
                oCusipPriceStatus.lowestPrice = Int32.MaxValue;
                oCusipPriceStatus.highestPrice = Int32.MinValue;

                foreach (var price in prices) // Find Min and Max
                {
                    if (price < oCusipPriceStatus.lowestPrice)
                    {
                        oCusipPriceStatus.lowestPrice = price;
                    }
                    if (price > oCusipPriceStatus.highestPrice)
                    {
                        oCusipPriceStatus.highestPrice = price;
                    }
                }
            }

            if (!string.IsNullOrEmpty(oCusipPriceStatus.cusipID))
            {
                // Create a file to write to.
                sw.WriteLine(oCusipPriceStatus.ToString());
                sw.Flush();
            }
        }

    }
}
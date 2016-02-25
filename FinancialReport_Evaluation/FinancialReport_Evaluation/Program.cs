using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


// time is sensitive, get something work first
namespace FinancialReport_Evaluation
{
     
    class Program
    {
        bool debugFlag = true;
        static void Main(string[] args)
        {
            
            String tickerPath = @"d:\FRE\ticker.txt";
            NewReport gatherFeb12 = new NewReport(tickerPath);
            gatherFeb12.farmReport();
            gatherFeb12.parseRawReport();
        }
    }

    class NewReport
    {
        String reportPath = @"d:\FRE\FR\";
        bool debugFlag = true;
        String tickerPath = "";
        public NewReport(String tickerPath)
        {
            this.tickerPath = tickerPath;
        }
        public void farmReport()
        {
            String line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(tickerPath);
            Console.WriteLine("Farming New Reports");
            Console.WriteLine("might take a while");
            //Console.ReadLine();

            //iterate tickers and download raw web pages
            WebClient client = new WebClient();
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine("working on: " + line);
                if (!fetchAndSaveData(line, client))
                {
                    Console.WriteLine("Failed!!!, please check");
                }
            }
            Console.WriteLine("Removing invalid reports");
            removeInvalidReports();
            Console.ReadLine();
        }

        bool fetchAndSaveData(String ticker, WebClient webResrouce)
        {
            String ISURL = @"http://finance.yahoo.com/q/is?s=" + ticker;
            String BSURL = @"http://finance.yahoo.com/q/bs?s=" + ticker;
            String CFURL = @"http://finance.yahoo.com/q/cf?s=" + ticker;
            String ISPath = reportPath+"IS_" + ticker + ".txt";
            String BSPath = reportPath + "BS_" + ticker + ".txt";
            String CFPath = reportPath + "CF_" + ticker + ".txt";
            using (System.IO.File.Create(ISPath)) 
            using (System.IO.File.Create(BSPath)) 
            using (System.IO.File.Create(CFPath))
            if (debugFlag)
            {
                Console.WriteLine(ISPath);
                Console.WriteLine(BSPath);
                Console.WriteLine(CFPath);
            }
            try
            {
                webResrouce.DownloadFile(ISURL, ISPath);
                webResrouce.DownloadFile(BSURL, BSPath);
                webResrouce.DownloadFile(CFURL, CFPath);
                //Console.ReadLine();
            }
            catch (WebException)
            {
                return false;
            }
            return true;
        }
        void removeInvalidReports()
        {
          String invalidKeywords1 = "data available for";
          String invalidKeywords2 = "no results for the given search term";
          String[] reportPaths = Directory.GetFiles(reportPath);
          List<string> invalidReportPaths = new List<string>();
          // iterate every files under reportPath
          foreach (String reportFile in reportPaths)
          {
              //if contain invalidKeywords,delete that file
              foreach (string reportContents in File.ReadAllLines(reportFile))
              {
                  if (reportContents.Contains(invalidKeywords1) || reportContents.Contains(invalidKeywords2))
                  {
                      invalidReportPaths.Add(reportFile);
                  }
              }

          }
          foreach (string invalidReportFile in invalidReportPaths)
          {
              Console.WriteLine("Remove Invalid Report:" + invalidReportFile);
              File.Delete(invalidReportFile);
          }
          
          
          
        }
        public void parseRawReport()
        {
        }
    }

    class EvaluateReport
    {
        bool debugFlag = true;
        public void CalculateRatio()
        {
        }
        public void AnalysisPerformance()
        {

        }
    }
}

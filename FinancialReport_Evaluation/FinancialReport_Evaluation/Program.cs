﻿using System;
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
        static void Main(string[] args)
        {
            String tickerPath = @"e:\FRE\ticker.txt";
            NewReport gatherFeb12 = new NewReport(tickerPath);
            gatherFeb12.farmReport();
            gatherFeb12.parseRawReport();
        }
    }

    class NewReport
    {
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
            Console.ReadLine();

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
        }

        bool fetchAndSaveData(String ticker, WebClient webResrouce)
        {
            String ISURL = @"http://finance.yahoo.com/q/is?s=" + ticker;
            String BSURL = @"http://finance.yahoo.com/q/bs?s=" + ticker;
            String CFURL = @"http://finance.yahoo.com/q/cf?s=" + ticker;
            String ISPath = @"e:\ticker\misc\IS_" + ticker + ".txt";
            String BSPath = @"e:\ticker\misc\BS_" + ticker + ".txt";
            String CFPath = @"e:\ticker\misc\CF_" + ticker + ".txt";
            using (System.IO.File.Create(ISPath)) ;
            using (System.IO.File.Create(BSPath)) ;
            using (System.IO.File.Create(CFPath)) ;
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

        public void parseRawReport()
        {
        }
    }

    class EvaluateReport
    {
        public void CalculateRatio()
        {
        }
        public void AnalysisPerformance()
        {

        }
    }
}
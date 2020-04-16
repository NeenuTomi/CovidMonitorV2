using System;
using System.IO;
using System.Net.Http;
using HtmlAgilityPack;


namespace CovidMonitor
{
    class Monitor
    {

        public static void Process()
        {

            string url = "https://www.ncdhhs.gov/covid-19-case-count-nc";
            try
            {
                HttpClient hc = new HttpClient();
                HttpResponseMessage result = hc.GetAsync(url).Result;

                Stream stream = result.Content.ReadAsStreamAsync().Result;
                HtmlDocument doc = new HtmlDocument();

                doc.Load(stream);
                var table = doc.DocumentNode.SelectSingleNode("//table");
                var rows = table.SelectNodes("//tbody/tr");

                int rowsCount = 1;
                //extracting the count from the web page
                Console.WriteLine("************* Printing Values ***********");
                foreach (var r in rows)
                {
                    //Console.WriteLine("Row count\t:" + rowsCount);
                    int colCount = 1;
                    var cols = r.ChildNodes;
                    foreach (var c in cols)
                    {
                        var value = c.InnerText;
                        //Console.Write(value.ToString());						
                        //Console.Write("\tcount colum:"+col);	

                        // Identifying the count from the first coulnm of first row 
                        if (rowsCount == 1 && colCount == 1)
                        {
                            //calling the phone message function
                            Console.Write("\nThe count of covid - 19 Confirmed Case in NC is  " + value);
                            PhoneUtility phone = new PhoneUtility();
                            phone.countOfToday = value;
                            phone.SendMessage();
                            Console.WriteLine("\nMessage sent to phone");
                            //skipping unwanted data
                            goto display;
                        }
                        colCount++;
                    }
                    rowsCount++;
                }
            display:
                Console.WriteLine("from the website https://www.ncdhhs.gov/covid-19-case-count-nc ");
            }
            catch (HtmlWebException e)
            {
                Console.WriteLine("html web exception");
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Agggregate Exception");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception");
            }
        }


    }
}





using System;
using System.IO;
using System.Net.Http;
using HtmlAgilityPack;


namespace CovidMonitor
{
    class AzureBrowserClass
    {
        
        public static void ExtractCount()
        {

            string url = "https://www.ncdhhs.gov/covid-19-case-count-nc";            
            try
            {
                HttpClient hc = new HttpClient();
                HttpResponseMessage result =  hc.GetAsync(url).Result;

                Stream stream = result.Content.ReadAsStreamAsync().Result;
                HtmlDocument doc = new HtmlDocument();

                doc.Load(stream);
                var table = doc.DocumentNode.SelectSingleNode("//table");
                var rows = table.SelectNodes("//tbody/tr");

				int rowscount = 1;
                //extracting the count from the web page
				Console.WriteLine("************* Printing Values ***********");
                foreach (var r in rows)
                {
					//Console.WriteLine("Row count\t:" + rowscount);
					int col = 1;
					var cols = r.ChildNodes;
                    foreach (var c in cols)
                    {
                        var value = c.InnerText;
						//Console.Write(value.ToString());						
						//Console.Write("\tcount colum:"+col);					
						if (rowscount==1&&col==1)
						{
                            //calling the phone message function
                            Console.Write("\nThe count of covid - 19 Confirmed Case in NC is  "+value);                            
							AzurePhoneClass phone = new AzurePhoneClass();
							phone.countoftoday = value;
							phone.SendMessage();
                            Console.WriteLine("\nMessage sent to phone");
                            //skipping unwanted data
                            goto disply;
						}
						col++;
					}
					rowscount++;
                }
                disply:
                Console.WriteLine("from the website https://www.ncdhhs.gov/covid-19-case-count-nc ");
            }
            catch (Exception e)
            {
                if(e is HtmlWebException)
                {
                    Console.WriteLine("html web exception");
                }
                if(e is AggregateException)
                {
                    Console.WriteLine("Aggregate exception");
                }                
                else
                {
                    Console.WriteLine("Exception");
                }
                    
            }
        }


    }
}


    


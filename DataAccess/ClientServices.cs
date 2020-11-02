
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ClientServices : IClientServices
    {
        public Config Config { get; set; }
        public string LastError { get; private set; }

      

        private string SessionID;
        public ClientServices(string sessionID)
        {
            SessionID = sessionID;
        }

        public void Initialize()
        {

        }
        public DataSet Execute(RequestCollection requests)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            this.LastError = "";

            var ds = DataProvider.Excute(requests, SessionID); //await DataService.ExecuteAsync(requests, SessionID);
            var table = ds.Tables[0];
            if (table != null)
            {
                if (table.TableName == "Error")
                {
                    var row = table.Rows[0];

                    var message = row["Message"].ToString();
                    var source = row["Source"].ToString();
                    var stackTrace = row["StackTrace"].ToString();
                    var helpLink = row["HelpLink"].ToString();

                    this.LastError = message;
                    ds = null;
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(DateTime.Now.ToString() + "Total time load data from sql server=" + elapsedMs);
            return ds;
        }
        public async Task<DataSet> ExecuteAsync(RequestCollection requests)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            this.LastError = "";

            var ds = await DataProvider.ExcuteAsync(requests, SessionID); //await DataService.ExecuteAsync(requests, SessionID);
            var table = ds.Tables[0];
            if (table != null)
            {
                if (table.TableName == "Error")
                {
                    var row = table.Rows[0];

                    var message = row["Message"].ToString();
                    var source = row["Source"].ToString();
                    var stackTrace = row["StackTrace"].ToString();
                    var helpLink = row["HelpLink"].ToString();

                    this.LastError = message;
                    ds = null;
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(DateTime.Now.ToString() + "Total time load data from sql server=" + elapsedMs);
            return ds;
        }


    }
}
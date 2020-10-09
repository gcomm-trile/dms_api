
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
        private HttpDataServices DataService { get; set; }

        private DataTable Localization { get; set; }

        private DataTable Setting { get; set; }

        private List<string> Permissions { get; set; }

        private List<string> UserPermissions { get; set; }

        public string LastError { get; private set; }

        private Dictionary<string, object> SharedInfo = new Dictionary<string, object>();

        private string SessionID;
        public ClientServices(string sessionID)
        {
            SessionID = sessionID;
            //this.Config = DataAccess.Config.Load();
            this.DataService = new HttpDataServices();
            this.Permissions = new List<string>();
            this.UserPermissions = new List<string>();
        }

        public void Initialize()
        {
                
        }

        public async Task<DataSet> ExecuteAsync(RequestCollection requests)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
         
          
            this.LastError = "";
            var ds =await DataService.ExecuteAsync(requests, SessionID);

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
            Debug.WriteLine("Total time load data from sql server=" + elapsedMs);
            return ds;
        }
        public DataSet Execute(RequestCollection requests)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here


            this.LastError = "";
            var ds =  DataService.Execute(requests,SessionID);

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
            Debug.WriteLine("Total time load data from sql server=" + elapsedMs);
            return ds;
        }

        public void SetInformation(string key, object value)
        {
            if (SharedInfo.ContainsKey(key))
                SharedInfo[key] = value;
            else
                SharedInfo.Add(key, value);
        }

        public object GetInformation(string key)
        {
            if (SharedInfo.ContainsKey(key))
                return SharedInfo[key];

            return null;
        }

      

        public object this[string key]
        {
            get { return GetInformation(key); }
            set { SetInformation(key, value); }
        }
    
    }
}
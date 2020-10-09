
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccess
{
    internal class HttpDataServices 
    {
     
        private const string ClientUserAgent = "my-api-client-v1";
        private const string MediaTypeJson = "application/json";
        public string HttpURLREST { get; private set; }
        public string HttpURL { get; private set; }
        private string SessionID;

        public CookieContainer HttpCookies { get; private set; }
        public string HttpUserAgent { get; private set; }

        public HttpDataServices(string webServiceURL="", int timeout = 120)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //this.HttpURLREST = @"http://localhost:63440/api";
            //this.HttpURLREST = @"http://119.82.129.43:8888/api";
            this.HttpURL = @"gcomm.online";           
            this.SessionID = Guid.NewGuid().ToString();
            this.HttpCookies = new CookieContainer();        
            this.HttpUserAgent = "Mozilla /5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
        }

        public async Task<DataSet> ExecuteAsync(RequestCollection requests, string sessionID)
        {          
            return await Task.Run(() => Execute(requests,sessionID));
        }

        public DataSet Execute(RequestCollection requests, string sessionID)
        {
            return DataProvider.Excute(requests,sessionID);
        }

        private static string ConvertToBase64Error(string error)
        {
            var table = new DataTable("Error");
            table.Columns.Add("Message");
            table.Columns.Add("Source");
            table.Columns.Add("StackTrace");
            table.Columns.Add("HelpLink");

            var row = table.NewRow();
            row["Message"] = error;
            table.Rows.Add(row);

            var response = new DataSet();
            response.Tables.Add(table);
            var bytes = Serializer.Compress(response);
            //var bytes = Serializer.ToBinary(response);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }
  
        //private string HttpPostREST(string data)
        //{
        //    try
        //    {             
        //        try
        //        {
        //            //string url = "https://gcommrestapi.azurewebsites.net/api/query?xml=" + data;
        //            string url = string.Format(" {0}/query?xml={1}", this.HttpURLREST, data);
        //            MyDebugger.WriteLog(url.Length.ToString());
        //            var client = new RestClient(HttpURLREST);
        //            client.Timeout = 300000;
        //            client.AutomaticDecompression=true;
        //            var request = new RestRequest(string.Format("query"), Method.GET);

        //            request.AddParameter("xml", data);
        //            request.AddHeader("SessionID", SessionID);
                    
        //            var response = client.Execute(request);

        //            if(response.Content.Length>2)
        //            {
        //                response.Content= response.Content.Remove(0, 1);
        //                response.Content = response.Content.Remove(response.Content.Length - 1, 1);
        //            }

        //            return response.Content;
        //        }
        //        catch (Exception ex)
        //        {
        //            return ConvertToBase64Error(ex.Message);
        //        }

        //    }

        //    catch (Exception wex)
        //    {

        //        return ConvertToBase64Error(wex.Message);
        //    }
        //}

        private async Task<string> GetWebPageHtmlSizeAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new HttpClient();
                var html = await client.GetStringAsync("");
                Console.WriteLine(html.ToString().Length);
                Console.WriteLine("done");
                return html.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private string HttpPost(string data)
        {


            try
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(HttpURL);
                //var request = new HttpClient();
                request.CookieContainer = HttpCookies;
                string boundary = "---------------------------7db1af18b064a";
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)";
                request.Method = "POST";
                request.Timeout = 120000;
                //request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = HttpUserAgent;
                request.AllowAutoRedirect = true;
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                Debug.WriteLine("Data without zip length " + data.Length);
                var data_send = Serializer.Compress(data);
                Debug.WriteLine("Data zip length " + data_send.Length);
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    if (string.IsNullOrEmpty(data_send) == false)
                    {
                        writer.Write(data_send);
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var stream = GetStreamForResponse(response))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();
                        return result;
                    }
                }
            }


            catch (WebException wex)
            {

                return ConvertToBase64Error(wex.Message);
            }
        }
        private static Stream GetStreamForResponse(HttpWebResponse webResponse)
        {
            Stream stream;
            switch (webResponse.ContentEncoding.ToUpperInvariant())
            {
                case "GZIP":
                    stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;

                case "DEFLATE":
                    stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;

                default:
                    stream = webResponse.GetResponseStream();
                    break;
            }
            return stream;
        }
    }

   
}

    //private void EnsureHttpClientCreated()
    //{
    //    if (_httpClient == null)
    //    {
    //        CreateHttpClient();
    //    }
    //}

    //private void CreateHttpClient()
    //{
    //    _httpClientHandler = new HttpClientHandler
    //    {
    //        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
    //    };

    //    _httpClient = new HttpClient(_httpClientHandler, false)
    //    {
    //        Timeout = _timeout
    //    };

    //    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(ClientUserAgent);

    //    if (!string.IsNullOrWhiteSpace(_baseUrl))
    //    {
    //        _httpClient.BaseAddress = new Uri(_baseUrl);
    //    }

    //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
    //}



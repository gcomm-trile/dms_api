using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public  class DataService
    {
        private static ClientServices clientServices;
       
        public  async Task<DataSet> ExecuteAsync(RequestCollection requests)
        {
            if(clientServices==null)
            {
                clientServices = new ClientServices("");
            }
            return await clientServices.ExecuteAsync(requests);
        }
        public string LastError => clientServices.LastError;
    }
}



using DataAccess;

using System.Data;
using System.Threading.Tasks;

namespace System
{
    public interface IClientServices
    {
        string LastError { get; }
        Task<DataSet> ExecuteAsync(RequestCollection requests);
        DataSet Execute(RequestCollection requests);
        object this[string key] { get; set; }

    }
}
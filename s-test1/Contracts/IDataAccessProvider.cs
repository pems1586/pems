using System.Data;

namespace PEMS.Contracts
{
    public interface IDataAccessProvider
    {
        IEnumerable<T> GetItems<T>() where T : class;

        IEnumerable<T> GetItems<T>(string query, object parameters, bool isStoredProcedure = false) where T : class;

        IEnumerable<T> GetAllItems<T>(string query) where T : class;

        int AddItem(string query, IDictionary<string, object> parameters);

        T GetItem<T>(string query, object obj, bool isStoredProcedure = false);

        int UpdateItem(string query, IDictionary<string, object> parameters);
    }
}

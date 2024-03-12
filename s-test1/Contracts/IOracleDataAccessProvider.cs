using Oracle.ManagedDataAccess.Client;
using PEMS.Models;
using System.Data;

namespace PEMS.Contracts
{
    public interface IOracleDataAccessProvider
    {
        bool CanDBConnected();

        List<PEMSystem> GetItems(string query);

        void AddItem(string query, PEMSystem pems);

        T GetItem<T>(string query, object obj, bool isStoredProcedure = false);

        bool UpdateItem(string query, PEMSystem pems);

        bool DeleteItem(string query, int id);
    }
}

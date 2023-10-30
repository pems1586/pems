using RepoDb;
using System.Data;
using Microsoft.Data.SqlClient;
using PEMS.Contracts;

namespace PEMS.Providers
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private string _connectionString;

        private readonly ILogger<DataAccessProvider> _Logger;

        public DataAccessProvider(IConfiguration configuration, ILogger<DataAccessProvider> logger)
        {
            _connectionString = configuration.GetConnectionString(Constants.ConnectionStringKey);
            this._Logger = logger;
        }

        public IEnumerable<T> GetItems<T>() where T : class
        {
            IEnumerable<T> items = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    items = connection.QueryAll<T>();
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(1, ex, ex.Message);
            }

            return items;
        }

        public IEnumerable<T> GetItems<T>(string query, object parameters, bool isStoredProcedure = false) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteQuery<T>(query, parameters, commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public IEnumerable<T> GetAllItems<T>(string query) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteQuery<T>(query);
            }
        }

        public int AddItem(string query, IDictionary<string, object> parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = string.Concat("DECLARE @ID int; ", query, "; SET @ID = SCOPE_IDENTITY(); SELECT @ID");
                var response = connection.ExecuteQuery<int>(sql, parameters);
                return response != null ? response.Single() : 0;
            }
        }

        public T GetItem<T>(string query, object obj, bool isStoredProcedure = false)
        {
            T item = default(T);
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var items = connection.ExecuteQuery<T>(query, obj, commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text);
                    item = items != null ? items.FirstOrDefault() : default(T);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(1, ex, ex.Message);
            }

            return item;
        }

        

        public int UpdateItem(string query, IDictionary<string, object> parameters)
        {
            //Do not include try catch, as the actual exception is used to show in the UI.
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var response = connection.ExecuteNonQuery(query, parameters);
                return response;
            }
        }
    }
}
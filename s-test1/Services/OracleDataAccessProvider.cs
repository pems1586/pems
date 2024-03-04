using RepoDb;
using System.Data;
using Microsoft.Data.SqlClient;
using PEMS.Contracts;
using Oracle.ManagedDataAccess.Client;
using PEMS.Models;
using Serilog;

namespace PEMS.Providers
{
    public class OracleDataAccessProvider : IOracleDataAccessProvider
    {
        private string _connectionString;

        public OracleDataAccessProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Constants.ConnectionStringKey);
        }

        public List<PEMSystem> GetItems(string query)
        {
            var response = new List<PEMSystem>();
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = query;
                        OracleDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            PEMSystem pems = new PEMSystem
                            {
                                FLE_ID = Convert.ToInt32(rdr["FLE_ID"]),
                                TST_PGM_CDE = rdr["TST_PGM_CDE"].ToString(),
                                TST_ADM__TST_DTE = Convert.ToDateTime(rdr["TST_ADM__TST_DTE"]),
                                SRC_SYS_ID = rdr["SRC_SYS_ID"].ToString(),
                                TRGT_SYS_ID = rdr["TRGT_SYS_ID"].ToString(),
                                FLE_NAM = rdr["FLE_NAM"].ToString(),
                                DTA_TYP_NAM = rdr["DTA_TYP_NAM"].ToString(),
                                FLE_SEQ_NO = rdr["FLE_SEQ_NO"].ToString(),
                                FILE_TYPE_CODE = rdr["FILE_TYPE_CODE"].ToString(),
                                FLE_PRCSD_DTE = Convert.ToDateTime(rdr["FLE_PRCSD_DTE"]),
                                TOT_RCD_CNT = Convert.ToInt32(rdr["TOT_RCD_CNT"]),
                                PPR_BSD_TSTG_MC_RCD_CNT = Convert.ToInt32(rdr["PPR_BSD_TSTG_MC_RCD_CNT"]),
                                PPR_BSD_TSTG_CR_RCD_CNT = Convert.ToInt32(rdr["PPR_BSD_TSTG_CR_RCD_CNT"]),
                                CBT_MC_RCD_CNT = Convert.ToInt32(rdr["CBT_MC_RCD_CNT"]),
                                CBT_CR_RCD_CNT = Convert.ToInt32(rdr["CBT_CR_RCD_CNT"]),
                                FLE_CRETN_DTM = Convert.ToDateTime(rdr["FLE_CRETN_DTM"]),
                                LST_UPDT_DTM = Convert.ToDateTime(rdr["LST_UPDT_DTM"])
                            };
                            response.Add(pems);
                        }
                    }
                }
                Log.Information("Get all records successfully.", "OracleDataAccessProvider - UpdateItem");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "OracleDataAccessProvider - GetItems");
            }

            return response;
        }

        public void AddItem(string query, OracleParameter[] parameters)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Parameters.AddRange(parameters);

                    con.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }

            Log.Information("Create record successfully.", "OracleDataAccessProvider - UpdateItem");
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

                Log.Information("GetItem record successfull.", "OracleDataAccessProvider - UpdateItem");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "OracleDataAccessProvider - GetItem");
            }

            return item;
        }



        public bool UpdateItem(string query, OracleParameter[] parameters)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Parameters.AddRange(parameters);

                        con.Open();
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }

                Log.Information("Updated record successfull.", "OracleDataAccessProvider - UpdateItem");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "OracleDataAccessProvider - UpdateItem");
                return false;
            }
        }
    }
}
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

        public bool CanDBConnected()
        {
            var response = true;
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        con.Close();
                    }
                }

                Log.Information("Database connected successfully.", "OracleDataAccessProvider - CanDBConnected");
            }
            catch (Exception ex)
            {
                response = false;
                Log.Error(ex.ToString(), "OracleDataAccessProvider - CanDBConnected");
            }

            return response;
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
                    con.Close();
                }
                Log.Information("Get all records successfully.", "OracleDataAccessProvider - UpdateItem");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "OracleDataAccessProvider - GetItems");
            }

            return response;
        }

        public void AddItem(string query, PEMSystem item)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.Parameters.Add(new OracleParameter("TST_PGM_CDE", item.TST_PGM_CDE));
                    cmd.Parameters.Add(new OracleParameter("TST_ADM__TST_DTE", item.TST_ADM__TST_DTE));
                    cmd.Parameters.Add(new OracleParameter("SRC_SYS_ID", item.SRC_SYS_ID));
                    cmd.Parameters.Add(new OracleParameter("TRGT_SYS_ID", item.TRGT_SYS_ID));
                    cmd.Parameters.Add(new OracleParameter("FLE_NAM", item.FLE_NAM));
                    cmd.Parameters.Add(new OracleParameter("DTA_TYP_NAM", item.DTA_TYP_NAM));
                    cmd.Parameters.Add(new OracleParameter("FLE_SEQ_NO", item.FLE_SEQ_NO));
                    cmd.Parameters.Add(new OracleParameter("FILE_TYPE_CODE", item.FILE_TYPE_CODE));
                    cmd.Parameters.Add(new OracleParameter("FLE_PRCSD_DTE", item.FLE_PRCSD_DTE));
                    cmd.Parameters.Add(new OracleParameter("TOT_RCD_CNT", item.TOT_RCD_CNT));
                    cmd.Parameters.Add(new OracleParameter("PPR_BSD_TSTG_MC_RCD_CNT", item.PPR_BSD_TSTG_MC_RCD_CNT));
                    cmd.Parameters.Add(new OracleParameter("PPR_BSD_TSTG_CR_RCD_CNT", item.PPR_BSD_TSTG_CR_RCD_CNT));
                    cmd.Parameters.Add(new OracleParameter("CBT_MC_RCD_CNT", item.CBT_MC_RCD_CNT));
                    cmd.Parameters.Add(new OracleParameter("CBT_CR_RCD_CNT", item.CBT_CR_RCD_CNT));
                    cmd.Parameters.Add(new OracleParameter("FLE_CRETN_DTM", item.FLE_CRETN_DTM));
                    cmd.Parameters.Add(new OracleParameter("LST_UPDT_DTM", item.LST_UPDT_DTM));

                    con.Open();
                    cmd.BindByName = true;
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



        public bool UpdateItem(string query, PEMSystem item)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        cmd.Parameters.Add(new OracleParameter("FLE_ID", item.FLE_ID));
                        cmd.Parameters.Add(new OracleParameter("TST_PGM_CDE", item.TST_PGM_CDE));
                        cmd.Parameters.Add(new OracleParameter("TST_ADM__TST_DTE", item.TST_ADM__TST_DTE));
                        cmd.Parameters.Add(new OracleParameter("SRC_SYS_ID", item.SRC_SYS_ID));
                        cmd.Parameters.Add(new OracleParameter("TRGT_SYS_ID", item.TRGT_SYS_ID));
                        cmd.Parameters.Add(new OracleParameter("FLE_NAM", item.FLE_NAM));
                        cmd.Parameters.Add(new OracleParameter("DTA_TYP_NAM", item.DTA_TYP_NAM));
                        cmd.Parameters.Add(new OracleParameter("FLE_SEQ_NO", item.FLE_SEQ_NO));
                        cmd.Parameters.Add(new OracleParameter("FILE_TYPE_CODE", item.FILE_TYPE_CODE));
                        cmd.Parameters.Add(new OracleParameter("FLE_PRCSD_DTE", item.FLE_PRCSD_DTE));
                        cmd.Parameters.Add(new OracleParameter("TOT_RCD_CNT", item.TOT_RCD_CNT));
                        cmd.Parameters.Add(new OracleParameter("PPR_BSD_TSTG_MC_RCD_CNT", item.PPR_BSD_TSTG_MC_RCD_CNT));
                        cmd.Parameters.Add(new OracleParameter("PPR_BSD_TSTG_CR_RCD_CNT", item.PPR_BSD_TSTG_CR_RCD_CNT));
                        cmd.Parameters.Add(new OracleParameter("CBT_MC_RCD_CNT", item.CBT_MC_RCD_CNT));
                        cmd.Parameters.Add(new OracleParameter("CBT_CR_RCD_CNT", item.CBT_CR_RCD_CNT));
                        cmd.Parameters.Add(new OracleParameter("FLE_CRETN_DTM", item.FLE_CRETN_DTM));
                        cmd.Parameters.Add(new OracleParameter("LST_UPDT_DTM", item.LST_UPDT_DTM));

                        con.Open();
                        cmd.BindByName = true;
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

        public bool DeleteItem(string query, int id)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        cmd.Parameters.Add(new OracleParameter("FLE_ID", id));

                        con.Open();
                        cmd.BindByName = true;
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
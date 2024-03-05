using Oracle.ManagedDataAccess.Client;
using PEMS.Contracts;
using PEMS.Models;
using Serilog;
using System.Dynamic;

namespace PEMS.Providers
{
    public class PEMSProvider : BaseProvider, IPEMSProvider
    {
        private readonly ILogger<PEMSProvider> _Logger;

        public PEMSProvider(IOracleDataAccessProvider dataAccessProvider, ILogger<PEMSProvider> logger) : base(dataAccessProvider)
        {
            this._Logger = logger;
        }

        public bool CanDBConnected()
        {
            try
            {
                return this.DataAccessProvider.CanDBConnected();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "PEMSProvider - GetAll");
                throw ex;
            }
        }

        public List<PEMSystem> GetAll()
        {
            try
            {
                return this.DataAccessProvider.GetItems(Constants.QueryString.GetPEMS).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "PEMSProvider - GetAll");
                throw ex;
            }
        }

        public bool Save(PEMSystem item)
        {
            try
            {
                var parameters = new OracleParameter[] { };
                if (item.FLE_ID > 0)
                {
                    parameters.Append(new OracleParameter("FLE_ID", item.FLE_ID));
                }

                parameters.Append(new OracleParameter("TST_PGM_CDE", item.TST_PGM_CDE));
                parameters.Append(new OracleParameter("TST_ADM__TST_DTE", item.TST_ADM__TST_DTE));
                parameters.Append(new OracleParameter("SRC_SYS_ID", item.SRC_SYS_ID));
                parameters.Append(new OracleParameter("TRGT_SYS_ID", item.TRGT_SYS_ID));
                parameters.Append(new OracleParameter("FLE_NAM", item.FLE_NAM));
                parameters.Append(new OracleParameter("DTA_TYP_NAM", item.DTA_TYP_NAM));
                parameters.Append(new OracleParameter("FLE_SEQ_NO", item.FLE_SEQ_NO));
                parameters.Append(new OracleParameter("FILE_TYPE_CODE", item.FILE_TYPE_CODE));
                parameters.Append(new OracleParameter("FLE_PRCSD_DTE", item.FLE_PRCSD_DTE));
                parameters.Append(new OracleParameter("TOT_RCD_CNT", item.TOT_RCD_CNT));
                parameters.Append(new OracleParameter("PPR_BSD_TSTG_MC_RCD_CNT", item.PPR_BSD_TSTG_MC_RCD_CNT));
                parameters.Append(new OracleParameter("PPR_BSD_TSTG_CR_RCD_CNT", item.PPR_BSD_TSTG_CR_RCD_CNT));
                parameters.Append(new OracleParameter("CBT_MC_RCD_CNT", item.CBT_MC_RCD_CNT));
                parameters.Append(new OracleParameter("CBT_CR_RCD_CNT", item.CBT_CR_RCD_CNT));
                parameters.Append(new OracleParameter("FLE_CRETN_DTM", item.FLE_CRETN_DTM));
                parameters.Append(new OracleParameter("LST_UPDT_DTM", item.LST_UPDT_DTM));
                if (item.FLE_ID > 0)
                {
                    this.DataAccessProvider.UpdateItem(Constants.QueryString.UpdatePEMS, parameters);
                }
                else
                {
                    this.DataAccessProvider.AddItem(Constants.QueryString.AddPEMS, parameters);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "PEMSProvider - Save");
                return false;
            }
        }

        public bool Delete(int ID)
        {
            try
            {
                var parameters = new OracleParameter[] { 
                    new OracleParameter("FLE_ID", ID)
                };
                return this.DataAccessProvider.UpdateItem(Constants.QueryString.DeletePEMS, parameters);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "PEMSProvider - Delete");
                throw ex;
            }
        }
    }
}

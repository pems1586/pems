using PEMS.Contracts;
using PEMS.Models;
using System.Dynamic;

namespace PEMS.Providers
{
    public class PEMSProvider : BaseProvider, IPEMSProvider
    {
        private readonly ILogger<PEMSProvider> _Logger;

        public PEMSProvider(IDataAccessProvider dataAccessProvider /*IUserContext userContext*/, ILogger<PEMSProvider> logger) : base(dataAccessProvider /*userContext*/)
        {
            this._Logger = logger;
        }

        public List<PEMSystem> GetAll()
        {
            try
            {
                return this.DataAccessProvider.GetAllItems<PEMSystem>(Constants.QueryString.GetPEMS).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(PEMSystem item)
        {
            try
            {
                var parameters = new ExpandoObject() as IDictionary<string, object>;
                if (item.FLE_ID > 0)
                {
                    parameters.Add("FLE_ID", item.FLE_ID);
                }
                parameters.Add("TST_PGM_CDE", item.TST_PGM_CDE);
                parameters.Add("TST_ADM__TST_DTE", item.TST_ADM__TST_DTE);
                parameters.Add("SRC_SYS_ID", item.SRC_SYS_ID);
                parameters.Add("TRGT_SYS_ID", item.TRGT_SYS_ID);
                parameters.Add("FLE_NAM", item.FLE_NAM);
                parameters.Add("DTA_TYP_NAM", item.DTA_TYP_NAM);
                parameters.Add("FLE_SEQ_NO", item.FLE_SEQ_NO);
                parameters.Add("FILE_TYPE_CODE", item.FILE_TYPE_CODE);
                parameters.Add("FLE_PRCSD_DTE", item.FLE_PRCSD_DTE);
                parameters.Add("TOT_RCD_CNT", item.TOT_RCD_CNT);
                parameters.Add("PPR_BSD_TSTG_MC_RCD_CNT", item.PPR_BSD_TSTG_MC_RCD_CNT);
                parameters.Add("PPR_BSD_TSTG_CR_RCD_CNT", item.PPR_BSD_TSTG_CR_RCD_CNT);
                parameters.Add("CBT_MC_RCD_CNT", item.CBT_MC_RCD_CNT);
                parameters.Add("CBT_CR_RCD_CNT", item.CBT_CR_RCD_CNT);
                parameters.Add("FLE_CRETN_DTM", item.FLE_CRETN_DTM);
                parameters.Add("LST_UPDT_DTM", item.LST_UPDT_DTM);
                if (item.FLE_ID > 0)
                {
                    return this.DataAccessProvider.UpdateItem(Constants.QueryString.UpdatePEMS, parameters);
                }
                else
                {
                    return this.DataAccessProvider.AddItem(Constants.QueryString.AddPEMS, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int ID)
        {
            try
            {
                var parameters = new ExpandoObject() as IDictionary<string, object>;
                parameters.Add("FLE_ID", ID);
                return this.DataAccessProvider.UpdateItem(Constants.QueryString.DeletePEMS, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
                if (item.FLE_ID > 0)
                {
                    this.DataAccessProvider.UpdateItem(Constants.QueryString.UpdatePEMS, item);
                }
                else
                {
                    this.DataAccessProvider.AddItem(Constants.QueryString.AddPEMS, item);
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
                return this.DataAccessProvider.DeleteItem(Constants.QueryString.DeletePEMS, ID);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "PEMSProvider - Delete");
                throw ex;
            }
        }
    }
}

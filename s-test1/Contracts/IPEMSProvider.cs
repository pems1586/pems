using PEMS.Models;
using System.Data;

namespace PEMS.Contracts
{
    public interface IPEMSProvider
    {
        bool CanDBConnected();

        List<PEMSystem> GetAll();

        bool Save(PEMSystem item);

        bool Delete(int ID);
    }
}

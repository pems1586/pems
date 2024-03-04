using PEMS.Models;
using System.Data;

namespace PEMS.Contracts
{
    public interface IPEMSProvider
    {
        List<PEMSystem> GetAll();

        bool Save(PEMSystem item);

        bool Delete(int ID);
    }
}

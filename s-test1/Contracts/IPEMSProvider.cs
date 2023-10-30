using PEMS.Models;
using System.Data;

namespace PEMS.Contracts
{
    public interface IPEMSProvider
    {
        List<PEMSystem> GetAll();

        int Save(PEMSystem item);

        bool Delete(int ID);
    }
}

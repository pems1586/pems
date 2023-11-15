
using PEMS.Contracts;

namespace PEMS.Providers
{
    public class BaseProvider
    {
        protected IOracleDataAccessProvider DataAccessProvider { get; private set; }
        
        //protected IUserContext UserContext { get; private set; }

        public BaseProvider(IOracleDataAccessProvider dataAccessProvider /*IUserContext userContext*/)
        {
            this.DataAccessProvider = dataAccessProvider;
            //this.UserContext = userContext;
        }
    }
}

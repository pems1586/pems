
using PEMS.Contracts;

namespace PEMS.Providers
{
    public class BaseProvider
    {
        protected IDataAccessProvider DataAccessProvider { get; private set; }
        
        //protected IUserContext UserContext { get; private set; }

        public BaseProvider(IDataAccessProvider dataAccessProvider /*IUserContext userContext*/)
        {
            this.DataAccessProvider = dataAccessProvider;
            //this.UserContext = userContext;
        }
    }
}

using DataAccess;

namespace BusinessLogic
{
    public class BusinessLogic
    {
        public AdminLogic AdminLogic;
        public TokenLogic TokenLogic;
        public UserLogic UserLogic;
        public AuthLogic AuthLogic;

        public BusinessLogic(IDataAccess _dataAccess)
        {
            AdminLogic = new AdminLogic(_dataAccess);
            TokenLogic = new TokenLogic(_dataAccess);
            UserLogic = new UserLogic(_dataAccess);
            AuthLogic = new AuthLogic(_dataAccess);
        }



    }
}

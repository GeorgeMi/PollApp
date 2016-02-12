using DataAccess;

namespace BusinessLogic
{
    public class BusinessLogic
    {
        public AnswerLogic AnswerLogic;
        public CategoryLogic CategoryLogic;
        public FormLogic FormLogic;
        public QuestionLogic QuestionLogic;
        public TokenLogic TokenLogic;
        public UserLogic UserLogic;
        public AuthLogic AuthLogic;

        public BusinessLogic(IDataAccess _dataAccess)
        {
            AnswerLogic = new AnswerLogic(_dataAccess);
            CategoryLogic = new CategoryLogic(_dataAccess);
            FormLogic = new FormLogic(_dataAccess);
            QuestionLogic = new QuestionLogic(_dataAccess);
            TokenLogic = new TokenLogic(_dataAccess);
            UserLogic = new UserLogic(_dataAccess);
            AuthLogic = new AuthLogic(_dataAccess);
        }



    }
}

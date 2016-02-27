namespace WebAPI.Messages
{
    public class SuccessMessage : MyMessage
    {
        public string success;

        public SuccessMessage(string success)
        {
            this.success = success;
        }
    }
}
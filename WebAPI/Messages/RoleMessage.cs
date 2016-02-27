namespace WebAPI.Messages
{
    public class RoleMessage : MyMessage
    {
        public string role;

        public RoleMessage(string role)
        {
            this.role = role;
        }
    }
}
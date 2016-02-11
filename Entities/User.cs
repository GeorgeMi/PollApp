using System.Collections.Generic;

namespace Entities
{
    public partial class User
    {
        public User()
        {
            this.Forms = new List<Form>();
            this.Tokens = new List<Token>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}

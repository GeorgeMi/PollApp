using System.Collections.Generic;

namespace Entities
{
    public partial class Form
    {
        public Form()
        {
            this.Questions = new List<Question>();
            this.VotedForms = new List<VotedForms>();
        }

        public int FormID { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public System.DateTime Deadline { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<VotedForms> VotedForms { get; set; }
    }
}

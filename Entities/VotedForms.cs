namespace Entities
{
    public partial class VotedForms
    {
        public int UserID { get; set; }
        public int FormID { get; set; }
        public virtual User User { get; set; }
        public virtual Form Form { get; set; }
    }
}

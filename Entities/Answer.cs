namespace Entities
{
    public partial class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public decimal NrVotes { get; set; }
        public virtual Question Question { get; set; }
    }
}

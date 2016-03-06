using System.Collections.Generic;

namespace DataTransferObject
{
    public class QuestionDTO
    {
        public string Question { get; set; }
        public int QuestionID { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}


using System.Collections.Generic;

namespace DataTransferObject
{
   public class VoteQuestionResultDTO
    {
        public int QuestionID { get; set; }
        public int QuestionNrVotes { get; set; }
        public List<VoteAnswerResultDTO> Answers { get; set; }
    }
}

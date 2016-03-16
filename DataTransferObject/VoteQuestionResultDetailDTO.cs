using System.Collections.Generic;

namespace DataTransferObject
{
    public class VoteQuestionResultDetailDTO
    {
        public string Question { get; set; }
        public List<VoteAnswerDetailResultDTO> Answers { get; set; }
    }
}

using System.Collections.Generic;

namespace DataTransferObject
{
    public class VoteResultDetailDTO
    {
        public int NrVotes { get; set; }
        public string Title { get; set; }
        public List<VoteQuestionResultDetailDTO> Questions { get; set; }
    }
}

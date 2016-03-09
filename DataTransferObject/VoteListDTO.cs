using System.Collections.Generic;

namespace DataTransferObject
{
    public class VoteListDTO
    {
        public string Username { get; set; }
        public List<VoteDTO> Answers { get; set; }
    }
}
/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  Form DTO.
 *
 * History:
 * 25.02.2016    Miron George       Created class .
 */

using System.Collections.Generic;

namespace DataTransferObject
{
    public class FormDetailDTO
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string CreatedDate { get; set; }
        public string Deadline { get; set; }
        public string State { get; set; }
        public int Id { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}

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

namespace DataTransferObject
{
    public class FormDTO
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string CreatedDate { get; set; }
        public string Deadline { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public bool Voted { get; set; }

    }
}

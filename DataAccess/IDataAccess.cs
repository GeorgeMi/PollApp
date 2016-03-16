/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 *
 * History:
 * 09.02.2016    Miron George       Created interface.
 */
using DataAccess.Repository.Interfaces;

namespace DataAccess
{
    public interface IDataAccess
    {
        IUserRepository UserRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        ITokenRepository TokenRepository { get; set; }
        IQuestionRepository QuestionRepository { get; set; }
        IAnswerRepository AnswerRepository { get; set; }
        IFormRepository FormRepository { get; set; }
        IVotedFormsRepository VotedFormsRepository { get; set; }

    }
}

/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 * Separates the data access logic and maps it to the entities in the business logic
 *
 * History:
 * 09.02.2016    Miron George       Created interface.
 */
using Entities;

namespace DataAccess.Repository.Interfaces
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
        void AddVote(int id);
    }
}

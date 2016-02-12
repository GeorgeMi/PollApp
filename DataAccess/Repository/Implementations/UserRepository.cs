/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 * Repository implementation. 
 *
 * History:
 * 09.02.2016    Miron George       Created class.
 */
using Entities;
using DataAccess.Repository.Interfaces;
using DataAccess.Context;
using System;

namespace DataAccess.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PollAppDBContext context) : base(context)
        {

        }

        public void ChangeRole(int userID, string role)
        {
            User u = Context.Users.Find(userID);
            u.Role = role;

            Context.SaveChanges();
        }
    }
}

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
using System.Data.SqlClient;

namespace DataAccess.Repository.Implementations
{
    public class TokenRepository : GenericRepository<Token>, ITokenRepository
    {
        public TokenRepository(PollAppDBContext context) : base(context)
        {
        }
        public void UpdateExpirationDate(string tokenString,DateTime expirationDate)
        {
            Token t = Context.Tokens.Find(11);
            
            
           // t.TokenString = tokenString;
            t.ExpirationDate = expirationDate;
            Context.SaveChanges();
        }

    }
}

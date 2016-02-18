/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * History:
 * 09.02.2016    Miron George       Created interface.
 */
using Entities;
using System;

namespace DataAccess.Repository.Interfaces
{
    public interface ITokenRepository : IGenericRepository<Token>
    {
        void UpdateExpirationDate(int id, DateTime expirationDate);
        void UpdateToken(int id, DateTime createdDate, DateTime expirationDate, string tokenString);
    }
}

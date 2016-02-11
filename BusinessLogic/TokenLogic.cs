/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  logic. 
 *
 * History:
 * 10.02.2016    Miron George       Created class.
 */
using DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class TokenLogic
    {
        private IDataAccess _dataAccess;

        public TokenLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba TokenLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Token> GetAll()
        {
            return _dataAccess.TokenRepository.GetAll().ToList();
        }
        public void AddForm(Token token)
        {
            _dataAccess.TokenRepository.Add(token);
        }
        public Token GetToken(int id)
        {
            return _dataAccess.TokenRepository.FindFirstBy(token => token.TokenID == id);
        }
        public void DeleteToken(Token token)
        {
            _dataAccess.TokenRepository.Delete(token);
        }
    }
}

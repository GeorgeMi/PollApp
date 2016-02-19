/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  Token logic. 
 *
 * History:
 * 10.02.2016    Miron George       Created class.
 */
using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
        public void AddToken(Token token)
        {
            _dataAccess.TokenRepository.Add(token);
        }

        public Token GetToken(int id)
        {
            return _dataAccess.TokenRepository.FindFirstBy(token => token.TokenID == id);
        }
        public int GetTokenID(string tokenString)
        {
            return _dataAccess.TokenRepository.FindFirstBy(token => token.TokenString == tokenString).TokenID;
        }
        public Token GetTokenByUserID(int id)
        {
            return _dataAccess.TokenRepository.FindFirstBy(token => token.UserID == id);
        }

        public string GetRoleByToken(string tokenString)
        {
            int userID = _dataAccess.TokenRepository.FindFirstBy(token => token.TokenString == tokenString).UserID;
            return _dataAccess.UserRepository.FindFirstBy(user => user.UserID == userID).Role;
        }

        public string UpdateToken(int id, string username)
        {
            Token t;
            DateTime expirationDate;
            DateTime createdDate;
            string text;
            MD5 md5;
            byte[] textToHash;
            byte[] result;

            try
            {
                //daca exista un token pentru user preiau obiectul
                t = GetTokenByUserID(id);

                createdDate = DateTime.Now;
                expirationDate = DateTime.Now.AddHours(3);

                //creez token string
                text = t.TokenID.ToString() + username + createdDate.ToString();

                md5 = new MD5CryptoServiceProvider();
                textToHash = Encoding.Default.GetBytes(text);
                result = md5.ComputeHash(textToHash);

                //Convert result back to string.
                text = BitConverter.ToString(result);

                try
                {
                    //verificare update
                    _dataAccess.TokenRepository.UpdateToken(t.TokenID, createdDate, expirationDate, text);
                }
                catch (Exception ex)
                {
                    return ex.InnerException.InnerException.Message;
                }
                return text;

            }
            catch
            {
                t = new Token();
                t.UserID = id;

                t.CreatedDate = DateTime.Now;
                t.ExpirationDate = t.CreatedDate.AddHours(3);

                //creez token string
                text = t.TokenID.ToString() + username + t.CreatedDate.ToString();

                md5 = new MD5CryptoServiceProvider();
                textToHash = Encoding.Default.GetBytes(text);
                result = md5.ComputeHash(textToHash);

                //Convert result back to string.
                t.TokenString = BitConverter.ToString(result);

                try
                {
                    //verificare inserare
                    _dataAccess.TokenRepository.Add(t);
                }
                catch (Exception ex)
                {
                    return ex.InnerException.InnerException.Message;
                }

                return t.TokenString;
            }
        }
        public DateTime GetTokenExpirationDate(string tokenString)
        {
            return _dataAccess.TokenRepository.FindFirstBy(token => token.TokenString == tokenString).ExpirationDate;
        }

        public void UpdateTokenExpirationDate(string tokenString)
        {
            int id = GetTokenID(tokenString);
            _dataAccess.TokenRepository.UpdateExpirationDate(id, DateTime.Now.AddHours(3));
        }

    }
}

﻿/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  logic. 
 *
 * History:
 * 12.02.2016    Miron George       Created class and implemented methods.
 */
using DataAccess;
using DataTransferObject;
using System;

namespace BusinessLogic
{
    public class AuthLogic
    {
        private IDataAccess _dataAccess;
        private TokenLogic TokenLogic;
        private UserLogic UserLogic;
        private UserDTO userDTO = new UserDTO() { Username = "george", Password = "pass" };

        public AuthLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
            TokenLogic = new TokenLogic(_dataAccess);
            UserLogic = new UserLogic(_dataAccess);
        }
        private int Validate(string username, string password)
        {
            //verifica daca in baza de date exista un tuplu ce corespunde cu datele introduse
            try
            {
                return _dataAccess.UserRepository.FindFirstBy(user => user.Username == username && user.Password == password).UserID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public string Authenticate(string username, string password)
        {
            //daca userul si pass carespund se updateaza tokenul in baza de date si se intoarce stringul updatat
            int userID = Validate(username, password);

            if (userID == -1)
            {
                return "Invalid username or password";
            }
            else
            {
                string token = TokenLogic.UpdateToken(userID, username);
                return token;
            }
        }


        public string Register(string username, string password, string email)
        {
            //verifica disponibilitatea numelui si introduce un nou user in baza de date
            try
            {
                UserLogic.AddUser(username, password, email);
            }
            catch
            {
                return "Name already exists";
            }
            int userID = Validate(username, password);
            if (userID == -1)
            {
                return "register failed";
            }
            else
            {
                return "register successfully";
            }
        }

        public bool VerifyTokenDate(string tokenString)
        {
            //verifica tokenul din baza de date. Daca nu exista sau este expirat->eroare. 
            //Altfel se updateaza data de expirare si se intoarce ok
            DateTime expirationDate = TokenLogic.GetTokenExpirationDate(tokenString);

            if (expirationDate.CompareTo(DateTime.Now) != 1)
            {
                return false;
            }
            else
            {
                TokenLogic.UpdateTokenExpirationDate(tokenString);
                return true;
            }
        }
    }
}

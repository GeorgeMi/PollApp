/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  User logic. 
 *
 * History:
 * 10.02.2016    Miron George       Created class.
 */
using DataAccess;
using DataTransferObject;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLogic
{
    public class UserLogic
    {
        private IDataAccess _dataAccess;
        private UserDTO userDTO = new UserDTO() { Username = "george", Password="pass" };

        public UserLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<User> GetAll()
        {
            return _dataAccess.UserRepository.GetAll().ToList();
        }
        public void AddUser(string username, string password,string email)
        {
            User user = new User() { Username = username, Password = password, Email = email, Role = "user" };
            _dataAccess.UserRepository.Add(user);
        }
        public User GetUser(int id)
        {
            return _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
        }
         public void DeleteUser(int id)
        {
           User u= _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.UserRepository.Delete(u);
        }
        public void DeleteUser(User u)
        {
            _dataAccess.UserRepository.Delete(u);
        }
        public int GetID(string username)
        {
            return _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
        }
       


      
    }
}

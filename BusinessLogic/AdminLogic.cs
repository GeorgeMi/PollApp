/* Copyright (C) Miron George - All Rights Reserved
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* Written by Miron George <george.miron2003@gmail.com>, 2016
* 
* Role:
*   Admin logic. 
*
* History:
* 12.02.2016    Miron George       Created class.
*/

using DataAccess;
using DataTransferObject;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class AdminLogic : UserLogic
    {
        private IDataAccess _dataAccess;
        public AdminLogic(IDataAccess objDataAccess) : base(objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<UserDetailDTO> GetAllUsers()
        {
            //returneaza lista cu toti userii
            List<User> userList = _dataAccess.UserRepository.GetAll().ToList();
            List<UserDetailDTO> userDtoList = new List<UserDetailDTO>();
            UserDetailDTO userDTO;

            foreach (User u in userList)
            {
                userDTO = new UserDetailDTO();
                userDTO.Email = u.Email;
                userDTO.Password = u.Password;
                userDTO.Role = u.Role;
                userDTO.Username = u.Username;
                userDTO.UserID = u.UserID;

                userDtoList.Add(userDTO);
            }

            return userDtoList;
        }
        public void AddUser(UserDetailDTO userDTO)
        {
            //adauga un user
            User user = new User() { Username = userDTO.Username, Password = userDTO.Password, Email = userDTO.Email, Role = userDTO.Role };
            _dataAccess.UserRepository.Add(user);
        }
        public UserDetailDTO GetUser(int id)
        {
            //gaseste user dupa id
            User user = _dataAccess.UserRepository.FindFirstBy(u => u.UserID == id);
            UserDetailDTO userDTO = new UserDetailDTO();

            userDTO.Username = user.Username;
            userDTO.Password = user.Password;
            userDTO.Email = user.Email;
            userDTO.Role = user.Role;
            userDTO.UserID = user.UserID;

            return userDTO;
        }
        public void DeleteUser(int id)
        {
            //sterge user dupa id
            User u = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.UserRepository.Delete(u);
        }
        public void DeleteUser(UserDetailDTO userDTO)
        {
            //sterge user
            User user = new User() { Username = userDTO.Username, Password = userDTO.Password, Email = userDTO.Email, Role = userDTO.Role };

            _dataAccess.UserRepository.Delete(user);
        }
        public int GetUserID(string username)
        {
            //cauta id-ul userului dupa username
            return _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
        }
        public void PromoteUser(int id)
        {
            //user devine admin
            User u = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.UserRepository.ChangeRole(id, "admin");
        }
        public void DemoteUser(int id)
        {
            //admin devine user
            User u = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.UserRepository.ChangeRole(id, "user");
        }

    }
}

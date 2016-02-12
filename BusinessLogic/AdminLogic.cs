using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AdminLogic:UserLogic
    {
        private IDataAccess _dataAccess;
        public AdminLogic(IDataAccess objDataAccess):base(objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<User> GetAllUsers()
        {
            //returneaza lista cu toti userii
            return _dataAccess.UserRepository.GetAll().ToList();
        }
        public void AddUser(string username, string password, string email)
        {
            //adauga un user
            User user = new User() { Username = username, Password = password, Email = email, Role = "user" };
            _dataAccess.UserRepository.Add(user);
        }
        public User GetUser(int id)
        {
            //gaseste user dupa id
            return _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
        }
        public void DeleteUser(int id)
        {
            //sterge user dupa id
            User u = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.UserRepository.Delete(u);
        }
        public void DeleteUser(User u)
        {
            //sterge user
            _dataAccess.UserRepository.Delete(u);
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

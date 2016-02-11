using DataAccess;
using Entities;
using System;

namespace ConsoleApplicationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dal = new DataAccess.DataAccess();
            BusinessLogic.BusinessLogic bl = new BusinessLogic.BusinessLogic(dal);

            User u = new User { Email = "email", Username = "geo", Password = "p", Role = "user" };
            bl.UserLogic.AddUser(u);
           


            //  User n = bl.UserLogic.GetUser(2);
            // bl.UserLogic.DeleteUser(3);


            //  Console.WriteLine(n.Username);




        }
    }
}

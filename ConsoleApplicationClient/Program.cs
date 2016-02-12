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
            try
            {
               // bl.UserLogic.AddUser(u);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.InnerException.Message);
                Console.WriteLine(DateTime.Now.ToString());
                Console.WriteLine(DateTime.Now.AddHours(2));
            }

           //string token=bl.AuthLogic.Authenticate("a","b");
            Console.WriteLine(bl.AuthLogic.VerifyTokenDate("7A-01-17-30-F4-59-39-FA-95-0C-5D-B2-91-27-7B-98"));
        }
    }
}

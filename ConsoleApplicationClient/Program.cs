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

              Console.WriteLine(bl.AuthLogic.Authenticate("geo","p"));
            // Console.WriteLine(bl.AuthLogic.VerifyTokenDate("A7-AB-5B-E1-B4-0D-FC-F4-C2-EC-9C-19-9B-91-D8-47"));
            //Console.WriteLine(bl.AdminLogic.GetUserID("geo"));
        }
    }
}

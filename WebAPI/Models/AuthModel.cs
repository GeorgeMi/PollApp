/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  Auth Model.
 *
 * History:
 * 14.02.2016    Miron George       Created class and implemented methods.
 */
using Microsoft.Practices.Unity;

namespace WebAPI.Models
{
    public class AuthModel
    {
        private BusinessLogic.BusinessLogic bl;
        public AuthModel()
        {
            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<BusinessLogic.BusinessLogic>();
            objContainer.RegisterType<DataAccess.IDataAccess, DataAccess.DataAccess>();
            bl = objContainer.Resolve<BusinessLogic.BusinessLogic>();
        }

        public string Authenticate(string username, string password)
        {
            return bl.AuthLogic.Authenticate(username, password);
        }

        public string GetRole(string username)
        {
            return bl.UserLogic.GetUserRole(username);
        }

        public bool VerifyToken(string token)
        {
            return bl.AuthLogic.VerifyTokenDate(token);
        }

        public bool VerifyAdminToken(string token)
        {
            return bl.AuthLogic.VerifyAdminToken(token);
        }

     }
}
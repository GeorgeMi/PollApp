using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ControllerModel
    {
        private BusinessLogic.BusinessLogic bl;
        public ControllerModel()
        {
            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<BusinessLogic.BusinessLogic>();
            objContainer.RegisterType<DataAccess.IDataAccess, DataAccess.DataAccess>();
            bl = objContainer.Resolve<BusinessLogic.BusinessLogic>();
        }
    }
}
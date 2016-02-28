/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  Category Model.
 *
 * History:
 * 25.02.2016    Miron George       Created class and implemented methods.
 */
using DataTransferObject;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CategoryModel
    {
        private BusinessLogic.BusinessLogic bl;
        public CategoryModel()
        {
            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<BusinessLogic.BusinessLogic>();
            objContainer.RegisterType<DataAccess.IDataAccess, DataAccess.DataAccess>();
            bl = objContainer.Resolve<BusinessLogic.BusinessLogic>();
        }

        public List<CategoryDTO> GetAllCategories()
        {
            try
            {
                return bl.CategoryLogic.GetAllCategories();
            }
            catch
            {
                return null;
            }
        }
        public bool AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                bl.CategoryLogic.AddCategory(categoryDTO);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(int categoryID)
        {
            try
            {
                bl.CategoryLogic.DeleteCategory(categoryID);
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
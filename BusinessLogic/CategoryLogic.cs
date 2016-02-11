/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  logic. 
 *
 * History:
 * 10.02.2016    Miron George       Created class.
 */
using DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryLogic
    {
        private IDataAccess _dataAccess;

        public CategoryLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba CategoryLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Category> GetAll()
        {
            return _dataAccess.CategoryRepository.GetAll().ToList();
        }
        public void AddCategory(Category category)
        {
            _dataAccess.CategoryRepository.Add(category);
        }
        public Category GetCategory(int id)
        {
            return _dataAccess.CategoryRepository.FindFirstBy(category => category.CategoryID == id);
        }
        public void DeleteCategory(Category category)
        {
            _dataAccess.CategoryRepository.Delete(category);
        }
    }
}

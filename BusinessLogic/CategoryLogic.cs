﻿using DataAccess;
using DataTransferObject;
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
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }

        public List<CategoryDTO> GetAllCategories()
        {
            //intoarce toate categoriile 
            List<CategoryDTO> listCategoryDTO = new List<CategoryDTO>();
            List<Category> listCategory = _dataAccess.CategoryRepository.GetAll().ToList();
            CategoryDTO categoryDTO;

            foreach(Category c in listCategory)
            {
                categoryDTO = new CategoryDTO();
                categoryDTO.CategoryID = c.CategoryID;
                categoryDTO.Name = c.Name;

                listCategoryDTO.Add(categoryDTO);
            }
            return listCategoryDTO;
        }
    }
}

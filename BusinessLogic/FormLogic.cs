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
    public class FormLogic
    {
        private IDataAccess _dataAccess;

        public FormLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba FormLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Form> GetAll()
        {
            return _dataAccess.FormRepository.GetAll().ToList();
        }
        public void AddForm(Form form)
        {
            _dataAccess.FormRepository.Add(form);
        }
        public Form GetForm(int id)
        {
            return _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);
        }
        public void DeleteForm(Form form)
        {
            _dataAccess.FormRepository.Delete(form);
        }
    }
}

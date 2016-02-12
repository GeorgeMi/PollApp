/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  User logic. 
 *
 * History:
 * 10.02.2016    Miron George       Created class.
 */
using DataAccess;
using DataTransferObject;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLogic
{
    public class UserLogic
    {
        private IDataAccess _dataAccess;
        private UserDTO userDTO = new UserDTO() { Username = "george", Password="pass" };

        public UserLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Form> GetAllForms(int id)
        {
            return _dataAccess.FormRepository.FindAllBy(form => form.FormID == id).ToList();
        }
        public void AddForm(Form form)
        {
            _dataAccess.FormRepository.Add(form);
        }
        public Form GetForm(int id)
        {
            return _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);
        }
        public void DeleteForm(int id)
        {
            Form f = _dataAccess.FormRepository.FindFirstBy(user => user.UserID == id);
            _dataAccess.FormRepository.Delete(f);
        }
        public void DeleteForm(Form f)
        {
            _dataAccess.FormRepository.Delete(f);
        }
        public int GetFormID(string title)
        {
            return _dataAccess.FormRepository.FindFirstBy(form => form.Title == title).UserID;
        }





    }
}

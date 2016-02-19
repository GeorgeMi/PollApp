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
       
        public UserLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Form> GetAllForms(int id)
        {
            //returneaza toate formurile unui user
            return _dataAccess.FormRepository.FindAllBy(form => form.UserID == id).ToList();
        }
        public void AddForm(Form form)
        {
            //adauga un form
            _dataAccess.FormRepository.Add(form);
        }
        public Form GetForm(int id)
        {
            //returneaza form dupa id
            return _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);
        }
        public void DeleteForm(int id)
        {
            //sterge un form dupa id
            Form f = _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);
            _dataAccess.FormRepository.Delete(f);
        }
        public void DeleteForm(Form f)
        {
            //sterge form
            _dataAccess.FormRepository.Delete(f);
        }
        public int GetFormID(string title)
        {
            //returneaza id-ul unui form
            return _dataAccess.FormRepository.FindFirstBy(form => form.Title == title).UserID;
        }

       





    }
}

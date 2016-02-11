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
    public class AnswerLogic
    {
        private IDataAccess _dataAccess;

        public AnswerLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba AnswerLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Answer> GetAll()
        {
            return _dataAccess.AnswerRepository.GetAll().ToList();
        }
        public void AddAnswer(Answer answer)
        {
            _dataAccess.AnswerRepository.Add(answer);
        }
        public Answer GetAnswer(int id)
        {
            return _dataAccess.AnswerRepository.FindFirstBy(answer => answer.AnswerID == id);
        }
        public void DeleteAnswer(Answer answer)
        {
            _dataAccess.AnswerRepository.Delete(answer);
        }
    }
}

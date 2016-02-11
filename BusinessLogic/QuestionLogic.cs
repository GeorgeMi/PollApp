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
    public class QuestionLogic
    {
        private IDataAccess _dataAccess;

        public QuestionLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba QuestionLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }
        public List<Question> GetAll()
        {
            return _dataAccess.QuestionRepository.GetAll().ToList();
        }
        public void AddQuestion(Question question)
        {
            _dataAccess.QuestionRepository.Add(question);
        }
        public Question GetQuestion(int id)
        {
            return _dataAccess.QuestionRepository.FindFirstBy(question => question.QuestionID == id);
        }
        public void DeleteQuestion(Question question)
        {
            _dataAccess.QuestionRepository.Delete(question);
        }
    }
}


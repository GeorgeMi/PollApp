using DataAccess;
using DataTransferObject;
using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BusinessLogic
{
    public class FormLogic
    {
        private IDataAccess _dataAccess;

        public FormLogic(IDataAccess objDataAccess)
        {
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }

        public List<FormDTO> GetUserForms(string username)
        {
            //returnez toate formurile unui user
            int userID = _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
            List<Form> formList = _dataAccess.FormRepository.FindAllBy(form => form.UserID == userID).ToList();
            List<FormDTO> formDtoList = new List<FormDTO>();
            FormDTO formDTO;

            foreach (Form f in formList)
            {
                formDTO = new FormDTO();
                formDTO.Title = f.Title;
                formDTO.State = f.State;
                formDTO.CreatedDate = f.CreatedDate.ToString();
                formDTO.Deadline = f.Deadline.ToString();
                formDTO.Category = _dataAccess.CategoryRepository.FindFirstBy(category => category.CategoryID == f.CategoryID).Name;
                formDTO.Username = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == f.UserID).Username;
                formDTO.Id = f.FormID;
                formDtoList.Add(formDTO);
            }

            return formDtoList;
        }

        public List<FormDTO> GetAllForms()
        {
            //returneaza toate formurile 
            List<Form> formList = _dataAccess.FormRepository.GetAll().ToList();
            List<FormDTO> formDtoList = new List<FormDTO>();
            FormDTO formDTO;

            foreach (Form f in formList)
            {
                formDTO = new FormDTO();
                formDTO.Title = f.Title;
                formDTO.State = f.State;
                formDTO.CreatedDate = f.CreatedDate.ToString();
                formDTO.Deadline = f.Deadline.ToString();
                formDTO.Category = _dataAccess.CategoryRepository.FindFirstBy(category => category.CategoryID == f.CategoryID).Name;
                formDTO.Username = _dataAccess.UserRepository.FindFirstBy(user => user.UserID == f.UserID).Username;
                formDTO.Id = f.FormID;
                formDtoList.Add(formDTO);
            }

            return formDtoList;

        }

        public FormDetailDTO GetContentForm(int id)
        {
            FormDetailDTO formDTO = new FormDetailDTO();
            Form f = _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);

            formDTO.Title = f.Title;
            formDTO.CreatedDate = f.CreatedDate.ToString();
            formDTO.Deadline = f.Deadline.ToString();
            formDTO.State = f.State;
            formDTO.Category = GetCategory(f.CategoryID);
            formDTO.Questions = GetAllQuestionFromForm(id);
            formDTO.Username = GetUsername(f.UserID);
            formDTO.Id = f.FormID;
            return formDTO;

        }
        public string GetUsername(int id)
        {
            //cauta numele userului dupa id
            return _dataAccess.UserRepository.FindFirstBy(user => user.UserID == id).Username;
        }
        public List<QuestionDTO> GetAllQuestionFromForm(int id)
        {
            List<QuestionDTO> questionDTOList = new List<QuestionDTO>();
            QuestionDTO questionDTO;
            List<Question> questionList = _dataAccess.QuestionRepository.FindAllBy(question => question.FormID == id).ToList();

            foreach (Question q in questionList)
            {
                questionDTO = new QuestionDTO();
                questionDTO.Question = q.Content;
                questionDTO.QuestionID = q.QuestionID;
                questionDTO.Answers = GetAllAnswerFromQuestion(q.QuestionID);


                questionDTOList.Add(questionDTO);

            }
            return questionDTOList;
        }

        public List<AnswerDTO> GetAllAnswerFromQuestion(int id)
        {
            //returnez toate raspunsurile unei intrebari

            List<AnswerDTO> answerDTOList = new List<AnswerDTO>();
            AnswerDTO answerDTO;
            List<Answer> answerList = _dataAccess.AnswerRepository.FindAllBy(answer => answer.QuestionID == id).ToList();

            foreach (Answer a in answerList)
            {
                answerDTO = new AnswerDTO();
                answerDTO.Answer = a.Content;
                answerDTO.AnswerID = a.AnswerID;
                answerDTOList.Add(answerDTO);
            }
            return answerDTOList;
        }


        public string GetCategory(int id)
        {
            //
            return _dataAccess.CategoryRepository.FindFirstBy(cat => cat.CategoryID == id).Name;
        }

        public int GetCategoryID(string name)
        {
            //
            return _dataAccess.CategoryRepository.FindFirstBy(cat => cat.Name == name).CategoryID;
        }

        public int GetQuestionID(string content,int formID)
        {
            //
            return _dataAccess.QuestionRepository.FindFirstBy(q => q.Content == content && q.FormID == formID).QuestionID;
        }

        public void AddCategory(string name)
        {
            //adauga o categorie
            Category category = new Category();
            category.Name = name;
            _dataAccess.CategoryRepository.Add(category);
        }

        public void AddForm(FormDetailDTO formDTO)
        {
            //add form
            CultureInfo provider = CultureInfo.InvariantCulture;
         // string format = "ddd MMM d HH:mm:ss GMT";

            Form form = new Form() {CreatedDate = DateTime.Now, Deadline = DateTime.Now.AddDays(7*Int32.Parse(formDTO.Deadline)), State = formDTO.State, Title = formDTO.Title };
            Question q;
            Answer a;
            form.CategoryID = GetCategoryID(formDTO.Category);
            form.UserID = GetUserID(formDTO.Username);

            _dataAccess.FormRepository.Add(form);

            //add questions
            int formID = GetFormID(form.Title,form.UserID,form.CreatedDate); //preiau id-ul formului
            int questionID;
            foreach (QuestionDTO questionDTO in formDTO.Questions)
            {
                q = new Question();
                q.FormID = form.FormID;
                q.Content = questionDTO.Question;
                q.NrVotes = 0;

                _dataAccess.QuestionRepository.Add(q);

                //add answer
                questionID = GetQuestionID(q.Content,formID);
                foreach (AnswerDTO answerDTO in questionDTO.Answers)
                {
                    a = new Answer();
                    a.Content = answerDTO.Answer;
                    a.NrVotes = 0;
                    a.QuestionID = questionID;

                    _dataAccess.AnswerRepository.Add(a);
                }
            }
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

        public int GetFormID(string title, int userID, DateTime createdDate)
        {
            DateTime date = createdDate.AddSeconds(-5);
            //returneaza id-ul unui form
            return _dataAccess.FormRepository.FindFirstBy(form => form.Title == title && form.UserID == userID && form.CreatedDate >= date).FormID;
        }

        public int GetUserID(string username)
        {
            //returneaza id-ul unui user
            return _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
        }

        public int FormIdCreatedbyUserId(int formID, string userToken)
        {
            //returneaza id-ul formului daca formul apartine de userul care are tokenul usertToken
            int userID = _dataAccess.TokenRepository.FindFirstBy(token => token.TokenString == userToken).UserID;
            return _dataAccess.FormRepository.FindFirstBy(form => form.FormID == formID && form.UserID == userID).FormID;
        }


    }
}

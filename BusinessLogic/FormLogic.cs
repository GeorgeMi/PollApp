using DataAccess;
using DataTransferObject;
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
            //primesc obiectul, nu e treaba UserLogic ce dataAccess se foloseste
            //unity are grija de dependency injection

            _dataAccess = objDataAccess;
        }

        public List<Form> Form(int id)
        {
            //returnez toate formurile unui user
            return _dataAccess.FormRepository.FindAllBy(form => form.UserID == id).ToList();
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
                formDTO.CreatedDate = f.CreatedDate;
                formDTO.Deadline = f.Deadline;
                formDTO.Category = _dataAccess.CategoryRepository.FindFirstBy(category => category.CategoryID == f.CategoryID).Name;
                formDtoList.Add(formDTO);
            }

            return formDtoList;

        }

        public FormDetailDTO GetContentForm(int id)
        {
            FormDetailDTO formDTO = new FormDetailDTO();
            Form f = _dataAccess.FormRepository.FindFirstBy(form => form.FormID == id);

            formDTO.Title = f.Title;
            formDTO.CreatedDate = f.CreatedDate;
            formDTO.Deadline = f.Deadline;
            formDTO.State = f.State;
            formDTO.Category = GetCategory(f.CategoryID);
            formDTO.Questions = GetAllQuestionFromForm(id);
            formDTO.Username = GetUsername(f.UserID);

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

        public int GetQuestionID(string content)
        {
            //
            return _dataAccess.QuestionRepository.FindFirstBy(q => q.Content == content).QuestionID;
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
            Form form = new Form() { CreatedDate = formDTO.CreatedDate, Deadline = formDTO.Deadline, State = formDTO.State, Title = formDTO.Title };
            Question q;
            Answer a;
            form.CategoryID = GetCategoryID(formDTO.Category);
            form.UserID = GetUserID(formDTO.Username);

            _dataAccess.FormRepository.Add(form);

            //add questions
            int formID = GetFormID(form.Title);
            int questionID;
            foreach (QuestionDTO questionDTO in formDTO.Questions)
            {
                q = new Question();
                q.FormID = form.FormID;
                q.Content = questionDTO.Question;
                q.NrVotes = 0;

                _dataAccess.QuestionRepository.Add(q);

                //add answer
                questionID = GetQuestionID(q.Content);
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

        public int GetUserID(string username)
        {
            //returneaza id-ul unui form
            return _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
        }


    }
}

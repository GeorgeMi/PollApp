using DataAccess;
using DataTransferObject;
using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public VoteResultDetailDTO GetDetailResultForm(int id)
        {
            //returneaza rezultatul unui sondaj cu tot cu intrebari si raspunsuri
            VoteResultDetailDTO voteResult = new VoteResultDetailDTO();
            voteResult.Questions = new List<VoteQuestionResultDetailDTO>();
            voteResult.Title = _dataAccess.FormRepository.FindFirstBy(f => f.FormID == id).Title;

            List<Question> questionList = _dataAccess.QuestionRepository.FindAllBy(q => q.FormID == id).ToList();
            List<Answer> answerList;

            VoteQuestionResultDetailDTO questionDTO;
            VoteAnswerDetailResultDTO answerDTO;
            
            foreach (Question q in questionList)
            {
                questionDTO = new VoteQuestionResultDetailDTO();
                questionDTO.Answers = new List<VoteAnswerDetailResultDTO>();
                questionDTO.Question = q.Content;
                answerList = _dataAccess.AnswerRepository.FindAllBy(a => a.QuestionID == q.QuestionID).ToList();

                foreach (Answer a in answerList)
                {
                    answerDTO = new VoteAnswerDetailResultDTO();
                    answerDTO.Answer = a.Content;
                    answerDTO.AnswerNrVotes = Decimal.ToInt32(a.NrVotes);
                    questionDTO.Answers.Add(answerDTO);
                }

                voteResult.NrVotes = Decimal.ToInt32(q.NrVotes);
                voteResult.Questions.Add(questionDTO);
            }
           
            return voteResult;
        }

        public List<FormDTO> GetCategoryForms(int categoryID, string token)
        {
            //returneaza toate formurile dintr-o categorie
            List<Form> formList = _dataAccess.FormRepository.FindAllBy(f=>f.CategoryID == categoryID).ToList();
            List<FormDTO> formDtoList = new List<FormDTO>();
            FormDTO formDTO;

            int userID = _dataAccess.TokenRepository.FindFirstBy(user => user.TokenString == token).UserID;

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
                formDTO.Voted = true;

                try
                {
                    userID = _dataAccess.VotedFormsRepository.FindFirstBy(voted => voted.FormID == f.FormID && voted.UserID == userID).UserID;
                }
                catch
                {
                    //daca userul a votat sondajul deja, nu il va mai putea vota
                    formDTO.Voted = false;
                }

                formDtoList.Add(formDTO);
            }

            return formDtoList;
        }

        public List<FormDTO> GetVotedForms(string username)
        {
            //returneaza toate formurile votate de catre un user
            int userID = _dataAccess.UserRepository.FindFirstBy(user => user.Username == username).UserID;
            List<Form> formList=new List<Form>();
            List<VotedForms> votedFormsList = _dataAccess.VotedFormsRepository.FindAllBy(voted=>voted.UserID == userID).ToList();
            List<FormDTO> formDtoList = new List<FormDTO>();
            FormDTO formDTO;
            Form form;

            foreach (VotedForms votedForm in votedFormsList)
            {
                form = _dataAccess.FormRepository.FindFirstBy(f => f.FormID == votedForm.FormID);
                formList.Add(form);
            }
          
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
                formDTO.Voted = true;
                                                
                formDtoList.Add(formDTO);
            }

            return formDtoList;
        }

        public List<FormDTO> GetAllForms(string token)
        {
            //returneaza toate formurile 
            List<Form> formList = _dataAccess.FormRepository.GetAll().ToList();
            List<FormDTO> formDtoList = new List<FormDTO>();
            FormDTO formDTO;

            int userID = _dataAccess.TokenRepository.FindFirstBy(user => user.TokenString == token).UserID;
           
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
                formDTO.Voted = true;

                try
                {
                  userID = _dataAccess.VotedFormsRepository.FindFirstBy(voted => voted.FormID == f.FormID && voted.UserID == userID).UserID;
                }
                catch
                { 
                    //daca userul a votat sondajul deja, nu il va mai putea vota
                    formDTO.Voted = false;
                }

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

        public int GetQuestionID(string content, int formID)
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

            Form form = new Form() { CreatedDate = DateTime.Now, Deadline = DateTime.Now.AddDays(7 * Int32.Parse(formDTO.Deadline)), State = formDTO.State, Title = formDTO.Title };
            Question q;
            Answer a;
            form.CategoryID = GetCategoryID(formDTO.Category);
            form.UserID = GetUserID(formDTO.Username);

            _dataAccess.FormRepository.Add(form);

            //add questions
            int formID = GetFormID(form.Title, form.UserID, form.CreatedDate); //preiau id-ul formului
            int questionID;
            foreach (QuestionDTO questionDTO in formDTO.Questions)
            {
                q = new Question();
                q.FormID = form.FormID;
                q.Content = questionDTO.Question;
                q.NrVotes = 0;

                _dataAccess.QuestionRepository.Add(q);

                //add answer
                questionID = GetQuestionID(q.Content, formID);
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

        public VoteResultDTO Vote(VoteListDTO voteListDTO, string token)
        {
            int userID = _dataAccess.UserRepository.FindFirstBy(user => user.Username == voteListDTO.Username).UserID;
            int questionID;
            
            //testeaza daca tokenul si userul care a votat coincid
            if (userID == _dataAccess.TokenRepository.FindFirstBy(user => user.TokenString == token).UserID)
            {
                questionID= voteListDTO.Answers[0].Question;

                int formID = _dataAccess.QuestionRepository.FindFirstBy(question => question.QuestionID == questionID).FormID;

                //incrementez numarul de voturi pentru fiecare intrebare si raspuns

                foreach (VoteDTO voteDTO in voteListDTO.Answers)
                {
                    _dataAccess.AnswerRepository.AddVote(voteDTO.Answer);
                    _dataAccess.QuestionRepository.AddVote(voteDTO.Question);
                }

                //preiau rezultatele din baza de date pentru fiecare intrebare si raspuns
                VoteResultDTO voteResult = new VoteResultDTO();
                voteResult.Questions = new List<VoteQuestionResultDTO>();

                VoteQuestionResultDTO voteQuestionResult;
                VoteAnswerResultDTO voteAnswerResult;
                List<Answer> listAnswer;

                foreach (VoteDTO voteDTO in voteListDTO.Answers)
                {
                    listAnswer = _dataAccess.AnswerRepository.FindAllBy(answer => answer.QuestionID == voteDTO.Question).ToList();
                    voteQuestionResult = new VoteQuestionResultDTO();

                    //id-ul si nr voturi intrebare
                    voteQuestionResult.QuestionID = voteDTO.Question;
                    voteQuestionResult.QuestionNrVotes = Decimal.ToInt32(_dataAccess.QuestionRepository.FindFirstBy(question => question.QuestionID == voteDTO.Question).NrVotes);
                    voteQuestionResult.Answers = new List<VoteAnswerResultDTO>();

                    foreach (Answer a in listAnswer)
                    {
                        voteAnswerResult = new VoteAnswerResultDTO();
                        voteAnswerResult.AnswerID = a.AnswerID;
                        voteAnswerResult.AnswerNrVotes = Decimal.ToInt32(_dataAccess.AnswerRepository.FindFirstBy(answer => answer.AnswerID == a.AnswerID).NrVotes);

                        voteQuestionResult.Answers.Add(voteAnswerResult);
                    }

                    voteResult.Questions.Add(voteQuestionResult);
                }

                VotedForms voted = new VotedForms();
                voted.UserID = userID;
                voted.FormID = formID;

                //adauga sondajul in lista sondajelor votate de userul respectiv
                _dataAccess.VotedFormsRepository.Add(voted);
                return voteResult;
            }
            else throw new Exception("Poll already voted!");

        }
          
    }
}

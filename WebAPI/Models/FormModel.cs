/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 *  Form Model.
 *
 * History:
 * 25.02.2016    Miron George       Created class and implemented methods.
 */
using DataTransferObject;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class FormModel
    {
        private BusinessLogic.BusinessLogic bl;
        public FormModel()
        {
            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<BusinessLogic.BusinessLogic>();
            objContainer.RegisterType<DataAccess.IDataAccess, DataAccess.DataAccess>();
            bl = objContainer.Resolve<BusinessLogic.BusinessLogic>();
        }

        public List<FormDTO> GetAllForms(string token)
        {
            try
            {
                return bl.FormLogic.GetAllForms(token);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public VoteResultDetailDTO GetDetailResultForm(int id)
        {
            try
            {
                return bl.FormLogic.GetDetailResultForm(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<FormDTO> GetUserForms(string username)
        {
            try
            {
                return bl.FormLogic.GetUserForms(username);
            }
            catch
            {
                return null;
            }
        }

        public List<FormDTO> GetVotedForms(string username)
        {
            try
            {
                return bl.FormLogic.GetVotedForms(username);
            }
            catch
            {
                return null;
            }
        }

        public FormDetailDTO GetContentForm(int id)
        {
            try
            {
                FormDetailDTO formDTO = bl.FormLogic.GetContentForm(id);
                return formDTO;
                
            }
            catch
            {
                return null;
            }
        }

        internal List<FormDTO> GetCategoryForms(int categoryID,string token)
        {
            try
            {
                return bl.FormLogic.GetCategoryForms(categoryID, token);
            }
            catch
            {
                return null;
            }
        }

        public bool AddForm(FormDetailDTO formDTO)
        {
            try
            {
                bl.FormLogic.AddForm(formDTO);
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
          
        }

        public bool DeleteForm(int formID)
        {
            try
            {
                bl.FormLogic.DeleteForm(formID);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool FormIdCreatedbyUserId(int formID, string userToken)
        {
            try
            {
                bl.FormLogic.FormIdCreatedbyUserId(formID, userToken);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public VoteResultDTO Vote(VoteListDTO voteDTO, string token)
        {
            try
            {
               return bl.FormLogic.Vote(voteDTO,token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
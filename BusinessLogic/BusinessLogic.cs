/* Copyright (C) Miron George - All Rights Reserved
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* Written by Miron George <george.miron2003@gmail.com>, 2016
* 
* Role:
*   All Logic. 
*
* History:
* 12.02.2016    Miron George       Created class.
*/
using DataAccess;

namespace BusinessLogic
{
    public class BusinessLogic
    {
        public FormLogic FormLogic;
        public TokenLogic TokenLogic;
        public UserLogic UserLogic;
        public AuthLogic AuthLogic;
        public CategoryLogic CategoryLogic;

        public BusinessLogic(IDataAccess _dataAccess)
        {
            FormLogic = new FormLogic(_dataAccess);
            TokenLogic = new TokenLogic(_dataAccess);
            UserLogic = new UserLogic(_dataAccess);
            AuthLogic = new AuthLogic(_dataAccess);
            CategoryLogic = new CategoryLogic(_dataAccess);
    }



    }
}

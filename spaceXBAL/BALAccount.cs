using CDModels;
using spaceXDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceXBAL
{
   public class BALAccount
    {
        public loggedinUser LoginUser(LoginBindingModel obj)
        {
            try
            {
                DALAccount accountObj = new DALAccount();
                return accountObj.Login(obj);
            }
            catch (Exception ex)
            {
               

            }
            return null;
        }

    }
}

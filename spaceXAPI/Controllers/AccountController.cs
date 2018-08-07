using CDModels;
using spaceXBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace spaceXAPI.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LoginUser()
        {
            return Content(HttpStatusCode.OK, "Ashwani");
        }
        [HttpPost]
        [Route("api/Account/LoginUser")]
        public IHttpActionResult LoginUser(LoginBindingModel obj)
        {
            //Encrypt password
            string strAPIKey = CommonLib.FetchMD5(CommonLib.FetchMD5("Ashwani") + obj.Salt);
            loggedinUser loggedinUser = new loggedinUser();
            BALAccount accountObj = new BALAccount();
            loggedinUser = accountObj.LoginUser(obj);
            
            if (loggedinUser != null && strAPIKey==obj.APIKey)
                return Content(HttpStatusCode.OK, loggedinUser);
            else return NotFound();
        }
    }
}

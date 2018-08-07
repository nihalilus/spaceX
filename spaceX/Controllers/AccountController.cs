using CDModels;
using spaceX.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace spaceX.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> login(LoginBindingModel obj, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var client = new HttpClient())
                {
                    string strSalt = CommonLib.FetchRandStr(3);
                    //Encrypt password
                    string strAPIKey = CommonLib.FetchMD5(CommonLib.FetchMD5("Ashwani") + strSalt);
                    obj.APIKey = strAPIKey;
                    obj.Salt = strSalt;
                    client.BaseAddress = new Uri(spaceX.utilities.configuration.ConfigurationKeys.apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method  
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/account/LoginUser", obj);
                    if (response.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response.Content.ReadAsStringAsync();
                        loggedinUser loggedinUserObj = configuration.JsonDeserialize<loggedinUser>(customerJsonString);

                        Session["loggedinUser"] = loggedinUserObj;

                        
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email ID/Password is Incorrect");
                        return View(obj);

                    }

                }
            }
            catch (Exception ex)
            {
               
            }

            return View();
        }
    }
}
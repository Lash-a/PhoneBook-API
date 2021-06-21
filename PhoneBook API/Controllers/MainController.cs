using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace PhoneBook_API.Controllers
{
    public class MainController : ApiController
    {
        protected User AuthUser;

        public MainController()
        {
            var identity = (ClaimsIdentity)User.Identity;

            if (identity.Claims.Count() > 0)
            {
                AuthUser = JsonConvert.DeserializeObject<User>(identity.Claims.FirstOrDefault(x => x.Type == "AuthUser").Value);
            }
        }
    }

    public class User
    {
        public int UserID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserType { get; set; }
        public string Password { get; internal set; }

    }
}
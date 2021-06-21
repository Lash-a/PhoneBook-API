using Newtonsoft.Json;
using PhoneBook_API.Controllers;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
//using WebApiOwin.Controllers;

namespace WebApiOwin
{
    public class Authorization : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            var authUser = JsonConvert.DeserializeObject<User>(identity.Claims.FirstOrDefault(x => x.Type == "AuthUser").Value);
            //if (authUser.UserType != Roles) return false;

            return base.IsAuthorized(actionContext);
        }
    }
}
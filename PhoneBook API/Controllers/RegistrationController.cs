using PhoneBook_API.Models;
using PhoneBook_API.Repositories;
using System.Web.Http;

namespace PhoneBook_API.Controllers
{
    //public class RegistrationController: MainController
        public class RegistrationController : ApiController
    {

        [HttpPost]
        public IHttpActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            var registrationRepo = new RegistrationRepo();
            registrationRepo.RegisterUser(registrationModel);

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult CheckIfUserExists(UserRegistrationModel registrationModel)
        {
            var registrationRepo = new RegistrationRepo();
            int s = registrationRepo.CheckIfUserExists(registrationModel);
            //s -is mnishvneloba aris 1 an 0 imis mixedvit arsebobs tu ara gadacemuli emaili da paroli
            return Ok(s);
        }


        [HttpPost]
        public IHttpActionResult CheckIfEmailExists(UserRegistrationModel registrationModel)
        {
            var registrationRepo = new RegistrationRepo();
          
                int s = registrationRepo.CheckIfEmailExists(registrationModel);
                string errorMesseage = "Email Already Exists";
                if(s >= 1) {
                //throw new System.Exception("This Email Already Exists");
                return Ok(errorMesseage);
                }

                return Ok(s);
        }

    }
}
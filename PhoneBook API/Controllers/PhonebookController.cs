using PhoneBook_API.Models;
using PhoneBook_API.Repositories;
using System.Web.Http;


namespace PhoneBook_API.Controllers
{
    [Authorize]
    public class PhonebookController : MainController
    {
        [HttpPost]
        public IHttpActionResult SaveContact(PhoneBookModel phoneBook)
        {
            var phoneBookRepo = new PhonebookRepository();
            phoneBookRepo.SaveContact(phoneBook, AuthUser);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult DeleteContact([FromBody] int ID)
        {
            var phoneBookRepo = new PhonebookRepository();
            phoneBookRepo.DeleteContact(ID, AuthUser);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult EditContact(PhoneBookModel phoneBook)
        {
            var phoneBookRepo = new PhonebookRepository();
            phoneBookRepo.EditContact(phoneBook, AuthUser);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult GetAllContacts()
        {

            var phoneBookRepo = new PhonebookRepository();
            var contacts = phoneBookRepo.GetAllContacts(AuthUser);

            return Ok(contacts);
        }

    }
}




using System.Security.Cryptography;
using System.Text;
using KeyPass_Toshmatov.Classes;
using KeyPass_Toshmatov.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyPass_Toshmatov.Controllers
{
    public class UserController : Controller
    {
        private DatabaseManager databaseManager;

        public UserController()
        {
            this.databaseManager = new DatabaseManager();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromForm] string login, [FromForm] string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);  

                User? AuthUser = databaseManager.Users
                    .Where(x => x.Login == login && x.Password == hashedPassword) 
                    .FirstOrDefault();

                if (AuthUser == null)
                {
                    return StatusCode(401);
                }
                else
                {
                    string Token = JwtToken.Generate(AuthUser);
                    AuthUser.LastAuth = DateTime.Now;
                    databaseManager.SaveChanges();
                    return Ok(new { token = Token });
                }
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
    }
}
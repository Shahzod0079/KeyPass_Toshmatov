using KeyPass_Toshmatov.Classes;
using KeyPass_Toshmatov.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KeyPass_Toshmatov.Controllers
{
    [Route("/storage")]
    public class StorageController : Controller
    {
        private DatabaseManager databaseManager;

        public StorageController()
        {
            this.databaseManager = new DatabaseManager();
        }

        [Route("get")]
        [HttpGet]
        public ActionResult Get([FromHeader] string token)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                if (IdUser == null)
                    return StatusCode(401);

                var Storages = databaseManager.Storages
                    .Where(x => x.UserId == IdUser.Value)
                    .Select(s => new StorageDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Url = s.Url,
                        Login = s.Login,
                        Password = s.Password,
                    })
                    .ToList();
                return Ok(Storages);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("add")]
        [HttpPost]
        public ActionResult Add([FromHeader] string token, [FromBody] StorageDto storageDto)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                if (IdUser == null)
                    return StatusCode(401);

                Storage storage = new Storage
                {
                    Name = storageDto.Name,
                    Url = storageDto.Url,
                    Login = storageDto.Login,
                    Password = storageDto.Password,
                    UserId = IdUser.Value
                };

                databaseManager.Storages.Add(storage);
                databaseManager.SaveChanges();

                storageDto.Id = storage.Id;
                return StatusCode(200, storageDto);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("update")]
        [HttpPut]
        public ActionResult Update([FromHeader] string token, [FromBody] StorageDto storageDto)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                if (IdUser == null)
                    return StatusCode(401);

                Storage? uStorage = databaseManager.Storages
                    .Where(x => x.Id == storageDto.Id && x.UserId == IdUser.Value)
                    .FirstOrDefault();

                if (uStorage == null)
                    return StatusCode(404);

                uStorage.Name = storageDto.Name;
                uStorage.Url = storageDto.Url;
                uStorage.Login = storageDto.Login;
                uStorage.Password = storageDto.Password;

                databaseManager.SaveChanges();

                return StatusCode(200, storageDto);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("delete")]
        [HttpDelete]
        public ActionResult Delete([FromHeader] string token, [FromBody] int id)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);
                if (IdUser == null)
                    return StatusCode(401);

                Storage? uStorage = databaseManager.Storages
                    .Where(x => x.Id == id && x.UserId == IdUser.Value)
                    .FirstOrDefault();

                if (uStorage == null)
                    return StatusCode(404);

                databaseManager.Storages.Remove(uStorage);
                databaseManager.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
    }
}
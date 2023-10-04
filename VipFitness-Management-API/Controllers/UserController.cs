using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VipFitness_Management_API.Entities;
using VipFitness_Management_API.Models;

namespace VipFitness_Management_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       UserEntity userEntity = new UserEntity();


        private readonly ILogger<MyClass> _logger;

        public UserController(ILogger<MyClass> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<User> Get()
        {
            try
            {
                return userEntity.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir hata oluştu: {ErrorMessage}", ex.Message);
                throw; // Hatanın yukarıya fırlatılması
            }
           
        }


        [HttpGet("{id}")]
        public List<User> Get(int id)
        {
                return userEntity.FindUser(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public string Post([FromBody] User user)
        {
           return userEntity.AddUser(user);

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] User user)
        {
            return userEntity.UpdateUser(id, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userEntity.DeleteUser(id);
        }
    }
}

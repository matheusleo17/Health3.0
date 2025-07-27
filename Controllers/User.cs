using Care3._0.Data;
using Care3._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Care3._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public UserController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult<User> Register(User user)
        {
            var userLogin = _appDbContext.Users.Where(_ => _.Email == user.Email).FirstOrDefault();

            if(userLogin == null)
            {
                var HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = HashedPassword;
                _appDbContext.Add(user);
                _appDbContext.SaveChanges();

                return Ok(user);


            }
            else
            {
                return BadRequest("Ja existe um cadastro com esse email registrado.");
            }

        }
        [HttpPost]
        [Route("Login")]
        public ActionResult<User> Login(string email, string senha)
        {
            var getUserLogin = _appDbContext.Users.Where(_ => _.Email == email).FirstOrDefault();
            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, getUserLogin?.Password);

            if (getUserLogin != null && senhaValida == true)
            {
                return Ok("Login com sucesso");
            } else {
                return BadRequest("Dados de login invalidos");

            }
        }
    }
}

using Care3._0.Data;
using Care3._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Care3._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        private readonly IAuthService _authService;

        public UserController(AppDBContext appDbContext, IAuthService authService)
        {
            _appDbContext = appDbContext;
            _authService = authService;

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
                var token = _authService.GenerateToken(getUserLogin.Email);
                return Ok(new { token });
            } else {
                return BadRequest("Dados de login invalidos");

            }
        }
    }
}

using MangeAPI.Dto;
using MangeAPI.Model.Auth;
using MangeAPI.Repository.Repo;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MangeAPI.Controllers
{

    [Route("api/auth")]
    public class AuthController : ApiController
    {
        private readonly Repo_Auth _repo;

        public AuthController()
        {
            _repo = new Repo_Auth();
        }
        
        [HttpGet]
        public string Get()
        {
            return "ddcccddddd";
        }

        [Route("api/auth/Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.password);

            return StatusCode(HttpStatusCode.Created);
        }

        [Route("api/auth/login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {

            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromRepo == null) return Unauthorized();


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.user_id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT_PasswordToken"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
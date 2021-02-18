using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.UserController
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IMapper Mapper { get; set; }
        private IUsersInterface UsersService { get; set; }

        private AppSettings _appSettings { get; set; }
        public UserController(IMapper _mapper, IUsersInterface usersService, IOptions<AppSettings> appSettings)
        {

            Mapper = _mapper;
            UsersService = usersService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("createActivationCode/{idUser}")]
        public IActionResult CreateActivationCode(int idUser)
        {
            try
            {
                var activationCode = UsersService.CreateActivationKey();

                UsersService.AddActivationKeyToTable(activationCode, idUser);

                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("sendActivationCode/{idUser}")]
        public IActionResult SendActivationCode([FromBody] string Email, int idUser)
        {
            try
            {
                var activationCode = UsersService.CreateActivationKey();

                UsersService.AddActivationKeyToTable(activationCode, idUser);

                UsersService.SendActivationCode(Email, activationCode);

                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("activateUser")]
        public IActionResult ActivateUser([FromBody] UserActivationViewDto userActivationViewDto)
        {
            var userActivationView = Mapper.Map<UserActivationView>(userActivationViewDto);

            var user = new User(userActivationView.IdUser, userActivationView.Username, userActivationView.FirstName,
                                userActivationView.LastName, userActivationView.NrMatricol,
                                userActivationView.Email, userActivationView.IsAccountActive);

            try
            {
                //schimba numele functiei
                UsersService.Create(user, userActivationView.Password, userActivationView.ActivationCode);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]//ROUTE
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            try
            {
                var user = UsersService.Authenticate(userDto.Username, userDto.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                if (!user.IsAccountActive)
                    return BadRequest(new { message = "User is inactive" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.IdUser.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    IdUser = user.IdUser,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NrMatricol = user.NrMatricol,
                    IsAccountActive = user.IsAccountActive,
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        //sa scrie si parola veche
        [HttpPut("changePassword")]
        public IActionResult ChangePassword([FromBody]UserDto userDto)
        {
            string password = userDto.Password;
            // map dto to entity and set id
            var user = Mapper.Map<User>(userDto);

            try
            {
                // save 
                UsersService.ChangePassword(user, password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
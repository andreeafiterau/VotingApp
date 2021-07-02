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

        //[HttpPost("getRole")]

        //public IActionResult GetRole([FromBody] UserDto userDto)
        //{
        //    try
        //    {
        //        return Ok(UsersService.getRole(userDto.IdUser));
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}


        [HttpPost("sendActivationCode")]
        public IActionResult SendActivationCode([FromBody] UserDto userDto)
        {
            try
            {
                var activationCode = UsersService.CreateActivationKey();

                if(UsersService.IsUserActive(UsersService.FindUser(userDto.Email)))
                {
                    throw new Exception("The user is already active");
                }

                UsersService.AddActivationKeyToTable(activationCode, UsersService.FindUser(userDto.Email));

                UsersService.SendActivationCode(userDto.Email, activationCode);

                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("sendPasswordToken")]
        public IActionResult SendPasswordToken([FromBody] UserDto userDto)
        {
            try
            {
                var passwordToken = UsersService.CreateActivationKey();

                UsersService.AddPasswordTokenToTable(passwordToken, UsersService.FindUser(userDto.Email));

                UsersService.SendPasswordToken(userDto.Email, passwordToken);

                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("forgotPassword")]
        public IActionResult ForgotPasswordUpdate([FromBody] UserActivationViewDto userActivationViewDto)
        {
            
            User user = UsersService.FindUserForActivation(userActivationViewDto.Email);

            if (user!=null)
            {
                try
                {
                    //schimba numele functiei
                    UsersService.ForgotPasswordUpdate(user, userActivationViewDto.Password, userActivationViewDto.Code);


                    return Ok();
                }
                catch (Exception ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest("User is not activated");
            }
            
        }

        [HttpPut("activateUser")]
        public IActionResult ActivateUser([FromBody] UserActivationViewDto userActivationViewDto)
        {
            User user=UsersService.FindUserForActivation(userActivationViewDto.Email);

            try
            {
                //schimba numele functiei
                UsersService.ActivateUser(user, userActivationViewDto.Password, userActivationViewDto.Code);
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

                var userAdminView = UsersService.Authenticate(userDto.Username, userDto.Password);

                if (userAdminView.User == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                if (!userAdminView.User.IsAccountActive)
                    return BadRequest(new { message = "User is inactive" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userAdminView.User.IdUser.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    IdUser = userAdminView.User.IdUser,
                    Username = userAdminView.User.Username,
                    Email = userAdminView.User.Email,
                    FirstName = userAdminView.User.FirstName,
                    LastName = userAdminView.User.LastName,
                    NrMatricol = userAdminView.User.NrMatricol,
                    IsAccountActive = userAdminView.User.IsAccountActive,
                    Role= userAdminView.Role.RoleName,
                    College=userAdminView.Colleges[0].IdCollege,
                    Department=userAdminView.Departments[0].IdDepartment,
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
            string username = userDto.Username;
            // map dto to entity and set id
            //var user = Mapper.Map<User>(userDto);

            try
            {
                // save 
                UsersService.ChangePassword(username, password);
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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.AdminControllers.UsersComponentManager
{
    [ApiController]
    [Route("[controller]")]
    public class UsersComponentController : ControllerBase
    {
        
        private IMapper Mapper { get; set; }
        private  IUsersInterface UsersService {get;set;}

        private AppSettings _appSettings { get; set; }
        public UsersComponentController(IMapper _mapper,IUsersInterface usersService, IOptions<AppSettings> appSettings)
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

        [HttpPost("filter")]
        public IActionResult GetAll([FromBody] ObjectForUsersFilterDto objectForUsersFilterDto)
        {
            var objectForUsersFilter = Mapper.Map<ObjectForUsersFilter>(objectForUsersFilterDto);
            var user = UsersService.GetAllUsersForAdmin(objectForUsersFilter);
            var usersDtos = Mapper.Map<IList<UserAdminViewDto>>(user);
            return Ok(usersDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserAdminViewDto userAdminViewDto)
        {
            var userAdminView = Mapper.Map<UserAdminView>(userAdminViewDto);
           
            try
            {
                // save 
                UsersService.AddUsers(userAdminView);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UserAdminViewDto userAdminViewDto,int id)
        {
            // map dto to entity and set id
            var userAdminView = Mapper.Map<UserAdminView>(userAdminViewDto);
            userAdminView.User.IdUser = id;


            try
            {
                // save 
                UsersService.UpdateUsersForAdmin(userAdminView);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UsersService.DeleteUserForAdmin(id);
            return Ok();
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
        /***********************************************************************************
         * 
         * Authenticate method:
         *     + Return type: IActionResult.
         *     + @param userDto: first argument,type UserDto.
         *     + It is used to validate if a user exists in the database and the password is 
         *       correct.
         *     + HttpPost request.
         * 
         ***********************************************************************************/
        public IActionResult Authenticate([FromBody]UserDto userDto)
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

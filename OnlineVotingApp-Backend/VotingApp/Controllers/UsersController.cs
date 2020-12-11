using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class UsersController : ControllerBase
    {
        private IRepository<User> Repository { get; set; }

        private IUsersInterface _userService { get; set; }
        private IMapper Mapper { get; set; }

        private AppSettings _appSettings { get; set; }

        public UsersController(IRepository<User> _repository, IMapper _mapper, IUsersInterface userService,AppSettings appSettings)
        {
            Repository = _repository;
            Mapper = _mapper;
            _userService = userService;
            _appSettings = appSettings;
        }

        [HttpGet("{id}")]        
        public IActionResult Get(int id)
        {
            var user = Repository.GetByID(id);
            var userDto = Mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = Repository.GetAll();
            var userDtos = Mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDto userDto)
        {
            // map dto to entity
            var user = Mapper.Map<User>(userDto);

            try
            {
                // save 
                Repository.Insert(user);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = Mapper.Map<User>(userDto);
            user.IdUser = id;

            try
            {
                // save 
                Repository.Update(user);
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
            Repository.Delete(id);
            return Ok();
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
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

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
                IsActive = user.IsAccountActive,
                Token = tokenString
            });
        }
    }
}

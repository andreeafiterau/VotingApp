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

        private IRepository<Role> Repository { get; set; }

        private AppSettings _appSettings { get; set; }
        public UsersComponentController(IMapper _mapper,IUsersInterface usersService, IOptions<AppSettings> appSettings, IRepository<Role> roleRepo)
        {
            
            Mapper = _mapper;
            UsersService = usersService;
            _appSettings = appSettings.Value;
            Repository = roleRepo;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = Repository.GetAll();
                //var rolesDto = Mapper.Map<RoleDto>(roles);
                return Ok(roles);
            }
            catch(Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllUsers")]

        public IActionResult GetAllUsers()
        {
            try
            {
                var users=UsersService.GetAllUsers();
                var usersDto = Mapper.Map<IList<UserAdminViewDto>>(users);
                return Ok(usersDto);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("filter")]
        public IActionResult GetAll([FromBody] ObjectForUsersFilterDto objectForUsersFilterDto)
        {
            try
            {
                var objectForUsersFilter = Mapper.Map<ObjectForUsersFilter>(objectForUsersFilterDto);
                var user = UsersService.GetAllUsersForAdmin(objectForUsersFilter);
                var usersDtos = Mapper.Map<IList<UserAdminViewDto>>(user);
                return Ok(usersDtos);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserAdminViewDto userAdminViewDto)
        {
            var userAdminView = Mapper.Map<UserAdminView>(userAdminViewDto);
           
            try
            {
                // save 
                UsersService.AddUsers(userAdminView);
                return Ok(userAdminView);
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
                return Ok(userAdminView);
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
            return Ok(id);
        }

        

    }
}

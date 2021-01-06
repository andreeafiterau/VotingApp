using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.AdminControllers.UsersComponentManager
{
    [ApiController]
    [Route("[controller]")]
    public class UsersComponentController : ControllerBase
    {
        
        private IMapper Mapper { get; set; }
        private  IUsersInterface UsersService {get;set;}
        public UsersComponentController(IMapper _mapper,IUsersInterface usersService)
        {
            
            Mapper = _mapper;
            UsersService = usersService;
        }

        [HttpGet("filter")]
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
        public IActionResult Update([FromBody]UserAdminViewDto userAdminViewDto)
        {
            // map dto to entity and set id
            var userAdminView = Mapper.Map<UserAdminView>(userAdminViewDto);
            
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

    }
}

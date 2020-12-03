using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class UsersController : ControllerBase
    {
        private IRepository<User> Repository { get; set; }
        private IMapper Mapper { get; set; }
        public UsersController(IRepository<User> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
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
            user.UserId = id;

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
    }
}

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
    public class ApplicationRoleController : ControllerBase
    {
        private IRepository<Role> Repository { get; set; }
        private IMapper Mapper { get; set; }
        public ApplicationRoleController(IRepository<Role> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var applicationRole = Repository.GetByID(id);
            var applicationRoleDto = Mapper.Map<RoleDto>(applicationRole);
            return Ok(applicationRoleDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var applicationRole = Repository.GetAll();
            var applicationRoleDtos = Mapper.Map<IList<RoleDto>>(applicationRole);
            return Ok(applicationRoleDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RoleDto applicationRoleDto)
        {
            // map dto to entity
            var applicationRole = Mapper.Map<Role>(applicationRoleDto);

            try
            {
                // save 
                Repository.Insert(applicationRole);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]RoleDto applicationRoleDto)
        {
            // map dto to entity and set id
            var applicationRole = Mapper.Map<Role>(applicationRoleDto);
            applicationRole.IdRole = id;

            try
            {
                // save 
                Repository.Update(applicationRole);
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

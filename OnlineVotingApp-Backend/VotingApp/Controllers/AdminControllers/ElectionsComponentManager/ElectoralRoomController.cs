using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.AdminControllers.ElectionsComponentManager.NewFolder
{
    [ApiController]
    [Route("[controller]")]
    public class ElectoralRoomController : ControllerBase
    {
        private IRepository<Electoral_Room> Repository { get; set; }
        private IMapper Mapper { get; set; }

        public ElectoralRoomController(IRepository<Electoral_Room> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var electoralRoom = Repository.GetAll();
            var electoralRoomDtos = Mapper.Map<IList<Electoral_Room_Dto>>(electoralRoom);
            return Ok(electoralRoomDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Electoral_Room_Dto electoralRoomDto)
        {
            // map dto to entity
            var electoralRoom = Mapper.Map<Electoral_Room>(electoralRoomDto);

            try
            {
                // save 
                Repository.Insert(electoralRoom);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update( [FromBody]Electoral_Room_Dto electoralRoomDto)
        {
            // map dto to entity and set id
            var college = Mapper.Map<Electoral_Room>(electoralRoomDto);
            

            try
            {
                // save 
                Repository.Update(college);
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

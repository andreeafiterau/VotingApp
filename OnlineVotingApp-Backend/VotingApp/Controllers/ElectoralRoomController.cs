using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectoralRoomController : ControllerBase
    {
        private IRepository<ElectoralRoom> Repository { get; set; }
        private IMapper Mapper { get; set; }
        public ElectoralRoomController(IRepository<ElectoralRoom> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var electoralRoom = Repository.GetByID(id);
            var electoralRoomDto = Mapper.Map<ElectoralRoomDto>(electoralRoom);
            return Ok(electoralRoomDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var electoralRoom = Repository.GetAll();
            var electoralRoomDtos = Mapper.Map<IList<ElectoralRoomDto>>(electoralRoom);
            return Ok(electoralRoomDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ElectoralRoomDto electoralRoomDto)
        {
            // map dto to entity
            var electoralRoom = Mapper.Map<ElectoralRoom>(electoralRoomDto);

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
        public IActionResult Update(int id, [FromBody]ElectoralRoomDto electoralRoomDto)
        {
            // map dto to entity and set id
            var electoralRoom = Mapper.Map<ElectoralRoom>(electoralRoomDto);
            electoralRoom.ElectoralRoomId = id;

            try
            {
                // save 
                Repository.Update(electoralRoom);
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

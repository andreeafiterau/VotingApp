using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;
using VotingApp.Services;

namespace VotingApp.Controllers.AdminControllers.ElectionsComponentManager.NewFolder
{
    [ApiController]
    [Route("[controller]")]
    public class ElectoralRoomController : ControllerBase
    {
        private IRepository<Electoral_Room> Repository { get; set; }

        private IElectionInterface ElectionService { get; set; }
        private IMapper Mapper { get; set; }

        public ElectoralRoomController(IRepository<Electoral_Room> _repository, IMapper _mapper,IElectionInterface electionService)
        {
            Repository = _repository;
            Mapper = _mapper;
            ElectionService = electionService;
        }

        [HttpGet] //gets all electoral rooms + candidates
        public IActionResult GetAll()
        {
            //var electoralRooms = Repository.GetAll();
            var electoralRooms = ElectionService.GetAllElectoralRooms();
            //var electoralRoomDtos = Mapper.Map<IList<ElectionView>>(electoralRoom);
            return Ok(electoralRooms);
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
                return Ok(electoralRoom);
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
                return Ok(college);
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
            return Ok(id);
        }
    }
}

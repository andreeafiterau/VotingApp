using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.AdminControllers.ElectionsComponentManager
{
    [ApiController]
    [Route("[controller]")]
    public class ElectionController : ControllerBase
    {
        private IRepository<Electoral_Room> Repository { get; set; }
        private IMapper Mapper { get; set; }

        private IElectionInterface ElectionService { get; set; }

        public ElectionController(IRepository<Electoral_Room> _repository, IMapper _mapper,IElectionInterface electionService)
        {
            Repository = _repository;
            Mapper = _mapper;
            ElectionService = electionService;
        }

        [HttpGet("{id}")]
        public IActionResult GetAll([FromBody] ObjectForUsersFilterDto objectForUsersFilterDto,int id)
        {
            var objectForUsersFilter = Mapper.Map<ObjectForUsersFilter>(objectForUsersFilterDto);
            var usersForElection = ElectionService.GetUsersForElection(objectForUsersFilter, id);
            var usersForElectionDtos = Mapper.Map<IList<UserDto>>(usersForElection);
            return Ok(usersForElectionDtos);
        }

        [HttpPost("{id}")]
        public IActionResult Create([FromBody] ObjectForUsersFilterDto objectForUsersFilterDto, int id)
        {
            // map dto to entity
            var objectForUsersFilter = Mapper.Map<ObjectForUsersFilter>(objectForUsersFilterDto);

            try
            {
                // save 
                ElectionService.AddUsersForElection(objectForUsersFilter, id);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}")]
        public IActionResult GetResults(int id)
        {
           
            try
            {
                // save 
                var res=ElectionService.GetResults(id);

                var resDto = Mapper.Map<ResultDto>(res);

                return Ok(resDto);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    Repository.Delete(id);
        //    return Ok();
        //}
    }
}

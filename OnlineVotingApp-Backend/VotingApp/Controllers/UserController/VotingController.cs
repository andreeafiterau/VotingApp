using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.UserController
{
    [Route("[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {

        private IRepository<Candidate> Repository { get; set; }
        private IMapper Mapper { get; set; }

        private ICandidateInterface CandidateService { get; set; }

        private IElectionInterface ElectionService { get; set; }

        public VotingController(IRepository<Candidate> _repository, IMapper _mapper, ICandidateInterface candidateService,IElectionInterface electionService)
        {
            Repository = _repository;
            Mapper = _mapper;
            CandidateService = candidateService;
            ElectionService = electionService;
        }

        [HttpPost("vote")]
        public IActionResult Vote([FromBody] VotesDto voteDto)
        {
            // map dto to entity
            var vote = Mapper.Map<Vote>(voteDto);

            try
            {
                // save 
                AddToKeylessTable.AddToTable_Vote(vote.IdUser, vote.IdCandidate);
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
                var res = ElectionService.GetResults(id);

                

                return Ok(res);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
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
    public class CandidatesController : ControllerBase
    {
        private IRepository<Candidate> Repository { get; set; }
        private IMapper Mapper { get; set; }

        private ICandidateInterface CandidateService { get; set; }

        public CandidatesController(IRepository<Candidate> _repository, IMapper _mapper, ICandidateInterface candidateService)
        {
            Repository = _repository;
            Mapper = _mapper;
            CandidateService = candidateService;
        }

        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            var candidates = CandidateService.GetCandidates(id);
            var candidatesDtos = Mapper.Map<IList<Candidate_Dto>>(candidates);
            return Ok(candidatesDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Candidate_Dto candidateDto)
        {
            // map dto to entity
            var candidate = Mapper.Map<Candidate>(candidateDto);

            try
            {
                // save 
                Repository.Insert(candidate);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{idElectoralRoom}/{idCandidate}")]
        public IActionResult AddElectoralRoom(int idElectoralRoom, int idCandidate)
        {
            CandidateService.AddCandidateOnElectoralRoom(idElectoralRoom, idCandidate);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]Candidate_Dto candidateDto)
        {
            // map dto to entity and set id
            var candidate = Mapper.Map<Candidate>(candidateDto);


            try
            {
                // save 
                Repository.Update(candidate);
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

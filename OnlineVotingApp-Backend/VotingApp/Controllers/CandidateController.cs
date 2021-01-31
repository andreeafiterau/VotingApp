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
    public class CandidateController : ControllerBase
    {
        private IRepository<Candidate> Repository { get; set; }
        private IMapper Mapper { get; set; }
        public CandidateController(IRepository<Candidate> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var candidate = Repository.GetByID(id);
            var candidateDto = Mapper.Map<Candidate_Dto>(candidate);
            return Ok(candidateDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var candidate = Repository.GetAll();
            var candidateDtos = Mapper.Map<IList<Candidate_Dto>>(candidate);
            return Ok(candidateDtos);
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Candidate_Dto candidateDto)
        {
            // map dto to entity and set id
            var candidate = Mapper.Map<Candidate>(candidateDto);
            candidate.IdCandidate = id;

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

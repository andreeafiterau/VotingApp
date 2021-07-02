using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Helpers;
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

        [HttpGet("{id}")]//get candidates for every electoral room
        public IActionResult GetAll(int id)//alt numeeeeeee
        {
            var candidates = CandidateService.GetCandidates(id);
            var candidatesDtos = Mapper.Map<IList<CandidateViewDto>>(candidates);
            return Ok(candidatesDtos);
        }

        [HttpPost("getPossibleCandidates")]

        public IActionResult GetPossibleCandidates([FromBody] List<Candidate> currentCandidates)
        {
            try
            {
                var possibleCanddates=CandidateService.GetCandidatesForAdmin(currentCandidates);

                return Ok(possibleCanddates);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        

        //[HttpPost("{idElectoralRoom}/{idCandidate}")]
        //public IActionResult AddElectoralRoom(int idElectoralRoom, int idCandidate)
        //{
        //    CandidateService.AddCandidateOnElectoralRoom(idElectoralRoom, idCandidate);

        //    return Ok();
        //}

        [HttpPut("{id}")]// sa trimit si vechea cheie pe care vreau sa o modific
        public IActionResult Update([FromBody]Candidate_Dto candidateDto,int id)
        {
            // map dto to entity and set id
            var candidate = Mapper.Map<Candidate>(candidateDto);
            candidate.IdCandidate = id;

            try
            {
                // save 
                Repository.Update(candidate);
                return Ok(candidate);
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

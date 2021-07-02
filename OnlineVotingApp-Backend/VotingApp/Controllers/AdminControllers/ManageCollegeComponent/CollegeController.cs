using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;
using VotingApp.Services.Linq;

namespace VotingApp.Controllers.AdminControllers.ManageCollegeComponent
{
    [ApiController]
    [Route("[controller]")]
    public class CollegeController : ControllerBase
    {
        private IRepository<College> Repository { get; set; }
        private IMapper Mapper { get; set; }

        private ICollegeInterface CollegeService;
        public CollegeController(IRepository<College> _repository, IMapper _mapper, ICollegeInterface collegeService)
        {
            Repository = _repository;
            Mapper = _mapper;
            CollegeService = collegeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var college = Repository.GetAll();
                //var collegeDtos = Mapper.Map<IList<College_Dto>>(college);
                return Ok(college);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult Create([FromBody] College_Dto collegeDto)
        {
            // map dto to entity
            var college = Mapper.Map<College>(collegeDto);

            try
            {
                // save 
                Repository.Insert(college);
                return Ok(college);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]College_Dto collegeDto)
        {
            // map dto to entity and set id
            var college = Mapper.Map<College>(collegeDto);
            college.IdCollege = id;

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
            try
            {
                CollegeService.DeleteCollege(id);
                return Ok(id);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;

namespace VotingApp.Controllers.AdminControllers.ManageCollegeComponent
{
    [ApiController]
    [Route("[controller]")]
    public class CollegeController : ControllerBase
    {
        private IRepository<College> Repository { get; set; }
        private IMapper Mapper { get; set; }
        public CollegeController(IRepository<College> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var college = Repository.GetAll();
            var collegeDtos = Mapper.Map<IList<College_Dto>>(college);
            return Ok(collegeDtos);
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
                return Ok();
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

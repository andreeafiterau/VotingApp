using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VotingApp.Dtos;
using VotingApp.Entities;
using VotingApp.Interfaces;
using VotingApp.Services.Linq;

namespace VotingApp.Controllers.AdminControllers.ManageCollegeComponent
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private IRepository<Department> Repository { get; set; }
        private IMapper Mapper { get; set; }

        private ICollegeInterface CollegeService { get; set; }

        public DepartmentController(IRepository<Department> _repository, IMapper _mapper,ICollegeInterface _collegeService)
        {
            Repository = _repository;
            Mapper = _mapper;
            CollegeService = _collegeService;
        }

        [HttpPost("getDepartmentsByCollegeId")]
        public IActionResult GetAllForAdmin([FromBody] College_Dto collegeDto)
        {
            var department = CollegeService.GetDepartmentsForCollegeId(collegeDto.IdCollege);
            var departmentDtos = Mapper.Map<IList<DepartmentDto>>(department);
            return Ok(departmentDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DepartmentDto departmentDto)
        {
            // map dto to entity
            var department = Mapper.Map<Department>(departmentDto);

            try
            {
                // save 
                Repository.Insert(department);
                return Ok(department);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]DepartmentDto departmentDto)
        {
            // map dto to entity and set id
            var department = Mapper.Map<Department>(departmentDto);
            department.IdDepartment = id;

            try
            {
                // save 
                Repository.Update(department);
                return Ok(department);
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

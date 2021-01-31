using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Dtos;
using VotingApp.Entities;

namespace VotingApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Electoral_Room, Electoral_Room_Dto>();
            CreateMap<Electoral_Room_Dto, Electoral_Room>();
            CreateMap<Role,RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<ObjectForUsersFilterDto, ObjectForUsersFilter>();
            CreateMap<ObjectForUsersFilter, ObjectForUsersFilterDto>();
            CreateMap<UserAdminViewDto, UserAdminView>();
            CreateMap<UserAdminView, UserAdminViewDto>();
            CreateMap<Candidate, Candidate_Dto>();
            CreateMap<Candidate_Dto, Candidate>();
            CreateMap<College, College_Dto>();
            CreateMap<College_Dto, College>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

        }//CONSTRUCTOR
    }
}

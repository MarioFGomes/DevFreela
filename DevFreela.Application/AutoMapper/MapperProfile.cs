using AutoMapper;
using DevFreela.Application.Commands.CreateComent;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTO;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateSkillCommand, Skill>();
            CreateMap<Skill, SkillDTO>();
            CreateMap<CreateComentCommand, ProjectComment>();
            CreateMap<CreateProjectCommand, Project>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<Project, ProjectDetailsViewModel>();
            CreateMap<User, UserDetailViewModel>();
        }
    }
}

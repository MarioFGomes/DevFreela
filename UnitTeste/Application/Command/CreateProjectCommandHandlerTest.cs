using AutoMapper;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTeste.Application.Command;

public class CreateProjectCommandHandlerTest
{
    //[Fact]
    //public async Task InputDataOk_Executed_ReturnProjectId()
    //{
    //    // Arrange

    //     var ProjectRepositoryMock = new Mock<IProjectRepository>();
       
      

    //    var createProjectCommand=new CreateProjectCommand { Description="Titulo de teste", IdClient=1, IdFreelancer=1, Title="Novo Projeto", TotalCost=5000000 };

    //    var createProjectCommandHandler = new CreateProjectCommandHandler(ProjectRepositoryMock.Object, mapper.Object);
    //    //Action

    //    var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());
    //    //Assert

    //    Assert.True(id >= 0);
    //    ProjectRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Project>()), Times.Once);

    //}
}

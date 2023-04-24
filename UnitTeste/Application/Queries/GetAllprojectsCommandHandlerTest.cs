using DevFreela.Application.Queries.GetAllProject;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTeste.Application.Queries;

public class GetAllprojectsCommandHandlerTest
{
    [Fact]
    public async Task ThreeprojectExist_Executed_ReturnThreeProjectViewModel()
    {
        // Arrange

        var ProjectList = new List<Project> { 
           new Project("codereevew", "projeto de teste de codigo", 1, 2, 2000),
           new Project("rever", "projeto de teste de codigo", 1, 2, 2000),
           new Project("App-comida", "projeto de teste de codigo", 1, 2, 2000)
         };

        var ProjectRepositoryMock = new Mock<IProjectRepository>();
        ProjectRepositoryMock.Setup(p => p.GetAllAsync().Result).Returns(ProjectList);

        //Act

        var getAllProjectQuery = new GetAllProjectQuery();
        var getAllProjectQueryHandler = new GetAllProjectQueryHandler(ProjectRepositoryMock.Object);
        var projectViewModelList = await getAllProjectQueryHandler.Handle(getAllProjectQuery, new CancellationToken());

        //Assert
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(ProjectList.Count, projectViewModelList.Count);

        ProjectRepositoryMock.Verify(p => p.GetAllAsync().Result, Times.Once);

    }
}

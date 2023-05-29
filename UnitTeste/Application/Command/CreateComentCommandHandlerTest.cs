using AutoMapper;
using DevFreela.Application.AutoMapper;
using DevFreela.Application.Commands.CreateComent;
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

namespace UnitTeste.Application.Command
{
    public class CreateComentCommandHandlerTest
    {
        [Fact]
        public async Task InputDataOK_Createcomentforproject()
        {
            // Arrange

            var ProjectRepositoryMock = new Mock<IProjectRepository>();

            var configuracao = new MapperConfiguration(config =>
            {
                config.AddProfile<MapperProfile>();
            });
            var mapper = configuracao.CreateMapper();

            var coment = new CreateComentCommand() { Content = "This is a comment", IdProject = 1, IdUser = 1 };

            var createcomentCommandHandler = new CreateComentCommandHandler(ProjectRepositoryMock.Object, mapper);
            //Action

           var data=await createcomentCommandHandler.Handle(coment, new CancellationToken());
            //Assert

            Assert.NotEmpty(data.ToString());
            ProjectRepositoryMock.Verify(p => p.CreateComentAsync(It.IsAny<ProjectComment>()), Times.Once);
        }
    }
}

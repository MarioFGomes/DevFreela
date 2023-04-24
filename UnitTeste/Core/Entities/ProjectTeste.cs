using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTeste.Core.Entities
{
    public class ProjectTeste
    {
        [Fact]
        public void IfProjectStartWork()
        {
            var project = new Project("Mobile Project", "aplicativo de venda", 1, 2, 50000);
            project.Start();
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}

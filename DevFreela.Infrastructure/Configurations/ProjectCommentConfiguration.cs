using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Configurations
{
    public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.project)
               .WithMany(p => p.Comments)
               .HasForeignKey(p => p.IdProject);


            builder.HasOne(p => p.user)
             .WithMany(p => p.Comments)
             .HasForeignKey(p => p.IdUser);
        }
    }
}

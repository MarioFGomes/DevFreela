

namespace DevFreela.Core.Entities;

public class ProjectComment:BaseEntity
{
  

    public string? Content { get; private set; }
    public int IdProject { get; private set; }
    public Project project { get; private set; }
    public int IdUser { get; private set; }

    public User user { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public ProjectComment() { }
    public ProjectComment(string? content, int idProject, int idUser)
    {
        Content = content;
        IdProject = idProject;
        IdUser = idUser;
    }
}

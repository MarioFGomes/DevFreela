using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels;

public class ProjectViewModel
{
    public ProjectViewModel() {}
    public ProjectViewModel(int Id, string Title, DateTime CreatedAt, string Description, int Status)
    {
        id = Id;
        title = Title;
        createAt = CreatedAt;
        description = Description;
        status = (ProjectStatusEnum)Status;
    }

    public int id { get; private set; }
    public string title { get; private set; }
    public DateTime createAt { get; private set; }
    public string? description { get; private set; }
    public ProjectStatusEnum status { get; private set; }
}

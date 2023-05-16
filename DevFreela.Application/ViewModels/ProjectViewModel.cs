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

    public int id { get;  set; }
    public string title { get;  set; }
    public DateTime createAt { get; set; }
    public string? description { get; set; }
    public ProjectStatusEnum status { get; set; }
}

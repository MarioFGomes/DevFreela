using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels;

public class ProjectDetailsViewModel
{
    public ProjectDetailsViewModel(){}
    public ProjectDetailsViewModel(int id, string title, string description, DateTime? startedAt, DateTime? finishedAt, decimal totalCost,string clientefullname,string freelancefullname)
    {
        this.id = id;
        Title = title;
        Description = description;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
        TotalCost = totalCost;
        ClienteFullname = clientefullname;
        FreelanceFullname = freelancefullname;
        
    }

    public int id { get; set; }
    public string Title { get; set; }
    public string Description { get;  set; }
    public DateTime? StartedAt { get;  set; }
    public DateTime? FinishedAt { get;  set; }
    public decimal TotalCost { get;  set; }
    public string ClienteFullname { get; set; }
    public string FreelanceFullname { get; set; }
}

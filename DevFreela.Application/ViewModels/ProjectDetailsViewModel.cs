using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels;

public class ProjectDetailsViewModel
{
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

    public int id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public decimal TotalCost { get; private set; }
    public string ClienteFullname { get; set; }
    public string FreelanceFullname { get; set; }
}

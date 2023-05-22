

using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project: BaseEntity
{
   

    public string? Title { get; set; }
    public string? Description { get; set; }
    public int IdCliente { get; set; }
    public User Cliente { get; set; }

    public int IdFreelancer { get; set; }

    public User Freelancer { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public ProjectStatusEnum Status { get;  set; }

    public List<ProjectComment> Comments { get; set; }

    public Project() { }

    public Project(string? title, string? description, int idCliente, int idFreelancer, decimal totalCost)
    {
        Title = title;
        Description = description;
        IdCliente = idCliente;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;

        CreatedAt = DateTime.Now;
        Status=ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
    }

  
    public void Start()
    {
        if (Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }
    }


    public void Finish()
    {
        if (Status == ProjectStatusEnum.payment)
        {
            Status = ProjectStatusEnum.Finished;
            FinishedAt = DateTime.Now;
        }
    }

    public void Cancel()
    {
        if (Status == ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.Cancelled;
        }

    }

    public void Payment()
    {
        if (Status == ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.payment;
        }
    }


    public void Update(string title, string description,decimal totalcost)
    {
        Title=title;
        Description = description;
        TotalCost=totalcost;
    }



}

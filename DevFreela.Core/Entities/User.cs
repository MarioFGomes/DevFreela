using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities;

public class User:BaseEntity
{
    public string FullName { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

    public string Role { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; }

    public List<UserSkill> Skills { get; private set; }

    public List<Project> OwnedProjects { get; private set; }

    public List<Project> FreelanceProject { get; private set; }

    public List<ProjectComment> Comments { get; private set; }

  
    public User() { }
    public User(string fullname,string email, DateTime birthdate, string password, string role)
    {
        FullName = fullname;
        Email = email;
        BirthDate = birthdate;
        Active = true;
        Password = password;
        Role = role;

        CreatedAt = DateTime.Now;
        Skills = new List<UserSkill>();
        OwnedProjects = new List<Project>();
        FreelanceProject = new List<Project>();
        
    }

    public void DisableUser()
    {
        Active= false;
    }

    public void EnableUser()
    {
        Active = true;
    }
}

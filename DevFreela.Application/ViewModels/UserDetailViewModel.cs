

namespace DevFreela.Application.ViewModels;

public class UserDetailViewModel
{
    public UserDetailViewModel(string fullName, string email, DateTime birthDate, bool active, string password, string role)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Active = active;
        Password = password;
        Role = role;
    }

    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string? Password { get; set; }

    public string Role { get; set; }

    public DateTime BirthDate { get; private set; }

    public bool Active { get; private set; }
}

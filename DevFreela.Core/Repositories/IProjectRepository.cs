using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();

    Task<Project> GetByIdAsync(int id);

    Task AddAsync(Project project);

    Task CreateComentAsync(ProjectComment comment);

    Task SaveChangeAsync();

    Task UpdateChangesAsync(Project project);



}

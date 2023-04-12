namespace DevFreela.API.Models
{
    public class CreateProjectModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

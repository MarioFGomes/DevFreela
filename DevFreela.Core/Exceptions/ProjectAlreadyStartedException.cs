
using System.Resources;
namespace DevFreela.Core.Exceptions;

public class ProjectAlreadyStartedException:Exception
{
    public ProjectAlreadyStartedException():base("Project is Already in Started Satus") {}
}

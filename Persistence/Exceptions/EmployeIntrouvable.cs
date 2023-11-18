namespace ApiCube.Persistence.Exceptions;

public class EmployeIntrouvable : Exception
{
    public EmployeIntrouvable() : base("employe_introuvable")
    {
    }
    public EmployeIntrouvable(string message) : base(message)
    {
    }
    
    public EmployeIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}
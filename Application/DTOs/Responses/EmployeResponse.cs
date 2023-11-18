namespace ApiCube.Application.DTOs.Responses;

public class EmployeResponse
{
    public required int Id { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Email { get; set; }
    public required DateTime DateEmbauche { get; set; }
    public required DateTime? DateDepart { get; set; }
    public required string Poste { get; set; }
}
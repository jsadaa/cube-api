namespace ApiCube.Application.DTOs.Responses;

public class ClientResponse
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Adresse { get; set; }
    public required string CodePostal { get; set; }
    public required string Ville { get; set; }
    public required string Pays { get; set; }
    public required string Telephone { get; set; }
    public required string Email { get; set; }
    public DateTime DateNaissance { get; set; }
    public DateTime DateInscription { get; set; }
}
namespace ApiCube.Application.DTOs.Requests;

public class FournisseurRequest
{
    public required string Nom { get; set; }

    public required string Adresse { get; set; }

    public required string CodePostal { get; set; }

    public required string Ville { get; set; }

    public required string Pays { get; set; }

    public required string Telephone { get; set; }

    public required string Email { get; set; }
}
namespace ApiCube.Application.DTOs.Requests;

public class FournisseurRequestDTO
{
    public string Nom { get; set; }

    public string Adresse { get; set; }

    public string CodePostal { get; set; }

    public string Ville { get; set; }

    public string Pays { get; set; }

    public string Telephone { get; set; }

    public string Email { get; set; }
}
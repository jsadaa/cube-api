namespace ApiCube.Application.DTOs.Responses;

public class FournisseurResponseDTO
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public AdresseDTO Adresse { get; set; }

    public string Telephone { get; set; }

    public string Email { get; set; }
}
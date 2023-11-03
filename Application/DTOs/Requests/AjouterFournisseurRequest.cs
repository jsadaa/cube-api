namespace ApiCube.Application.DTOs.Requests;

public class AjouterFournisseurRequest
{
    public string Nom { get; set; }
    
    public string Adresse { get; set; }
    
    public string Telephone { get; set; }
    
    public string Email { get; set; }
}
namespace ApiCube.Application.DTOs.Requests;

public class CommandeFournisseurUpdate
{
    public int FournisseurId { get; set; }
    public int EmployeId { get; set; }
    public List<ProduitCommandeRequest> Produits { get; set; }
}
namespace ApiCube.Application.DTOs.Responses;

public class LigneCommandeClientResponse
{
    public int Id { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
    public double Total { get; set; }
    public ProduitResponse Produit { get; set; }
}
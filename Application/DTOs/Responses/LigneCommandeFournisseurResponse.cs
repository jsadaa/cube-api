namespace ApiCube.Application.DTOs.Responses;

public class LigneCommandeFournisseurResponse
{
    public int Id { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
    public double Remise { get; set; }
    public double Total { get; set; }
    public ProduitResponse Produit { get; set; }
    public int CommandeFournisseurId { get; set; }
}
namespace ApiCube.Application.DTOs.Requests;

public class CommandeFournisseurRequest
{
    public int FournisseurId { get; set; }
    public int EmployeId { get; set; }
    public List<ProduitCommandeRequest> Produits { get; set; }
}

public class ProduitCommandeRequest
{
    public int ProduitId { get; set; }
    public int Quantite { get; set; }
}


namespace ApiCube.Domain.Exceptions;

public class QuantiteProduitCommandeInvalide : Exception
{
    public QuantiteProduitCommandeInvalide() : base("quantite_produit_commande_invalide")
    {
    }
    
    public QuantiteProduitCommandeInvalide(string message) : base(message)
    {
    }
    
    public QuantiteProduitCommandeInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}
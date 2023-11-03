namespace ApiCube.Domain.Enums.Stock;

public enum TypeTransactionStock
{
    Achat,
    Vente,
    Retour,
    Perte,
    Vol,
    Peremption
}

public class TypeTransactionStockMapper
{
    public TypeTransactionStock Mapper(string type)
    {
        return type switch
        {
            "Achat" => TypeTransactionStock.Achat,
            "Vente" => TypeTransactionStock.Vente,
            "Retour" => TypeTransactionStock.Retour,
            "Perte" => TypeTransactionStock.Perte,
            "Vol" => TypeTransactionStock.Vol,
            "Peremption" => TypeTransactionStock.Peremption,
            _ => throw new Exception("Le type de transaction n'existe pas")
        };
    }
}
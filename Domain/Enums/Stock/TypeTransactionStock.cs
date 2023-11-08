using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Stock;

public enum TypeTransactionStock
{
    Achat,
    Vente,
    Retour,
    Perte,
    Vol,
    Peremption,
    ModificationInterne
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
            "ModificationInterne" => TypeTransactionStock.ModificationInterne,
            _ => throw new TypeTransactionStockInexistant()
        };
    }
}
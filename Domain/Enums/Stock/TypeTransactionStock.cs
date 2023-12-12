using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Stock;

public enum TypeTransactionStock
{
    Achat,
    AjoutInterne,
    Vente,
    Retour,
    Perte,
    Vol,
    Peremption,
    ModificationInterne,
    Suppression
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
            "Suppression" => TypeTransactionStock.Suppression,
            _ => throw new TypeTransactionStockInexistant()
        };
    }
}
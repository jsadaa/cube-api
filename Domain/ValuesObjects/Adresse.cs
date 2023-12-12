namespace ApiCube.Domain.ValuesObjects;

public class Adresse
{
    public Adresse(string rue, string codePostal, string ville, string pays)
    {
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Pays = pays;
    }

    public string Rue { get; set; }

    public string CodePostal { get; set; }

    public string Ville { get; set; }

    public string Pays { get; set; }

    public override string ToString()
    {
        return $"{Rue}, {CodePostal}, {Ville}, {Pays}";
    }
}
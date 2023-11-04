namespace ApiCube.Domain.ValuesObjects;

public class Adresse
{
    private string Numero { get; set; }

    private string Rue { get; set; }

    private string CodePostal { get; set; }

    private string Ville { get; set; }

    private string Pays { get; set; }
    
    public Adresse(string numero, string rue, string codePostal, string ville, string pays)
    {
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Pays = pays;
    }
  
    public override string ToString()
    {
         return $"{Numero.Trim()} {Rue.Trim()} {CodePostal.Trim()} {Ville.Trim()} {Pays.Trim()}";
    }
}
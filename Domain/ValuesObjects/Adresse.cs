namespace ApiCube.Domain.ValuesObjects;

public class Adresse
{
    public string Numero { get; set; }
    
    public string Rue { get; set; }
    
    public string CodePostal { get; set; }
    
    public string Ville { get; set; }
    
    public string Pays { get; set; }
    
    public Adresse(string numero, string rue, string codePostal, string ville, string pays)
    {
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Pays = pays;
    }
    
    public void MettreAJourNumero(string numero)
    {
        Numero = numero;
    }
    
    public void MettreAJourRue(string rue)
    {
        Rue = rue;
    }
    
    public void MettreAJourCodePostal(string codePostal)
    {
        CodePostal = codePostal;
    }
    
    public void MettreAJourVille(string ville)
    {
        Ville = ville;
    }
    
    public void MettreAJourPays(string pays)
    {
        Pays = pays;
    }
  
    public override string ToString()
    {
        return Numero + ", " + Rue + ", " + CodePostal + ", " + Ville + ", " + Pays;
    }
}
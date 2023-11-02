namespace ApiCube.Domain.ValuesObjects;

public class Telephone
{
    
    public string Numero { get; set; }
    
    public Telephone(string numero)
    {
        Numero = numero;
    }
    
    public void MettreAJourNumero(string numero)
    {
        Numero = numero;
    }
    
    public override string ToString()
    {
        return Numero;
    }
}
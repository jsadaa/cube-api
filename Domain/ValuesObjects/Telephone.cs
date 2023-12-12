namespace ApiCube.Domain.ValuesObjects;

public class Telephone
{
    public Telephone(string numero)
    {
        Numero = numero;
    }

    private string Numero { get; set; }

    public void MettreAJourNumero(string numero)
    {
        Numero = numero;
    }

    public override string ToString()
    {
        return Numero;
    }
}
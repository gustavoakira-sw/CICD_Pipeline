namespace FiapApi.Models.Entities;

public class ColetaDeLixo
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string Endereco { get; set; }
    public string TipoDeLixo { get; set; }
}
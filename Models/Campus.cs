namespace SimpleLAP.Models;

public partial class Campus
{
    public int IdCampus { get; set; }
    public string Nombre { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public string Localidad { get; set; } = null!;
    public string Ciudad { get; set; } = null!;
    public string Pais { get; set; } = null!;
    public decimal Precio { get; set; }
    public int EdadMinima { get; set; }
    public int EdadMaxima { get; set; }
    public int AforoMaximo { get; set; }
    public int DescuentoHermano { get; set; }
    public int DescuentoJugador { get; set; }
    public string DescripcionCampus { get; set; } = null!;
    public string ImagenCampus { get; set; } = null!;
    public DateOnly DiaInicio { get; set; }
    public DateOnly DiaFinal { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFinal { get; set; }
    public string FormaPagoUno { get; set; } = null!;
    public string? FormaPagoDos { get; set; }
    public string? FormaPagoTres { get; set; }

    public virtual ICollection<Participante> Participantes { get; set; } = [];
}

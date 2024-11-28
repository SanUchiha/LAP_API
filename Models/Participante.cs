namespace SimpleLAP.Models;

public partial class Participante
{
    public int IdParticipante { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string Dniparticipante { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string DireccionParticipante { get; set; } = null!;

    public string Localidad { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string? Lesiones { get; set; }

    public string? DescripcionLesiones { get; set; }

    public bool TomaMedicacion { get; set; }

    public string? DescripcionMedicacion { get; set; }

    public bool Alergias { get; set; }

    public string? DescripcionAlergias { get; set; }

    public string NombreTutor { get; set; } = null!;

    public string PrimerApellidoTutor { get; set; } = null!;

    public string? SegundoApellidoTutor { get; set; }

    public string Dnitutor { get; set; } = null!;

    public string TelefonoPrincipal { get; set; } = null!;

    public string? TelefonoSecundario { get; set; }

    public string CorreoParticipante { get; set; } = null!;

    public bool PermiteFotos { get; set; }

    public bool Autorizacion { get; set; }

    public string? Firma { get; set; }

    public int IdCampus { get; set; }

    public string? TallaCamiseta { get; set; }

    public virtual Campus IdCampusNavigation { get; set; } = null!;
}

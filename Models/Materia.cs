using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OrganizerU.Models {
  public class Materia : IValidatableObject {
    [Required]
    public string Id { get; set; }

    [Required]
    public Dia[] Horario { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Profesor { get; set; }

    [Required]
    public string Salon { get; set; }

    [Required]
    public int Creditos { get; set; }

    [Required]
    public List<double>[] Cortes_Notas { get; set; }

    [Required]
    public List<Stream> Archivos { get; set; }
    public Materia (string nombre, string profesor, string salon, int creditos, Dia[] Horario) {
      this.Id = Guid.NewGuid().ToString();
      this.Nombre = nombre;
      this.Profesor = profesor;
      this.Salon = salon;
      this.Creditos = creditos;
      this.Horario = Horario;
      this.Cortes_Notas = new List<double>[0];
      this.Archivos = new List<Stream> ();
    }

    public IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
      var Err = new List<ValidationResult> ();
      if (Horario.Length <= 0 || Horario.Length >= 7) Err.Add (new ValidationResult ("Digite Cantidades Correspondiente A La Semana", new string[] { "1" }));
      if (Creditos < 0) Err.Add (new ValidationResult ("Digite Creditos Igual O Mayor A 0", new string[] { "2" }));
      return Err;
    }
  }
}
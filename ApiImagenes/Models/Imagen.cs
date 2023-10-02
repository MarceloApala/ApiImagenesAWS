using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiImagenes.Models;

[Table("Imagen")]
public partial class Imagen
{
    [Key]
    public int Id { get; set; }

    [Column("Imagen")]
    public string? Imagen1 { get; set; }
    [NotMapped]
    public IFormFile? Imagen2 { get; set; }
}

using System;
using System.Collections.Generic;

namespace PruebaTecnica_AARCO.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}

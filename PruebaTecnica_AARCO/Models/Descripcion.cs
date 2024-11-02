using System;
using System.Collections.Generic;

namespace PruebaTecnica_AARCO.Models
{
    public partial class Descripcion
    {
        public int DescripcionId { get; set; }
        public int ModeloId { get; set; }
        public string DescripcionTexto { get; set; } = null!;

        public virtual ModeloPorSubmarca Modelo { get; set; } = null!;
    }
}

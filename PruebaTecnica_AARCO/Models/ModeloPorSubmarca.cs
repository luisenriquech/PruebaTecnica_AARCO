using System;
using System.Collections.Generic;

namespace PruebaTecnica_AARCO.Models
{
    public partial class ModeloPorSubmarca
    {
        public ModeloPorSubmarca()
        {
            Descripcions = new HashSet<Descripcion>();
        }

        public int ModeloId { get; set; }
        public int SubmarcaId { get; set; }
        public int Anio { get; set; }

        public virtual Submarca Submarca { get; set; } = null!;
        public virtual ICollection<Descripcion> Descripcions { get; set; }
    }
}

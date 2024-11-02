using System;
using System.Collections.Generic;

namespace PruebaTecnica_AARCO.Models
{
    public partial class Submarca
    {
        public Submarca()
        {
            ModeloPorSubmarcas = new HashSet<ModeloPorSubmarca>();
        }

        public int SubmarcaId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual Marca Marca { get; set; } = null!;
        public virtual ICollection<ModeloPorSubmarca> ModeloPorSubmarcas { get; set; }
    }
}

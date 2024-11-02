using System;
using System.Collections.Generic;

namespace PruebaTecnica_AARCO.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Submarcas = new HashSet<Submarca>();
        }

        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Submarca> Submarcas { get; set; }
    }
}

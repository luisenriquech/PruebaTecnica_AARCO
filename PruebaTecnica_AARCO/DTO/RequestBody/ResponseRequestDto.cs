using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica_AARCO.DTO.RequestBody
{
    [Keyless]
    public class ResponseRequestDto
    {
        public string nombre { get; set; }
        public string submarca { get; set; }
        public string modelo { get; set; }
        public string descripcion { get; set; }
    }
}

namespace PruebaTecnica_AARCO.DTO.JWT
{
    public class ResponseTokenLoginDto
    {
        public string? token { get; set; }
        public string? nombre { get; set; }
        public DateTime? Caducidad { get; set; }
        public int MinutosSesion { get; set; }
    }
}

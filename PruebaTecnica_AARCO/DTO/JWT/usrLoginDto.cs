namespace PruebaTecnica_AARCO.DTO.JWT
{
    public class usrLoginDto
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }        
        public bool? Activo { get; set; }
        
    }
}
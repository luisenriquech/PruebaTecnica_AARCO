namespace PruebaTecnica_AARCO.Validaciones.Mensajes
{
    public class Utils
    {

        public static string MensajesValidacion(string campo, int opcion)
        {
            return opcion switch
            {
                1 => ("El campo " + campo + " no puede ser nulo. "),
                2 => ("El campo " + campo + " no puede ser vacío. "),
                3 => ("El campo " + campo + " está mal formado. "),
                4 => ("El campo " + campo + " no puede ser menor a 0. "),
                5 => ("El campo " + campo + " no puede ser menor a 1. "),
                6 => ("El campo " + campo + " no puede ser menor a "),
                7 => ("El campo " + campo + " no puede ser mayor a "),
                8 => ("El campo " + campo + " debe ser mayor a 0. "),
                9 => ("El campo " + campo + " debe ser 0. "),
                _ => "Error en el campo " + campo
            };
        }

        public static string Sugerencia(int opcion)
        {
            return opcion switch
            {
                0 => ("Debe ser un número positivo entero mayor a o igual 0. "), // Números enteros y Decimales
                1 => ("Debe ser un número positivo decimal o entero mayor a 0. "), // Números enteros y Decimales
                2 => ("Debe ser una fecha válida. "), // Fechas
                3 => ("Debe ser un número entero positivo mayor a 0. "), // Números enteros
                4 => ("Debe ser una cadena con mayúsculas, minúsculas, acentos, diéresis, números y espacios. "), // Números enteros
                _ => "",
            };
        }

        public static string MensajesGenerales(int opcion)
        {
            return opcion switch
            {
                1 => ("Excepción no controlada. "),
                2 => ("Error en validaciones de campos. "),
                3 => ("No se pudo iniciar sesión, verifique sus credenciales. "),
                4 => ("Todos los registros fueron insertados correctamente. "),
                5 => ("Registro actualizado correctamente. "),
                6 => ("Registro no encontrado. "),
                7 => ("Registro insertado correctamente. "),
                8 => ("Registro eliminado correctamente. "),
                9 => ("Sin registros para mostrar. "),
                _ => "",
            };

        }
    }
}
using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener hasta {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace OutletCelular.Models
{
    public class Accesorio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } // Nombre del accesorio

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; } // Descripción del accesorio

        [Required(ErrorMessage = "El precio del producto es requerido")]
        [Range(0, 9999.99, ErrorMessage = "El precio debe estar entre 0 y 9999.99")]
        public decimal Precio { get; set; } // Presii del accesorio

        [Required(ErrorMessage = "La marca del producto es requerida")]
        [StringLength(50, ErrorMessage = "La marca no puede exceder los 50 caracteres")]
        public string Marca { get; set; } // Marca del accesorio 

        [Required(ErrorMessage = "El stock del producto es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; } // Cantidad disponible en stock
    }
}

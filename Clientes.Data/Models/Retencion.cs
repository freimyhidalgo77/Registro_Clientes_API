using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retenciones.Data.Models
{
    public class Retencion
    {

        [Key]
        public int RetencionId { get; set; }

        [Required(ErrorMessage = "Campo nombre obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
        public string? descripcion { get; set; }


        [Required(ErrorMessage = "Campo numero de monto obligatorio")]
        public double monto { get; set; }



    }
}

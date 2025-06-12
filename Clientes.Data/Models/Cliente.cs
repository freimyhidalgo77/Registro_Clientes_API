using System;More actions
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Clientes.Data.Models
{
    public class Cliente
    {

        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Campo nombre obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
        public string? NombreCliente { get; set; }


        [Required(ErrorMessage = "Campo numero de whatsapp obligatorio")]
        public string? NumeroWhatsapp { get; set; }

     


       }
    }

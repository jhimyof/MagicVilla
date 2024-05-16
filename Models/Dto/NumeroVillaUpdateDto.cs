using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class NumeroVillaUpdateDto
    {
        [Required]
        public int villaNo { get; set; }

        [Required]
        public int villaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}

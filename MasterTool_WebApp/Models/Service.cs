using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Service
    {
        [Key] 
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Required] 
        [Column("name")]
        [StringLength(150, ErrorMessage = "Длина имени услуги не может превышать 150 символов.")]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(500, ErrorMessage = "Длина описания не может превышать 500 символов.")]
        public string Description { get; set; }

        [Required]
        [Column("price")]
        [Range(0.01, 99999.99, ErrorMessage = "Цена должна быть в пределах от 0.01 до 99999.99.")]
        public decimal Price { get; set; }

        [Required] 
        [Column("is_available")]
        public bool IsAvailable { get; set; }
    }
}

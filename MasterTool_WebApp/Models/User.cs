using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("user_name")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [Column("password")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [Column("phone_number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("surname")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [ForeignKey("Role")]
        [Column("role_id")]
        public int? RoleId { get; set; }

        public Role Role { get; set; } // Навигационное свойство
    }
}

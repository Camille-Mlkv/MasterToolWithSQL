using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Role
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        // Навигационное свойство для связи с пользователями
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

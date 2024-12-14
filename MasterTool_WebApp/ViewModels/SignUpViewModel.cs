using MasterTool_WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно.")]
        [StringLength(100, ErrorMessage = "Имя пользователя не может быть длиннее 100 символов.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 200 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен.")]
        [StringLength(20, ErrorMessage = "Номер телефона не может быть длиннее 20 символов.")]
        [RegularExpression(@"^(\+?[\d\(\)\-\s]+)$", ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Имя обязательно.")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Фамилия не может быть длиннее 50 символов.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Роль обязательна.")]
        public int SelectedRole { get; set; }

        public List<Role> Roles { get; set; }
    }

}

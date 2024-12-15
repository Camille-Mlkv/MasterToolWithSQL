using MasterTool_WebApp.LocalData;
using MasterTool_WebApp.Services;
using MasterTool_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleService _roleService;
        private readonly UserService _userService;

        public AccountController(RoleService roleService, UserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            var roles=await _roleService.GetRolesAsync();
            var model = new SignUpViewModel
            {
                Roles = roles
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            var model = new SignInViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = await _roleService.GetRolesAsync();
                model.Roles = roles;
            }
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetUserByUsernameAsync(model.UserName);
                if (existingUser.Count != 0)
                {
                    ModelState.AddModelError("", "User with this credentials already exists.");
                    return View(model);
                }

                await _userService.CreateUserAsync(model.UserName, model.Password, model.PhoneNumber, model.Name, model.Surname, model.SelectedRole);

                // Переходим на страницу входа
                return RedirectToAction("SignIn");
            }
            else
            {
                return View(model);
            }
            
            
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var users = await _userService.GetUserByCredentialsAsync(model.UserName,model.Password);
                if (users.Count==0)
                {
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
                    return View(model);
                }
                CurrentUser.UserName = model.UserName;
                CurrentUser.UserId = users.First().UserId;

                int? roleId = users.First().RoleId;
                var roles = await _roleService.GetRoleByIdAsync(roleId);

                if(roles.Count==0)
                {
                    ModelState.AddModelError("", "Ошибка с ролями.");
                    return View(model);
                }

                var role = roles.First();
                if (role.Name == "admin")
                {
                    CurrentUser.IsAdmin = true;
                    CurrentUser.IsClient = false;
                    CurrentUser.IsMaster = false;
                }
                else if (role.Name == "client")
                {
                    CurrentUser.IsAdmin = false;
                    CurrentUser.IsClient = true;
                    CurrentUser.IsMaster = false;
                }
                else if(role.Name =="master")
                {
                    CurrentUser.IsAdmin = false;
                    CurrentUser.IsClient = false;
                    CurrentUser.IsMaster = true;
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            CurrentUser.UserName = null;
            CurrentUser.IsClient = false;
            CurrentUser.IsMaster = false;
            CurrentUser.IsAdmin = false;

            return RedirectToAction("Index", "Home");
        }
    }
}

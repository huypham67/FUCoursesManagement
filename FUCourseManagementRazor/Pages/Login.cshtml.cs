using FURepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUCourseManagementRazor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userRepository.GetUserByEmail(Email);

            if (user == null || user.Password != Password)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return Page();
            }

            // Lưu Session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToPage("/Index");
        }
    }
}

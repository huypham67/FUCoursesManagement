using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUCourseManagementRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserRole { get; set; } = "Guest";
        public void OnGet()
        {
            UserRole = _httpContextAccessor.HttpContext?.Session.GetString("UserRole") ?? "Guest";
        }
    }
}

namespace Sample.Pages;

using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        ModelState.AddModelError("", "User account doesn't exist");

        return Page();
    }

    public class InputModel
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}

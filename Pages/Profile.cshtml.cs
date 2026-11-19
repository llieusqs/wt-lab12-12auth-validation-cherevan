using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wt_lab12_12auth_validation_cherevan.Pages;

public class ProfileModel : PageModel
{
    public string? UserLogin { get; private set; }

    public IActionResult OnGet()
    {
        // Проверяем, есть ли логин в сессии
        UserLogin = HttpContext.Session.GetString("UserLogin");

        if (UserLogin == null)
        {
            // Если нет — выкидываем на страницу входа
            return RedirectToPage("/Login");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        // Выход: очищаем сессию
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");
    }
}
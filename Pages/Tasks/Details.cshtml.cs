using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_cherevan.Data;
using wt_lab12_12auth_validation_cherevan.Models;

namespace wt_lab12_12auth_validation_cherevan.Pages.Tasks;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public TaskItem Task { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var task = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);
        if (task == null) return NotFound();

        Task = task;
        return Page();
    }
}
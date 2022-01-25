using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages
{
    public class ItemListModel : PageModel
    {
       
            public List<Item> items = new();

            [BindProperty]
            public Item NewItem { get; set; } = new();

            public void OnGet()
            {
                items = ItemService.GetAll();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                ItemService.Add(NewItem);
                return RedirectToAction("Get");
            }

            public IActionResult OnPostDelete(int id)
            {
                ItemService.Delete(id);
                return RedirectToAction("Get");
            }
        }
}

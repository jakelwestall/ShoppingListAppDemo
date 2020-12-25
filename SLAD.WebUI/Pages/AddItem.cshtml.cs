using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SLAD.DataLib;
using SLAD.DataLib.Models;
using System.Threading.Tasks;

namespace SLAD.WebUI.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty]
        public ListItem ThisItem { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await DataAccess.SaveData<ListItem>("insert into ListItems (ItemName, ItemQty) values (@ItemName, @ItemQty)", ThisItem, @"Data Source=Data/shoppinglist.db;Version=3;");

            return RedirectToPage("/Index");
        }
    }
}
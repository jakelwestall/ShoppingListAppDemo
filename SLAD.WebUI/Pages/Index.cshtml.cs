using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SLAD.DataLib;
using SLAD.DataLib.Models;

namespace SLAD.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<ListItem> ListItems { get; set; }

        public async void OnGetAsync()
        {
            ListItems = await DataAccess.LoadData<ListItem, dynamic>("select * from listitems", new { }, @"Data Source=Data/shoppinglist.db;Version=3;");
        }

        public async Task<IActionResult> OnPostMark(int id)
        {
            await DataAccess.SaveData<dynamic>($"update listitems set IsSelected = 1 where ID = { id }", new { }, @"Data Source=Data/shoppinglist.db;Version=3;");
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostUnmark(int id)
        {
            await DataAccess.SaveData<dynamic>($"update listitems set IsSelected = 0 where ID = { id }", new { }, @"Data Source=Data/shoppinglist.db;Version=3;");
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            // For some reason you have to pull the data from the database again to have items to foreach through in the list.
            ListItems = await DataAccess.LoadData<ListItem, dynamic>("select * from listitems", new { }, @"Data Source=Data/shoppinglist.db;Version=3;");

            foreach (ListItem item in ListItems)
            {
                if (item.IsSelected)
                {
                    await DataAccess.SaveData<dynamic>($"delete from listitems where ID = { item.ID }", new { }, @"Data Source=Data/shoppinglist.db;Version=3;");
                }
            }

            return RedirectToPage("/Index");
        }
    }
}

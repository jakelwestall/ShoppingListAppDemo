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

        public async Task OnBeforeUnloadAsync()
        {
            foreach (ListItem item in ListItems)
            {
                if (item.IsSelected)
                {
                    await DataAccess.SaveData<ListItem>("update listitems set IsSelected = 1 where ItemName = @ItemName", item, @"Data Source=Data/shoppinglist.db;Version=3;");
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sajjadmirzapour.Components
{
    public class ShortNews:ViewComponent
    {
        private Context _context;
        public ShortNews(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Models.ShortNews> shortNews = _context.ShortNews.ToList();
            return View("/Views/Components/ShortNews.cshtml", shortNews);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sajjadmirzapour.Components
{
    public class ShortTextNews : ViewComponent
    {
        private Context _context;
        public ShortTextNews(Context context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Models.ShortTextNews> shortTextNews = _context.ShortTextNews.ToList();
            return View("/Views/Components/ShortTextNews.cshtml", shortTextNews);
        }
    }
}

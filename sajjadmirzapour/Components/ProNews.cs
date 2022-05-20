using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sajjadmirzapour.Components
{
    public class ProNews:ViewComponent
    {
        private Context _context;
        public ProNews(Context context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Models.ProNews> news = _context.ProNews.ToList();
            return View("/Views/Components/ProNews.cshtml", news);
        }
    }
}

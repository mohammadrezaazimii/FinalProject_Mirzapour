using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace sajjadmirzapour.Controllers
{
    public class NewsController : Controller
    {
        private Context context;
        public NewsController(Context c)
        {
            context = c;
        }
        public IActionResult Index()
        { 
            return View(context.News.ToList());
        }
    }
}

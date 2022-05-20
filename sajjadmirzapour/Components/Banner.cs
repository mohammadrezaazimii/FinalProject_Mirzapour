using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace sajjadmirzapour.Components
{
    public class Banner : ViewComponent
    {

        private Context _context;
        public Banner(Context context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            Models.Banner banner = _context.Banner.Find(1);
            return View("/Views/Components/Banner.cshtml", banner);
        }
    }
}

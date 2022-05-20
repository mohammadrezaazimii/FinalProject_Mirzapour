using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sajjadmirzapour;
using sajjadmirzapour.Models;

namespace sajjadmirzapour.Controllers
{
    public class ProNewsController : Controller
    {
        private readonly Context _context;

        public ProNewsController(Context context)
        {
            _context = context;
        }





        #region Index


        public async Task<IActionResult> Index()
        {
            return View(await _context.ProNews.ToListAsync());
        }

        #endregion



        #region Details


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (proNews == null)
            {
                return NotFound();
            }

            return View(proNews);
        }

        #endregion


        #region Create


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ProNews proNews, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                #region Image
                if (Image != null)
                {


                    string imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    string _path = "wwwroot/Image/News/" + imageName;
                    using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                    {
                        await Image.CopyToAsync(f);

                    }
                    proNews.Picture = imageName;
                }
                else
                {
                    ModelState.AddModelError("Picture", "وارد کردن عکس الزامیست");
                    return View(proNews);
                }
                #endregion
                proNews.date = DateTime.Now;


                _context.Add(proNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proNews);
        }



        #endregion


        #region Edit



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews.FindAsync(id);
            if (proNews == null)
            {
                return NotFound();
            }
            return View(proNews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ProNews proNews, IFormFile Image)
        {
            if (id != proNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region Image

                ProNews OldPic = await _context.ProNews.FindAsync(id);
                if (Image != null)
                {
                    try
                    {

                        System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/News/" + OldPic.Picture);


                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError("Picture", "مشکل در حذف عکس");
                        return View(proNews);
                    }
                    string imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    string _path = "wwwroot/Image/News/" + imageName;
                    using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                    {
                        await Image.CopyToAsync(f);

                    }
                    proNews.Picture = imageName;


                }
                else
                {
                    proNews.Picture = OldPic.Picture;
                }

                #endregion


                try
                {
                    _context.Entry(OldPic).State = EntityState.Detached;
                    _context.ProNews.Update(proNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProNewsExists(proNews.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }



            return View(proNews);


        }


        #endregion

        #region Delete




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (proNews == null)
            {
                return NotFound();
            }

            return View(proNews);
        }






        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.ProNews == null)
            {
                return Problem("Entity set 'Context.ProNews'  is null.");
            }
            var proNews = await _context.ProNews.FindAsync(id);
            if (proNews != null)
            {
                try
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/News/" + proNews.Picture);

                }
                catch (Exception)
                {


                }

                _context.ProNews.Remove(proNews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private bool ProNewsExists(int id)
        {
            return _context.ProNews.Any(e => e.id == id);
        }
    }
}

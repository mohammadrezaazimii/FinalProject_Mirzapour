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
    public class ShortNewsController : Controller
    {
        #region DataBase


        private readonly Context _context;

        public ShortNewsController(Context context)
        {
            _context = context;
        }

        #endregion


        #region Index


        public async Task<IActionResult> Index()
        {
            return View(await _context.ShortNews.ToListAsync());
        }


        #endregion

        #region Details


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortNews == null)
            {
                return NotFound();
            }

            return View(shortNews);
        }

        #endregion



        #region Create

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ShortNews shortNews, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                #region Image
                if (Image != null)
                {


                    string imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    string _path = "wwwroot/Image/ShortNews/" + imageName;
                    using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                    {
                        await Image.CopyToAsync(f);

                    }
                    shortNews.Picture = imageName;
                }
                else
                {
                    ModelState.AddModelError("Picture", "وارد کردن عکس الزامیست");
                    return View(shortNews);
                }
                #endregion
                shortNews.date = DateTime.Now;



                _context.Add(shortNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shortNews);
        }




        #endregion

        #region Edit



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews.FindAsync(id);
            if (shortNews == null)
            {
                return NotFound();
            }
            return View(shortNews);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ShortNews shortNews, IFormFile Image)
        {
            if (id != shortNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                #region Image

                ShortNews OldPic = await _context.ShortNews.FindAsync(id);
                if (Image != null)
                {
                    try
                    {

                        System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/ShortNews/" + OldPic.Picture);


                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError("Picture", "مشکل در حذف عکس");
                        return View(shortNews);
                    }
                    string imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    string _path = "wwwroot/Image/ShortNews/" + imageName;
                    using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                    {
                        await Image.CopyToAsync(f);

                    }
                    shortNews.Picture = imageName;


                }
                else
                {
                    shortNews.Picture = OldPic.Picture;
                }

                #endregion



                try
                {
                    _context.Entry(OldPic).State = EntityState.Detached;
                    _context.Update(shortNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortNewsExists(shortNews.id))
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
            return View(shortNews);
        }





        #endregion


        #region Delete


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortNews == null)
            {
                return NotFound();
            }

            return View(shortNews);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShortNews == null)
            {
                return Problem("Entity set 'Context.ShortNews'  is null.");
            }
            var shortNews = await _context.ShortNews.FindAsync(id);
            if (shortNews != null)
            {

                try
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/ShortNews/" + shortNews.Picture);

                }
                catch (Exception)
                {


                }


                _context.ShortNews.Remove(shortNews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion



        private bool ShortNewsExists(int id)
        {
            return _context.ShortNews.Any(e => e.id == id);
        }
    }
}

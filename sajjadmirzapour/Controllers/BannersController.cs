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
    public class BannersController : Controller
    {

        #region Data Base
        private readonly Context _context;

        public BannersController(Context context)
        {
            _context = context;
        }
        #endregion
        #region Index

        public async Task<IActionResult> Index()
        {
            return View(await _context.Banner.ToListAsync());
        }


        #endregion

        #region Edit


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Banner == null)
            {
                return NotFound();
            }

            var banner = await _context.Banner.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Picture1,Picture2,Picture3")] Banner banner, IFormFile Image1, IFormFile Image2, IFormFile Image3)
        {
            if (id != banner.id)
            {
                return NotFound();
            }



            #region Pic 1

            Banner OldPic1 = await _context.Banner.FindAsync(1);
            if (Image1 != null)
            {
                try
                {

                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/Banner/" + OldPic1.Picture1);


                }
                catch (Exception)
                {

                    ModelState.AddModelError("Picture1", "مشکل در حذف عکس");
                    return View();
                }
                string imageName = Guid.NewGuid() + Path.GetExtension(Image1.FileName);
                string _path = "wwwroot/Image/Banner/" + imageName;
                using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                {
                    await Image1.CopyToAsync(f);

                }
                banner.Picture1 = imageName;


            }
            else
            {
                banner.Picture1 = OldPic1.Picture1;
            }
            _context.Entry(OldPic1).State = EntityState.Detached;


            #endregion



            #region Pic 2

            Banner OldPic2 = await _context.Banner.FindAsync(1);
            if (Image2 != null)
            {
                try
                {

                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/Banner/" + OldPic2.Picture2);


                }
                catch (Exception)
                {

                    ModelState.AddModelError("Picture2", "مشکل در حذف عکس");
                    return View();
                }
                string imageName = Guid.NewGuid() + Path.GetExtension(Image2.FileName);
                string _path = "wwwroot/Image/Banner/" + imageName;
                using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                {
                    await Image2.CopyToAsync(f);

                }
                banner.Picture2 = imageName;


            }
            else
            {
                banner.Picture2 = OldPic2.Picture2;
            }
            _context.Entry(OldPic2).State = EntityState.Detached;


            #endregion

            #region Pic 3

            Banner OldPic3 = await _context.Banner.FindAsync(1);
            if (Image3 != null)
            {
                try
                {

                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/Banner/" + OldPic2.Picture3);


                }
                catch (Exception)
                {

                    ModelState.AddModelError("Picture3", "مشکل در حذف عکس");
                    return View();
                }
                string imageName = Guid.NewGuid() + Path.GetExtension(Image3.FileName);
                string _path = "wwwroot/Image/Banner/" + imageName;
                using (FileStream f = new FileStream(_path, FileMode.CreateNew))
                {
                    await Image3.CopyToAsync(f);

                }
                banner.Picture3 = imageName;


            }
            else
            {
                banner.Picture3 = OldPic2.Picture3;
            }
            _context.Entry(OldPic3).State = EntityState.Detached;


            #endregion





            try
            {
                _context.Update(banner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(banner.id))
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



        #endregion

        private bool BannerExists(int id)
        {
            return _context.Banner.Any(e => e.id == id);
        }
    }
}

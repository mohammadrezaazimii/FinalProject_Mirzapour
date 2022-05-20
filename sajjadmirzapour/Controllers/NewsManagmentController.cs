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
    public class NewsManagmentController : Controller
    {
        private readonly Context _context;

        public NewsManagmentController(Context context)
        {
            _context = context;
        }

        // GET: NewsManagment
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }

        // GET: NewsManagment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: NewsManagment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsManagment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsID,NewsTitle,NewsDescription,NewsImage")] News news, IFormFile NewsImage)
        {
            if (ModelState.IsValid)
            {
                if (NewsImage == null)
                {
                    ModelState.AddModelError("NewsImage", "فیلد تصویر نباید خالی باشد");
                    return View();
                }


                string ImageName = Guid.NewGuid() + Path.GetExtension(NewsImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/News/", ImageName);
                using (FileStream f = new FileStream(path, FileMode.CreateNew))
                {
                    await NewsImage.CopyToAsync(f);
                    
                }


                News CreateNews = new News()
                {
                    NewsID = news.NewsID,
                    NewsImage = ImageName,
                    NewsDescription = news.NewsDescription,
                    NewsTitle = news.NewsTitle,
                };
                await _context.AddAsync(CreateNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: NewsManagment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: NewsManagment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsID,NewsTitle,NewsDescription,NewsImage")] News news, IFormFile NewsImage)
        {
            if (id != news.NewsID)
            {
                return NotFound();
            }
            else if (news.NewsTitle != null && news.NewsDescription != null)
            {

                News oldnews = _context.News.Find(news.NewsID);
                oldnews.NewsDescription = news.NewsDescription;
                oldnews.NewsTitle = news.NewsTitle;

                if (NewsImage != null)
                {


                    string ImageName = Guid.NewGuid() + Path.GetExtension(NewsImage.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/News/", ImageName);
                    using (FileStream f = new FileStream(path, FileMode.CreateNew))
                    {
                        await NewsImage.CopyToAsync(f);
                    }
                    if (_context.News.Find(news.NewsID).NewsImage != null)
                    {
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/News/" + oldnews.NewsImage);
                    }
                    oldnews.NewsImage = ImageName;
                }
                else
                {
                    string ImageName = oldnews.NewsImage;
                    oldnews.NewsImage = ImageName;
                }
                
                



                _context.Update(oldnews);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("NewsTitle", "مقادیر اشتباه یا خالی وارد شده اند");
                return View(news);
            }

        }

        // GET: NewsManagment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: NewsManagment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news.NewsImage != null)
            {
                System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/wwwroot/Image/News/" + news.NewsImage);
            }
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsID == id);
        }
    }
}

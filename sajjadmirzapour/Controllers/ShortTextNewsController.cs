using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sajjadmirzapour;
using sajjadmirzapour.Models;

namespace sajjadmirzapour.Controllers
{
    public class ShortTextNewsController : Controller
    {
        #region DATA BASE


        private readonly Context _context;

        public ShortTextNewsController(Context context)
        {
            _context = context;
        }
        #endregion



        #region Index



        public async Task<IActionResult> Index()
        {
            return View(await _context.ShortTextNews.ToListAsync());
        }


        #endregion




        #region Details  

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortTextNews == null)
            {
                return NotFound();
            }

            return View(shortTextNews);
        }


        #endregion




        #region Create  

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Description")] ShortTextNews shortTextNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shortTextNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shortTextNews);
        }



        #endregion

        #region Edit





        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews.FindAsync(id);
            if (shortTextNews == null)
            {
                return NotFound();
            }
            return View(shortTextNews);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Description")] ShortTextNews shortTextNews)
        {
            if (id != shortTextNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortTextNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortTextNewsExists(shortTextNews.id))
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
            return View(shortTextNews);
        }


        #endregion




        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortTextNews == null)
            {
                return NotFound();
            }

            return View(shortTextNews);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShortTextNews == null)
            {
                return Problem("Entity set 'Context.ShortTextNews'  is null.");
            }
            var shortTextNews = await _context.ShortTextNews.FindAsync(id);
            if (shortTextNews != null)
            {
                _context.ShortTextNews.Remove(shortTextNews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        #endregion



        private bool ShortTextNewsExists(int id)
        {
            return _context.ShortTextNews.Any(e => e.id == id);
        }
    }
}

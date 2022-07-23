using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationLinksTelegramBot.Authentication;
using WebApplicationLinksTelegramBot.Database;
using WebApplicationLinksTelegramBot.Models;

namespace WebApplicationLinksTelegramBot.Controllers
{
    [BasicAuthorization]
    public class LinksController : Controller
    {
        private readonly LinkstelegrambotContext _context;
        
        public LinksController(LinkstelegrambotContext context)
        {
            _context = context;
        }

        // GET: Links
        public IActionResult Index()
        {
            var existsUser = _context.Auths.Where(context => context.Login == User.Identity.Name).Any();
            long authId;
            if (existsUser)
                authId = _context.Auths.Where(context => context.Login == User.Identity.Name).FirstOrDefault().UserId;
            else
                return Problem("User not found");

            TgBot tgBot = new()
            {
                Links = _context.Links.Where(context => context.UserId == authId).ToList()
            };
            return tgBot != null ?
                          View(tgBot) :
                          Problem("Entity set 'LinkstelegrambotContext.Links'  is null.");
        }

        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Url,UserId")] Link link)
        {
            if (ModelState.IsValid)
            {
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Url,UserId")] Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
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
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Links == null)
            {
                return Problem("Entity set 'LinkstelegrambotContext.Links'  is null.");
            }
            var link = await _context.Links.FindAsync(id);
            if (link != null)
            {
                _context.Links.Remove(link);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkExists(int id)
        {
          return (_context.Links?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

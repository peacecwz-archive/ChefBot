using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChefBot.EF;
using ChefBot.EF.Tables;
using ChefBot.Areas.Admin.Attributes;

namespace ChefBot.Areas.Admin.Controllers
{
    [Admin]
    public class HistoriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/Histories
        public async Task<ActionResult> Index()
        {
            return View(await db.BotHistories.ToListAsync());
        }

        // GET: Admin/Histories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BotHistoryEntity botHistoryEntity = await db.BotHistories.FindAsync(id);
            if (botHistoryEntity == null)
            {
                return HttpNotFound();
            }
            return View(botHistoryEntity);
        }

        // GET: Admin/Histories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BotHistoryEntity botHistoryEntity = await db.BotHistories.FindAsync(id);
            if (botHistoryEntity == null)
            {
                return HttpNotFound();
            }
            return View(botHistoryEntity);
        }

        // POST: Admin/Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BotHistoryEntity botHistoryEntity = await db.BotHistories.FindAsync(id);
            db.BotHistories.Remove(botHistoryEntity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

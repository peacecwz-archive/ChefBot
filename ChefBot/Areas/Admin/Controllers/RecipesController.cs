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
    public class RecipesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/Recipes
        public async Task<ActionResult> Index()
        {
            return View(await db.Recipes.ToListAsync());
        }

        // GET: Admin/Recipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeEntity recipeEntity = await db.Recipes.FindAsync(id);
            if (recipeEntity == null)
            {
                return HttpNotFound();
            }
            return View(recipeEntity);
        }

        // GET: Admin/Recipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Ingredients,ImageUrl,CreateDate")] RecipeEntity recipeEntity)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipeEntity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recipeEntity);
        }

        // GET: Admin/Recipes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeEntity recipeEntity = await db.Recipes.FindAsync(id);
            if (recipeEntity == null)
            {
                return HttpNotFound();
            }
            return View(recipeEntity);
        }

        // POST: Admin/Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Ingredients,ImageUrl,CreateDate")] RecipeEntity recipeEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeEntity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recipeEntity);
        }

        // GET: Admin/Recipes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeEntity recipeEntity = await db.Recipes.FindAsync(id);
            if (recipeEntity == null)
            {
                return HttpNotFound();
            }
            return View(recipeEntity);
        }

        // POST: Admin/Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecipeEntity recipeEntity = await db.Recipes.FindAsync(id);
            db.Recipes.Remove(recipeEntity);
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

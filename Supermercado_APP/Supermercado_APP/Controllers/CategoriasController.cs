using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermercado_APP.Models;

namespace Supermercado_APP.Controllers
{
    public class CategoriasController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            var tblCategorias = db.tblCategorias.Include(t => t.tblUsuario).Include(t => t.tblUsuario1);
            return View(await tblCategorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = await db.tblCategorias.FindAsync(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            ViewBag.Cat_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Cat_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cat_Id,Cat_Descripcion,Cat_Estado,Cat_UsuarioCrea,Cat_FechaCrea,Cat_UsuarioModifica,Cat_FechaModifica")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tblCategorias.Add(tblCategoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioCrea);
            ViewBag.Cat_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioModifica);
            return View(tblCategoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = await db.tblCategorias.FindAsync(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioCrea);
            ViewBag.Cat_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioModifica);
            return View(tblCategoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cat_Id,Cat_Descripcion,Cat_Estado,Cat_UsuarioCrea,Cat_FechaCrea,Cat_UsuarioModifica,Cat_FechaModifica")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblCategoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioCrea);
            ViewBag.Cat_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioModifica);
            return View(tblCategoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = await db.tblCategorias.FindAsync(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCategoria tblCategoria = await db.tblCategorias.FindAsync(id);
            db.tblCategorias.Remove(tblCategoria);
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

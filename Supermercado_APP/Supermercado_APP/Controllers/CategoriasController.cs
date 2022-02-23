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
        public ActionResult Index()
        {
            var tblCategorias = db.VW_Categorias.Where(x => x.Estado == "Activo");
            return View(tblCategorias.ToList());
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
        public ActionResult Create([Bind(Include = "Cat_Descripcion,Cat_UsuarioCrea")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                int usu = int.Parse(Session["UsuarioId"].ToString());
                db.UDP_Categoria_INSERT(tblCategoria.Cat_Descripcion, usu);
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
        public ActionResult Edit([Bind(Include = "Cat_Id,Cat_Descripcion,Cat_UsuarioModifica")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                int usu = int.Parse(Session["UsuarioId"].ToString());
                db.UDP_Categoria_UPDATE(tblCategoria.Cat_Id, tblCategoria.Cat_Descripcion, usu);
                return RedirectToAction("Index");
            }
            ViewBag.Cat_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioCrea);
            ViewBag.Cat_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCategoria.Cat_UsuarioModifica);
            return View(tblCategoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            int usu = int.Parse(Session["UsuarioId"].ToString());
            db.UDP_Categoria_DELETE(id, usu);
            return RedirectToAction("Index");
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

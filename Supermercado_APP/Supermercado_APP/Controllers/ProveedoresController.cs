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
    public class ProveedoresController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Proveedores
        public async Task<ActionResult> Index()
        {
            var tblProveedores = db.tblProveedores.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblDireccione);
            return View(await tblProveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedore tblProveedore = await db.tblProveedores.FindAsync(id);
            if (tblProveedore == null)
            {
                return HttpNotFound();
            }
            return View(tblProveedore);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector");
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Pro_Id,Pro_Empresa,Pro_RepNombreS,Pro_RepPriApellido,Pro_RepSegApellido,Dir_Id,Pro_TelFijo,Pro_TelMovil,Pro_Email,Pro_PaginaWeb,Pro_Estado,Pro_UsuarioCrea,Pro_FechaCrea,Pro_UsuarioModifica,Pro_FechaModifica")] tblProveedore tblProveedore)
        {
            if (ModelState.IsValid)
            {
                db.tblProveedores.Add(tblProveedore);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblProveedore.Dir_Id);
            return View(tblProveedore);
        }

        // GET: Proveedores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedore tblProveedore = await db.tblProveedores.FindAsync(id);
            if (tblProveedore == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblProveedore.Dir_Id);
            return View(tblProveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Pro_Id,Pro_Empresa,Pro_RepNombreS,Pro_RepPriApellido,Pro_RepSegApellido,Dir_Id,Pro_TelFijo,Pro_TelMovil,Pro_Email,Pro_PaginaWeb,Pro_Estado,Pro_UsuarioCrea,Pro_FechaCrea,Pro_UsuarioModifica,Pro_FechaModifica")] tblProveedore tblProveedore)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblProveedore).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Pro_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProveedore.Pro_UsuarioCrea);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblProveedore.Dir_Id);
            return View(tblProveedore);
        }

        // GET: Proveedores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedore tblProveedore = await db.tblProveedores.FindAsync(id);
            if (tblProveedore == null)
            {
                return HttpNotFound();
            }
            return View(tblProveedore);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblProveedore tblProveedore = await db.tblProveedores.FindAsync(id);
            db.tblProveedores.Remove(tblProveedore);
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

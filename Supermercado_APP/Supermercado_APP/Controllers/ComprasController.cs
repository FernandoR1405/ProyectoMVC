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
    public class ComprasController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Compras
        public async Task<ActionResult> Index()
        {
            var tblCompras = db.tblCompras.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblProducto);
            return View(await tblCompras.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = await db.tblCompras.FindAsync(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            return View(tblCompra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.Cop_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Cop_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cop_Id,Cop_NumFactura,Cop_Fecha,Prd_Id,Cop_Cantidad,Cop_Estado,Cop_UsuarioCrea,Cop_FechaCrea,Cop_UsuarioModifica,Cop_FechaModifica")] tblCompra tblCompra)
        {
            if (ModelState.IsValid)
            {
                db.tblCompras.Add(tblCompra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cop_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioCrea);
            ViewBag.Cop_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblCompra.Prd_Id);
            return View(tblCompra);
        }

        // GET: Compras/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = await db.tblCompras.FindAsync(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cop_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioCrea);
            ViewBag.Cop_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblCompra.Prd_Id);
            return View(tblCompra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cop_Id,Cop_NumFactura,Cop_Fecha,Prd_Id,Cop_Cantidad,Cop_Estado,Cop_UsuarioCrea,Cop_FechaCrea,Cop_UsuarioModifica,Cop_FechaModifica")] tblCompra tblCompra)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblCompra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cop_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioCrea);
            ViewBag.Cop_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblCompra.Cop_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblCompra.Prd_Id);
            return View(tblCompra);
        }

        // GET: Compras/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = await db.tblCompras.FindAsync(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            return View(tblCompra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCompra tblCompra = await db.tblCompras.FindAsync(id);
            db.tblCompras.Remove(tblCompra);
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

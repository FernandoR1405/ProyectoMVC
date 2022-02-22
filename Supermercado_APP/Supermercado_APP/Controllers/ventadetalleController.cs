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
    public class ventadetalleController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: ventadetalle
        public async Task<ActionResult> Index()
        {
            var tblventadetalles = db.tblventadetalles.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblProducto).Include(t => t.tblVentaEncabezado);
            return View(await tblventadetalles.ToListAsync());
        }

        // GET: ventadetalle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblventadetalle tblventadetalle = await db.tblventadetalles.FindAsync(id);
            if (tblventadetalle == null)
            {
                return HttpNotFound();
            }
            return View(tblventadetalle);
        }

        // GET: ventadetalle/Create
        public ActionResult Create()
        {
            ViewBag.VentDet_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.VentDet_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo");
            ViewBag.VentEnc_Id = new SelectList(db.tblVentaEncabezadoes, "VentEnc_Id", "VentEnc_Id");
            return View();
        }

        // POST: ventadetalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VentDet_Id,VentEnc_Id,Prd_Id,VentDet_Precio,VentDet_Cantidad,VentDet_Descuento,VentDet_Impuesto,VentDet_Subtotal,VentDet_Activo,VentDet_UsuarioCrea,VentDet_FechaCrea,VentDet_UsuarioModifica,VentDet_FechaModifica")] tblventadetalle tblventadetalle)
        {
            if (ModelState.IsValid)
            {
                db.tblventadetalles.Add(tblventadetalle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VentDet_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioCrea);
            ViewBag.VentDet_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblventadetalle.Prd_Id);
            ViewBag.VentEnc_Id = new SelectList(db.tblVentaEncabezadoes, "VentEnc_Id", "VentEnc_Id", tblventadetalle.VentEnc_Id);
            return View(tblventadetalle);
        }

        // GET: ventadetalle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblventadetalle tblventadetalle = await db.tblventadetalles.FindAsync(id);
            if (tblventadetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentDet_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioCrea);
            ViewBag.VentDet_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblventadetalle.Prd_Id);
            ViewBag.VentEnc_Id = new SelectList(db.tblVentaEncabezadoes, "VentEnc_Id", "VentEnc_Id", tblventadetalle.VentEnc_Id);
            return View(tblventadetalle);
        }

        // POST: ventadetalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VentDet_Id,VentEnc_Id,Prd_Id,VentDet_Precio,VentDet_Cantidad,VentDet_Descuento,VentDet_Impuesto,VentDet_Subtotal,VentDet_Activo,VentDet_UsuarioCrea,VentDet_FechaCrea,VentDet_UsuarioModifica,VentDet_FechaModifica")] tblventadetalle tblventadetalle)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblventadetalle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VentDet_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioCrea);
            ViewBag.VentDet_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblventadetalle.VentDet_UsuarioModifica);
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Codigo", tblventadetalle.Prd_Id);
            ViewBag.VentEnc_Id = new SelectList(db.tblVentaEncabezadoes, "VentEnc_Id", "VentEnc_Id", tblventadetalle.VentEnc_Id);
            return View(tblventadetalle);
        }

        // GET: ventadetalle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblventadetalle tblventadetalle = await db.tblventadetalles.FindAsync(id);
            if (tblventadetalle == null)
            {
                return HttpNotFound();
            }
            return View(tblventadetalle);
        }

        // POST: ventadetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblventadetalle tblventadetalle = await db.tblventadetalles.FindAsync(id);
            db.tblventadetalles.Remove(tblventadetalle);
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

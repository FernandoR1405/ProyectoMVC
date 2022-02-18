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
    public class VentaEncabezadoController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: VentaEncabezado
        public async Task<ActionResult> Index()
        {
            var tblVentaEncabezadoes = db.tblVentaEncabezadoes.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblPersona);
            return View(await tblVentaEncabezadoes.ToListAsync());
        }

        // GET: VentaEncabezado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVentaEncabezado tblVentaEncabezado = await db.tblVentaEncabezadoes.FindAsync(id);
            if (tblVentaEncabezado == null)
            {
                return HttpNotFound();
            }
            return View(tblVentaEncabezado);
        }

        // GET: VentaEncabezado/Create
        public ActionResult Create()
        {
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Descripcion");
            ViewBag.VentEnc_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.VentEnc_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Per_Id = new SelectList(db.tblPersonas, "Per_Id", "Per_Identidad");
            return View();
        }

        // POST: VentaEncabezado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VentEnc_Id,VentEnc_NumFactura,VentEnc_Fecha,Per_Id,VentEnc_Total")] tblVentaEncabezado tblVentaEncabezado, [Bind(Include = "Prd_Id,VentDet_Precio,VentDet_Cantidad,VentDet_Descuento,VentDet_Impuesto,VentDet_Subtotal")] tblventadetalle tblventadetalle)
        {
            ModelState.Remove("VentEnc_Total");
            ModelState.Remove("VentDet_Subtotal");
            if (ModelState.IsValid)
            {
                db.UDP_Venta_INSERT(tblVentaEncabezado.VentEnc_NumFactura, tblVentaEncabezado.VentEnc_Fecha, tblVentaEncabezado.VentEnc_Total, 1, tblventadetalle.Prd_Id, tblventadetalle.VentDet_Precio, tblventadetalle.VentDet_Cantidad, tblventadetalle.VentDet_Descuento, tblventadetalle.VentDet_Impuesto,0);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VentEnc_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioCrea);
            ViewBag.VentEnc_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioModifica);
            ViewBag.Per_Id = new SelectList(db.tblPersonas, "Per_Id", "Per_Identidad", tblVentaEncabezado.Per_Id);
            return View(tblVentaEncabezado);
        }

        // GET: VentaEncabezado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVentaEncabezado tblVentaEncabezado = await db.tblVentaEncabezadoes.FindAsync(id);
            if (tblVentaEncabezado == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentEnc_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioCrea);
            ViewBag.VentEnc_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioModifica);
            ViewBag.Per_Id = new SelectList(db.tblPersonas, "Per_Id", "Per_Identidad", tblVentaEncabezado.Per_Id);
            return View(tblVentaEncabezado);
        }

        // POST: VentaEncabezado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VentEnc_Id,VentEnc_NumFactura,VentEnc_Fecha,Per_Id,VentEnc_Total,VentEnc_Activo,VentEnc_UsuarioCrea,VentEnc_FechaCrea,VentEnc_UsuarioModifica,VentEnc_FechaModifica")] tblVentaEncabezado tblVentaEncabezado)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblVentaEncabezado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VentEnc_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioCrea);
            ViewBag.VentEnc_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblVentaEncabezado.VentEnc_UsuarioModifica);
            ViewBag.Per_Id = new SelectList(db.tblPersonas, "Per_Id", "Per_Identidad", tblVentaEncabezado.Per_Id);
            return View(tblVentaEncabezado);
        }

        // GET: VentaEncabezado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVentaEncabezado tblVentaEncabezado = await db.tblVentaEncabezadoes.FindAsync(id);
            if (tblVentaEncabezado == null)
            {
                return HttpNotFound();
            }
            return View(tblVentaEncabezado);
        }

        // POST: VentaEncabezado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblVentaEncabezado tblVentaEncabezado = await db.tblVentaEncabezadoes.FindAsync(id);
            db.tblVentaEncabezadoes.Remove(tblVentaEncabezado);
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

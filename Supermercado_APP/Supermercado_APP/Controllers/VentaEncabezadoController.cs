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
using Supermercado_APP.Models.ViewModels;

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

        public async Task<ActionResult> IndexProducto()
        {
            var tblProductos = db.tblProductos.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblCategoria).Include(t => t.tblProveedore);
            return View(await tblProductos.ToListAsync());
        }

        [HttpPost]
        public JsonResult GetProductos(int id)
        {
            var list = db.tblProductos.ToList();
            return Json(list,JsonRequestBehavior.AllowGet);
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
            ViewBag.Prd_Id = new SelectList(db.tblProductos, "Prd_Id", "Prd_Id");
            //ViewBag.VentEnc_Id = new SelectList(db.tblVentaEncabezadoes, "VentEnc_Id", "VentEnc_Id");
            return View();
        }

        //Obtener listado de Productos
        [HttpGet]
        public async Task<ActionResult> ObtenerListadoProductos()
        {
            var data = db.tblProductos.Select(x => new {
                Prod_Id = x.Prd_Id,
                Prod_Descripcion = x.Prd_Descripcion,
                Prod_Precio = x.Prd_PrecioVenta
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> CreateFactura(int NoFactura, decimal Total)
        {
            int usu = int.Parse(Session["UsuarioId"].ToString());
            try
            {
                DateTime FechaActual = DateTime.Now;

                List<VentaDetalle_ViewModel> ListaDetalle = (List<VentaDetalle_ViewModel>)HttpContext.Session["ListaDetalle"];

                //Aquí hacer el insert del encabezado

                db.UDP_VentaEncabezado_INSERT(NoFactura,Total,usu);

                //luego hacer el insert del detalle con un foreach

                foreach (var item in ListaDetalle)
                {
                        db.UDP_VentaDetalle_INSERT(item.prod_Id,item.prod_Precio,item.prod_Cantidad, usu);
                }

                return Json("Bien", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ListaDetalleSesion(List<VentaDetalle_ViewModel> ListaDetalle)
        {
            try
            {
                Session["ListaDetalle"] = ListaDetalle;
                return Json("Bien", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: VentaEncabezado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VentEnc_Id,VentEnc_NumFactura,VentEnc_Fecha,Per_Id,VentEnc_Total")] tblVentaEncabezado tblVentaEncabezado, [Bind(Include = "Prd_Id,VentDet_Precio,VentDet_Cantidad,VentDet_Descuento,VentDet_Impuesto,VentDet_Subtotal")] tblventadetalle tblventadetalle, [Bind(Include = "Per_Identidad,Per_Rtn,Per_Nombres,Per_PrimerApellido,Per_SegundoApellido,Per_Sexo,Dir_Id,Per_Telefono,Per_Correo")] tblPersona tblPersona)
        {
            ModelState.Remove("VentEnc_Total");
            ModelState.Remove("VentDet_Subtotal"); 
            if (ModelState.IsValid)
            {
                //db.UDP_tblPersonas_Insert(tblPersona.Per_Identidad,tblPersona.Per_Rtn,tblPersona.Per_Nombres,tblPersona.Per_PrimerApellido,tblPersona.Per_SegundoApellido,tblPersona.Per_Sexo,1,tblPersona.Per_Telefono,tblPersona.Per_Correo,1);
                //db.UDP_Venta_INSERT(tblVentaEncabezado.VentEnc_NumFactura, tblVentaEncabezado.VentEnc_Fecha, tblVentaEncabezado.VentEnc_Total, 1, 1, tblventadetalle.VentDet_Precio, tblventadetalle.VentDet_Cantidad, tblventadetalle.VentDet_Descuento, tblventadetalle.VentDet_Impuesto, 0);
                //await db.SaveChangesAsync();
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

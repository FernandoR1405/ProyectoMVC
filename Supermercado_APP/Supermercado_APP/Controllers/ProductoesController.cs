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
    public class ProductoesController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Productoes
        public async Task<ActionResult> Index()
        {
            var tblProductos = db.tblProductos.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblCategoria).Include(t => t.tblProveedore);
            return View(await tblProductos.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = await db.tblProductos.FindAsync(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.Prd_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Prd_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Cat_Id = new SelectList(db.tblCategorias, "Cat_Id", "Cat_Descripcion");
            ViewBag.Pro_Id = new SelectList(db.tblProveedores, "Pro_Id", "Pro_Empresa");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Prd_Id,Prd_Codigo,Prd_Descripcion,Cat_Id,Pro_Id,Prd_Stock,Prd_PrecioCompra,Prd_PrecioVenta,Prd_Imagen,Prd_Estado,Prd_UsuarioCrea,Prd_FechaCrea,Prd_UsuarioModifica,Prd_FechaModifica")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                db.tblProductos.Add(tblProducto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Prd_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioCrea);
            ViewBag.Prd_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioModifica);
            ViewBag.Cat_Id = new SelectList(db.tblCategorias, "Cat_Id", "Cat_Descripcion", tblProducto.Cat_Id);
            ViewBag.Pro_Id = new SelectList(db.tblProveedores, "Pro_Id", "Pro_Empresa", tblProducto.Pro_Id);
            return View(tblProducto);
        }

        // GET: Productoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = await db.tblProductos.FindAsync(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Prd_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioCrea);
            ViewBag.Prd_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioModifica);
            ViewBag.Cat_Id = new SelectList(db.tblCategorias, "Cat_Id", "Cat_Descripcion", tblProducto.Cat_Id);
            ViewBag.Pro_Id = new SelectList(db.tblProveedores, "Pro_Id", "Pro_Empresa", tblProducto.Pro_Id);
            return View(tblProducto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Prd_Id,Prd_Codigo,Prd_Descripcion,Cat_Id,Pro_Id,Prd_Stock,Prd_PrecioCompra,Prd_PrecioVenta,Prd_Imagen,Prd_Estado,Prd_UsuarioCrea,Prd_FechaCrea,Prd_UsuarioModifica,Prd_FechaModifica")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblProducto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Prd_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioCrea);
            ViewBag.Prd_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblProducto.Prd_UsuarioModifica);
            ViewBag.Cat_Id = new SelectList(db.tblCategorias, "Cat_Id", "Cat_Descripcion", tblProducto.Cat_Id);
            ViewBag.Pro_Id = new SelectList(db.tblProveedores, "Pro_Id", "Pro_Empresa", tblProducto.Pro_Id);
            return View(tblProducto);
        }

        // GET: Productoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = await db.tblProductos.FindAsync(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblProducto tblProducto = await db.tblProductos.FindAsync(id);
            db.tblProductos.Remove(tblProducto);
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

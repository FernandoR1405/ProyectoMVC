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
    public class PersonasController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Personas
        public async Task<ActionResult> Index()
        {
            var tblPersonas = db.tblPersonas.Include(t => t.tblUsuario).Include(t => t.tblUsuario1).Include(t => t.tblDireccione);
            return View(await tblPersonas.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersona tblPersona = await db.tblPersonas.FindAsync(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            return View(tblPersona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.Per_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Per_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Per_Id,Per_Identidad,Per_Rtn,Per_Nombres,Per_PrimerApellido,Per_SegundoApellido,Per_Sexo,Dir_Id,Per_Telefono,Per_Correo,Per_EsActivo,Per_UsuarioCrea,Per_FechaCrea,Per_UsuarioModifica,Per_FechaModifica")] tblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                db.tblPersonas.Add(tblPersona);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Per_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioCrea);
            ViewBag.Per_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioModifica);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblPersona.Dir_Id);
            return View(tblPersona);
        }

        // GET: Personas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersona tblPersona = await db.tblPersonas.FindAsync(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.Per_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioCrea);
            ViewBag.Per_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioModifica);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblPersona.Dir_Id);
            return View(tblPersona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Per_Id,Per_Identidad,Per_Rtn,Per_Nombres,Per_PrimerApellido,Per_SegundoApellido,Per_Sexo,Dir_Id,Per_Telefono,Per_Correo,Per_EsActivo,Per_UsuarioCrea,Per_FechaCrea,Per_UsuarioModifica,Per_FechaModifica")] tblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tblPersona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Per_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioCrea);
            ViewBag.Per_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblPersona.Per_UsuarioModifica);
            ViewBag.Dir_Id = new SelectList(db.tblDirecciones, "Dir_Id", "Dir_Sector", tblPersona.Dir_Id);
            return View(tblPersona);
        }

        // GET: Personas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPersona tblPersona = await db.tblPersonas.FindAsync(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            return View(tblPersona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblPersona tblPersona = await db.tblPersonas.FindAsync(id);
            db.tblPersonas.Remove(tblPersona);
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

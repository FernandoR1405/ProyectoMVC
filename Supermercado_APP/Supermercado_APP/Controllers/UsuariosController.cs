using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermercado_APP.Models;

namespace Supermercado_APP.Controllers
{
    public class UsuariosController : Controller
    {
        private SupermercadoDBEntities db = new SupermercadoDBEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var tblUsuarios = db.VW_Usuarios;
            return View(tblUsuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuarios.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.CarP_Id = new SelectList(db.tblCargosPersonas, "CarP_Id", "CarP_Id");
            ViewBag.Usu_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            ViewBag.Usu_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Usu_UsuarioNombre,Usu_Contrasena,Usu_EsAdmin,Usu_UsuarioCrea")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                int usu = 1;
                db.UDP_Usuario_INSERT(tblUsuario.Usu_UsuarioNombre, tblUsuario.Usu_Contrasena, tblUsuario.Usu_EsAdmin,usu);
                //db.tblUsuarios.Add(tblUsuario);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarP_Id = new SelectList(db.tblCargosPersonas, "CarP_Id", "CarP_Id", tblUsuario.CarP_Id);
            ViewBag.Usu_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioCrea);
            ViewBag.Usu_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioModifica);
            return View(tblUsuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuarios.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarP_Id = new SelectList(db.tblCargosPersonas, "CarP_Id", "CarP_Id", tblUsuario.CarP_Id);
            ViewBag.Usu_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioCrea);
            ViewBag.Usu_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioModifica);
            return View(tblUsuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Usu_Id,Usu_UsuarioNombre,Usu_Contrasena,Usu_EsAdmin,Usu_UsuarioModifica")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                int usu = 1;
                db.UDP_Usuario_UPDATE(tblUsuario.Usu_Id,tblUsuario.Usu_UsuarioNombre, tblUsuario.Usu_Contrasena, tblUsuario.Usu_EsAdmin,usu);
                //db.Entry(tblUsuario).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarP_Id = new SelectList(db.tblCargosPersonas, "CarP_Id", "CarP_Id", tblUsuario.CarP_Id);
            ViewBag.Usu_UsuarioCrea = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioCrea);
            ViewBag.Usu_UsuarioModifica = new SelectList(db.tblUsuarios, "Usu_Id", "Usu_UsuarioNombre", tblUsuario.Usu_UsuarioModifica);
            return View(tblUsuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            db.UDP_Usuario_DELETE(id, 1);
            //tblUsuario tblUsuario = db.tblUsuarios.Find(id);
            //db.tblUsuarios.Remove(tblUsuario);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUsuario tblUsuario = db.tblUsuarios.Find(id);
            db.tblUsuarios.Remove(tblUsuario);
            db.SaveChanges();
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

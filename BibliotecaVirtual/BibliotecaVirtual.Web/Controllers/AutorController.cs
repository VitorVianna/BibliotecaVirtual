using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Dados;

namespace BibliotecaVirtual.Controllers
{
    public class AutorController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Autor
        public ActionResult Index()
        {
            return View(db.Autores.ToList());
        }

        // GET: Autor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorModel autorModel = db.Autores.Find(id);
            if (autorModel == null)
            {
                return HttpNotFound();
            }
            return View(autorModel);
        }

        // GET: Autor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                db.Autores.Add(autorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autorModel);
        }

        // GET: Autor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorModel autorModel = db.Autores.Find(id);
            if (autorModel == null)
            {
                return HttpNotFound();
            }
            return View(autorModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autorModel);
        }

        // GET: Autor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutorModel autorModel = db.Autores.Find(id);
            if (autorModel == null)
            {
                return HttpNotFound();
            }
            return View(autorModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutorModel autorModel = db.Autores.Find(id);
            db.Autores.Remove(autorModel);
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

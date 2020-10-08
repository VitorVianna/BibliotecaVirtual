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
    public class AssuntoController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Assunto
        public ActionResult Index()
        {
            return View(db.Assuntos.ToList());
        }

        // GET: Assunto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssuntoModel assuntoModel = db.Assuntos.Find(id);
            if (assuntoModel == null)
            {
                return HttpNotFound();
            }
            return View(assuntoModel);
        }

        // GET: Assunto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assunto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao")] AssuntoModel assuntoModel)
        {
            if (ModelState.IsValid)
            {
                db.Assuntos.Add(assuntoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assuntoModel);
        }

        // GET: Assunto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssuntoModel assuntoModel = db.Assuntos.Find(id);
            if (assuntoModel == null)
            {
                return HttpNotFound();
            }
            return View(assuntoModel);
        }

        // POST: Assunto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao")] AssuntoModel assuntoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assuntoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assuntoModel);
        }

        // GET: Assunto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssuntoModel assuntoModel = db.Assuntos.Find(id);
            if (assuntoModel == null)
            {
                return HttpNotFound();
            }
            return View(assuntoModel);
        }

        // POST: Assunto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssuntoModel assuntoModel = db.Assuntos.Find(id);
            db.Assuntos.Remove(assuntoModel);
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

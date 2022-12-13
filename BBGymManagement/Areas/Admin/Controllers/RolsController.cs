using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBGymManagement.Models.Context;
using BBGymManagement.Models.Entities;
using BBGymManagement.Services;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class RolsController : Controller
    {
        private RolService _rolService = new RolService();

        // GET: Admin/Rols
        public ActionResult Index()
        {
            return View(_rolService.GetAll());
        }

        // GET: Admin/Rols/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = _rolService.GetById(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // GET: Admin/Rols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Rols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
              _rolService.Add(rol);
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: Admin/Rols/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = _rolService.GetById(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Admin/Rols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
               _rolService.Update(rol,rol.Id);
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // GET: Admin/Rols/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = _rolService.GetById(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Admin/Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _rolService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}

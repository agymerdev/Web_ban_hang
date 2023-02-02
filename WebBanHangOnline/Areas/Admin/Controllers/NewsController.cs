using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        WebBanHangOnlineEntities db = new WebBanHangOnlineEntities();

        // GET: Admin/New
        public ActionResult Index()
        {
            var items=db.tb_News.OrderByDescending(x=>x.Id).ToList(); //OrderByDescending: bản ghi mới nhất lên đầu
            return View(items);
        }
        //nhúng các thư viện
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_News model)
        {
            if (ModelState.IsValid) 
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 1006;
                model.ModifiedDate = DateTime.Now;
                
                db.tb_News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.tb_News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
               
                model.ModifiedDate = DateTime.Now;

                db.tb_News.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
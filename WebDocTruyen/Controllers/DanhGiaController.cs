using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;

namespace WebDocTruyen.Controllers
{
    public class DanhGiaController : Controller
    {
        public WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();

        // GET: DanhGia
        public ActionResult Index()
        {
            var danhGias = objWebDocTruyenEntities.DanhGias.ToList();
            return View(danhGias);
        }

        

        // POST: DanhGia/Create12
        [HttpPost]
        public ActionResult Create(DanhGia danhGia, int Ma_Truyen)
        {
            if (ModelState.IsValid)
            {
                danhGia.Ma_Truyen = Ma_Truyen;
                danhGia.Ma_Nguoidung = Convert.ToInt32(Session["id"]);
                if(objWebDocTruyenEntities.DanhGias.ToList().Count == 0)
                {
                    danhGia.Ma_DanhGia = 1;
                }
                else
                {
                    var lastId = objWebDocTruyenEntities.DanhGias.OrderByDescending(
                   n => n.Ma_DanhGia).Select(n => n.Ma_DanhGia).FirstOrDefault();
                    danhGia.Ma_DanhGia = lastId + 1;
                }
               
                objWebDocTruyenEntities.DanhGias.Add(danhGia);
                objWebDocTruyenEntities.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(danhGia);
        }
    }
}
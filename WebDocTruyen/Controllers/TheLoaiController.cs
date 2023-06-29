using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;

namespace WebDocTruyen.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        public WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        public ActionResult Category(int id)
        {
           List<Truyen> truyen = objWebDocTruyenEntities.Truyens.Where(t => t.Ma_Theloai == id).ToList();
           return View(truyen);
        }

    }
}
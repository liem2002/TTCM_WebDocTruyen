using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;

namespace WebDocTruyen.Controllers
{
   
    public class ChuongController : Controller
    {
        // GET: Chuong
        public WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        public ActionResult Chapter(int id)
        {
            List<Chuong> chuong = objWebDocTruyenEntities.Chuongs.Where(c => c.So_Chuong == id).ToList();

            return View(chuong);
        }
        
    }
}
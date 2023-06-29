using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;
using WebDocTruyen.Models;

namespace WebDocTruyen.Controllers
{
    public class ChiTietController : Controller
    {
        // GET: ChiTiet
        WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        public ActionResult Detail(int id)
        {
            
            DanhSach danhSach = new DanhSach();
            var theloai = objWebDocTruyenEntities.TheLoais.ToList();
            List<Chuong> chuong = objWebDocTruyenEntities.Chuongs.Where(c => c.Ma_Truyen == id).ToList();
            List<DanhGia> danhGias = objWebDocTruyenEntities.DanhGias.Where(c => c.Ma_Truyen == id).ToList();

            var truyen = objWebDocTruyenEntities.Truyens.Where(t => t.Ma_Truyen == id).FirstOrDefault();
            danhSach.danhGia = danhGias;
            danhSach.truyen = truyen;
            danhSach.theLoais = theloai;
            danhSach.chuongs = chuong;
            return View(danhSach);
            
        }
        


    }
}
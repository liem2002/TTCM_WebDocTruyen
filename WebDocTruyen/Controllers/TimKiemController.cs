using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;
using WebDocTruyen.Models;

namespace WebDocTruyen.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        
        public ActionResult KQTimKiem(string sTuKhoa, int? id)
        {
            // tìm kiếm theo tên Truyện
            DanhSach danhSach = new DanhSach();
            var theloai = objWebDocTruyenEntities.TheLoais.ToList();
            List<Chuong> chuong = objWebDocTruyenEntities.Chuongs.Where(c => c.Ma_Truyen == id).ToList();
            var lstTruyen = objWebDocTruyenEntities.Truyens.Where(t => t.Ten_Truyen.Contains(sTuKhoa)).FirstOrDefault();
            danhSach.truyen = lstTruyen;
            danhSach.theLoais = theloai;
            danhSach.chuongs = chuong;
            return View(danhSach);
            
            
        }
    }
}
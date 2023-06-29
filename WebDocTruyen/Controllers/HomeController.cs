using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebDocTruyen.Context;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace WebDocTruyen.Controllers

{
    public class HomeController : Controller
    {
        public WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        public ActionResult Index()
        {
            var lstTruyen = objWebDocTruyenEntities.Truyens.ToList();
            return View(lstTruyen);
        }
        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Nguoidung nd, FormCollection f)
        {
            // kiem cha captcha hợp lệ
            
            
                var lastId = objWebDocTruyenEntities.Nguoidungs.OrderByDescending(n => n.Ma_Nguoidung).Select(n=>n.Ma_Nguoidung).FirstOrDefault();
                nd.Ma_Nguoidung = lastId + 1;
                // Them nguoi dung vao csdl
                nd.Quyen_Nguoidung = false;
                objWebDocTruyenEntities.Nguoidungs.Add(nd);
                objWebDocTruyenEntities.Configuration.ValidateOnSaveEnabled = false;
                objWebDocTruyenEntities.SaveChanges();
                ViewBag.ThongBao = "Đăng ký thành công";
                return RedirectToAction("Login");           
        }

        

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(Nguoidung nd)

        {
            //Check db
            WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
            var nguoidung = objWebDocTruyenEntities.Nguoidungs.SingleOrDefault(m => m.Ten_Nguoidung == nd.Ten_Nguoidung && m.Pass_Nguoidung == nd.Pass_Nguoidung);

            if (nguoidung != null)
            {
                Session["id"] = nguoidung.Ma_Nguoidung;
                Session["role"] = nguoidung.Quyen_Nguoidung;
                Session["user"] = nguoidung.Ten_Nguoidung;
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng";
                return View();
            }

        }

        public ActionResult Logout()
        {
            //Xoa sesion
            Session.Clear();
            // Xoa session authen
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
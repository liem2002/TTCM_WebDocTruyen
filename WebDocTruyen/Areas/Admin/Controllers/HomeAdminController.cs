using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Security;
using WebDocTruyen.Context;

namespace WebDocTruyen.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        WebDocTruyenEntities objWebDocTruyenEntities = new WebDocTruyenEntities();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap");
            }
            else
            {
                return View();
            }    
            
        }
        public ActionResult DanhSach(string currentFilter, string SearchString, int? page)
        {
            var lstTruyen = new List<Truyen>();
            if(SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstTruyen = objWebDocTruyenEntities.Truyens.Where(n=>n.Ten_Truyen.Contains(SearchString)).ToList();

            }
            else
            {
                lstTruyen = objWebDocTruyenEntities.Truyens.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
           
            //var lstTruyen = objWebDocTruyenEntities.Truyens.Where(n=>n.Ten_Truyen.Contains(SearchString)).ToList();
            return View(lstTruyen.ToPagedList(pageNumber, pageSize));
            
        }

        public ActionResult Details(int id)
        {
            var lstTruyen = objWebDocTruyenEntities.Truyens.Where(n => n.Ma_Truyen == id).FirstOrDefault();
            return View(lstTruyen);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Truyen objTruyen)
        {
            try
            {
                if (objTruyen.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objTruyen.ImageUpload.FileName);
                    string extension = Path.GetExtension(objTruyen.ImageUpload.FileName);
                    fileName = fileName  + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objTruyen.HinhDaiDien = fileName;
                    objTruyen.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));

                    // Tiếp tục xử lý tệp tin và lưu trữ tên file nếu cần
                }
                objWebDocTruyenEntities.Truyens.Add(objTruyen);
                objWebDocTruyenEntities.SaveChanges();
                return RedirectToAction("DanhSach");
            }
            catch (Exception)
            {
                return RedirectToAction("DanhSach");
            }
           
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var lstTruyen = objWebDocTruyenEntities.Truyens.Where(n=> n.Ma_Truyen == id).FirstOrDefault();
            return View(lstTruyen);
        }

        [HttpPost]
        public ActionResult Delete(Truyen objTruyen)
        {
            var lstTruyen = objWebDocTruyenEntities.Truyens.Where(n => n.Ma_Truyen == objTruyen.Ma_Truyen);
            objWebDocTruyenEntities.Truyens.Remove(objTruyen);
            objWebDocTruyenEntities.SaveChanges();
            return RedirectToAction("DanhSach");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var lstTruyen = objWebDocTruyenEntities.Truyens.Where(n => n.Ma_Truyen == id).FirstOrDefault();
            return View(lstTruyen);
        }

        [HttpPost]
        public ActionResult Edit(int id, Truyen objTruyen)
        {
            if (objTruyen.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objTruyen.ImageUpload.FileName);
                string extension = Path.GetExtension(objTruyen.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objTruyen.HinhDaiDien = fileName;
                objTruyen.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));

                // Tiếp tục xử lý tệp tin và lưu trữ tên file nếu cần
            }
            objWebDocTruyenEntities.Entry(objTruyen).State = System.Data.Entity.EntityState.Modified;
            objWebDocTruyenEntities.SaveChanges();
            return RedirectToAction("DanhSach");
        }
        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangNhap(string user, string password)
           
        {
            //Check db
            
            
            if (user == "Liêm Nguyễn" && password == "123") 
            {
                Session["user"] = "Liêm Nguyễn";
                
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Bạn không phải là Admin";
                return View();
            }
            
        }

        public ActionResult DangXuat()
        {
            //Xoa sesion
            Session.Remove("user");
            // Xoa session authen
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }
    }
}
using Model.Dal;
using QLKho_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EF;
namespace QLKho_Project.Controllers
{
    public class HangTonKhoController : Controller
    {
        // GET: HangTonKho
        public ActionResult Index(int page = 1,int pageSize=3)
        {
            var model = new SanPhamDao().ListAllByPaging(page, pageSize);
            
            return View(model);
        }
        public ActionResult KhaoSatTonKho()
        {
            DateTime date = DateTime.Today;
            ViewBag.Today = date.ToString("dd-MM -yyyy");
            ViewBag.model = new SanPhamDao().listAll();
            return View();
        }
        [HttpPost]
        public JsonResult AddSp(int id)

        {
         
            var sp = new JavaScriptSerializer().Serialize(new SanPhamDao().getSpByID(id));
            var jsonResult = Json(new
            {
                sanpham = sp
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult addKhaoSat(KhaoSatModel model)
        {
            var errorMessage = "";
            var status = true;
            var ngay = DateTime.Today;
            var maKhaoSat = new KhaoSatDao().Insert(new KhaoSatKho() { NguoiKhaoSat = model.TenNguoi, Ngay = ngay });
            if (maKhaoSat < 0)
            {
                errorMessage = "không thể insert Khảo Sát";
                status = false;
            }
            else
            {
                foreach(var item in model.chiTietKhaoSats)
                {
                    item.MaKhaoSat = maKhaoSat;
                    new ChiTietKhaoSatDao().Insert(item);
                }
            }
            return Json(new { status = status, errorMessage = errorMessage });
        }
        public JsonResult getSlHongByID(double spid)
        {
            var data = new SanPhamDao().getSLHongById(spid);
            return Json(new { data = data}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult doSomthing(double spid, int slXuLi)
        {
            new SanPhamDao().UpdateSLXuli(spid, slXuLi);
            return RedirectToAction("Index", "HangTonKho");
        }
    }
   
}
using Model.Dal;
using Newtonsoft.Json;
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
    public class SoGhiNoController : BaseController
    {
        // GET: SoGhiNo
       
        public ActionResult ViewSoGhiNo()
        {
        
            return View();
        }
        public void SetViewBag()
        {
            var dao = new CongTyDao();
            ViewBag.ListCongTy = dao.listAll();
        }
        public JsonResult SearchCustomer(string q)
        {
            var data = new KhachHangDao().ListName(q);
            var jsonResult= Json(new { data = data, status = true }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //[HttpGet]
        //public JsonResult getCusInfo(int id=3,int page=1,int pageSize=3)
        //{

        //    //        var data = JsonConvert.SerializeObject(new KhachHangDao().getKhachHangById(id),
        //    //Newtonsoft.Json.Formatting.None,
        //    //new JsonSerializerSettings()
        //    //{
        //    //    NullValueHandling = NullValueHandling.Ignore,
        //    //    DefaultValueHandling = DefaultValueHandling.Ignore,
        //    //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    //});
        //    var khachHang = new JavaScriptSerializer().Serialize( new KhachHangDao().getKhachHangById(id));

        //    var khachHangNoModels = new SoGhiNoDao().listKhachHangNoByPaging(id);
        //    var model = new JavaScriptSerializer().Serialize(khachHangNoModels.Skip((page - 1) * pageSize).Take(pageSize));
        //    var totalRow = khachHangNoModels.Count;

        //    var jsonResult = Json(new { result = khachHang, model = model, total = totalRow }, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        //[HttpGet]
        //public JsonResult ChangeCT(int id=1 , int page=1, int pageSize = 3)
        //{
        //    var congTy = new CongTyDao().getById(id);
        //    var CongTyNoModels = new SoGhiNoDao().listCongTYNo(id);
       
        //    var model = new JavaScriptSerializer().Serialize(CongTyNoModels.Skip((page - 1) * pageSize).Take(pageSize));
        //           var totalRow = CongTyNoModels.Count;
        //    var jsonResult= Json(new
        //    {
        //        DiaChi = congTy.DiaChi,
        //        STK = congTy.STK,
        //        SoDienThoai = congTy.SoDienThoai,
        //        SoNoCongTy= congTy.SoNo,
        //        model = model,
        //        total = totalRow


        //    }, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}


        public JsonResult getChiTietHD(int MaHd,int isCus)
        {
            if (isCus == 1)
            {
               
                var data = new JavaScriptSerializer().Serialize(new SoGhiNoDao().GetChiTietHDBanModels(MaHd));
                return Json(new {data=data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //             var data = JsonConvert.SerializeObject(new SoGhiNoDao().GetChiTietHDNhapModels(MaHd),

                //Newtonsoft.Json.Formatting.None,
                //new JsonSerializerSettings()
                //{
                //    NullValueHandling = NullValueHandling.Ignore,
                //    DefaultValueHandling = DefaultValueHandling.Ignore,
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                var data = new JavaScriptSerializer().Serialize(new SoGhiNoDao().GetChiTietHDNhapModels(MaHd));
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult LoadKhachHangNo(int page = 1, int pageSize = 3)
        {
            var data = new SoGhiNoDao().loadAllKhachHangNo();
            var model = new JavaScriptSerializer().Serialize(data.Skip((page - 1) * pageSize).Take(pageSize));
            var totalRow = data.Count;
            var jsonResult = Json(new { model = model, total = totalRow }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult LoadCongTyNo(int page = 1, int pageSize = 3)
        {
            var data = new SoGhiNoDao().loadAllCongTyNo();
            var model = new JavaScriptSerializer().Serialize(data.Skip((page - 1) * pageSize).Take(pageSize));
            var totalRow = data.Count;
            var jsonResult = Json(new { model = model, total = totalRow }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult TraNo(int khid , int isCus,float tien )
        {
            bool cus = false;
            if (isCus == 1)
            {
                cus = true;
               
            }
            var MaTraNo = new TraNoDao().addTraNo(new TraNo() { MaKH = khid, IsCus = cus, Tien = tien, Ngay = DateTime.Now });
            return RedirectToAction("ViewSoGhiNo", "SoGhiNo");
        }
        public ActionResult Index(int id, int isCus, int page = 1,int pageSize=3)
        {
            IEnumerable<KhachHangNoModel> models= new List<KhachHangNoModel>() ;
            ViewBag.isCus = isCus;
            if (isCus == 1)
            {
                ViewBag.UserInfo = new KhachHangDao().getKhachHangById(id);
                
                 models = new SoGhiNoDao().listKhachHangNoByPaging(id, page, pageSize);
         
            }
            else
            {
                ViewBag.UserInfo = new CongTyDao().getById(id);
                 models = new SoGhiNoDao().listCongTYNo(id, page, pageSize);
               
            }
            return View(models);
        }
    }
}
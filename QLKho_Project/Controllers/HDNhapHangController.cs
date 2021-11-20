using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using Model.Dal;
using Model.EF;
using Model.Models;
using Newtonsoft.Json;
using QLKho_Project.Models;


namespace QLKho_Project.Controllers
{
    public class things
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class HDNhapHangController : BaseController
    {
        // GET: HDNhapHang
        [HttpGet]
        public ActionResult Index()
        {
          
            SetViewBag();
            return View();
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new CongTyDao();
            var dao1 = new TrangTraiDao();
            DateTime date = DateTime.Today;
            ViewBag.Today = date.ToString("dd-MM -yyyy");
            ViewBag.TrangTrai = dao1.getInfo();
            ViewBag.ListCongTy = dao.listAll();
        }
        [HttpPost]
        public JsonResult ChangeCT(int id=1)
        {
            var congTy = new CongTyDao().getById(id);
            var listSP = new SanPhamDao().listByCTId(id);
            
            var model = new ChangeCongTyModel()
            {
            DiaChi = congTy.DiaChi,
                STK = congTy.STK + "",
                SoDienThoai = congTy.SoDienThoai + "",
                sanPhams = listSP

            };
            //        var data = JsonConvert.SerializeObject(model,
            //Newtonsoft.Json.Formatting.None,
            //new JsonSerializerSettings()
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    DefaultValueHandling = DefaultValueHandling.Ignore,
            //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //});
            var data = new JavaScriptSerializer().Serialize(model);
            var jsonResult = Json(new
            {
                data = data
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult ;

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
        public JsonResult addHDNhap(HdNhapModel model)
        {
            HDNhapDao hdNhapDao = new HDNhapDao();
            bool status = true;
            string errorMessage = "";
            model.ConNo.ToString();
            model.TongTien.ToString();
            model.ThanhToan.ToString();
            HDNhap hdNhap = new HDNhap()
            {
                TongTien = model.TongTien,
                ThanhToan = model.ThanhToan,
                Ngay = DateTime.Today,
                MaCongTy = model.MaCongTy
            };
            // insert vào hóa đơn nhập
            long maHDNHap = hdNhapDao.Insert(hdNhap);
            if (maHDNHap < 0)
            {
                status = false;
                errorMessage = "không thể insert HD Nhập";
            }
            else
            {
                ChiTietHDNhapDao chiTietHDNhapDao = new ChiTietHDNhapDao();
                foreach (var item in model.chiTietHDNhaps)
                {
                    item.MaHDNhap = maHDNHap;

                    if (chiTietHDNhapDao.Insert(item) < 0)
                    {
                        status = false;
                        errorMessage = "không thể insert  chi tiết HD Nhập";
                    }
                }
                // nếu mà giá trị còn nợ lớn hơn 0 thì insert vào bảng hóa đơn nợ công ty 
                if (model.ConNo != 0)
                {
                    HDNoCongTyDao hdNoDao = new HDNoCongTyDao();
                    HDNoCongTy hd = new HDNoCongTy()
                    {
                        MaHDNhap = maHDNHap,
                        SoNo = model.ConNo,
                        SoTienDaThanhToanSau = 0,
                        SoTienConLaiPhaiThanhToan = 0
                    };
                    long MaHDNo = hdNoDao.Insert(hd);
                    if (MaHDNo > 0)
                    {
                        Congty_HDNo ct_no;
                        if (model.ConNo > 0) ct_no = new Congty_HDNo() { MaCongTy = model.MaCongTy, MaHDNoCongTy = MaHDNo, TrangThai = false };
                        else ct_no = new Congty_HDNo() { MaCongTy = model.MaCongTy, MaHDNoCongTy = MaHDNo, TrangThai = true };
                        new CongTy_HDNoDao().Insert(ct_no);
                    }
                    else
                    {
                        status = false;
                        errorMessage = "không thể thêm hóa đơn nợ";
                    }
                }

            }
            return Json(new { status = status, errorMessage = errorMessage });
        }
    }
}
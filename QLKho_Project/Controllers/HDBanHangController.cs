using Model.Dal;
using Model.EF;
using Newtonsoft.Json;
using QLKho_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QLKho_Project.Controllers
{
    public class HDBanHangController : BaseController
    {
        // GET: HDBanHang
        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new SanPhamDao();
            var dao1 = new TrangTraiDao();
            DateTime date = DateTime.Today;
            ViewBag.Today = date.ToString("dd-MM -yyyy");
            ViewBag.TrangTrai = dao1.getInfo();
            ViewBag.ListSanPham = dao.listAll();
        }
        [HttpPost]
        public JsonResult AddSp(int id)

        {
            
            var sp = new JavaScriptSerializer().Serialize(new SanPhamDao().getSpByID(id));
            return Json(new
            {
                sanpham = sp
            }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult SearchCustomer(string q)
        {
            var data = new KhachHangDao().ListName(q);
            var jsonResult = Json(new { data = data, status = true }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult getCusInfo(int id)
        {
            var data = new JavaScriptSerializer().Serialize(new KhachHangDao().getKhachHangById(id));
            var jsonResult= Json(new { result = data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult addHDBan(HdBanModel model)
        {
            //HDNhapDao hdNhapDao = new HDNhapDao();
            bool status = true;
            string errorMessage = "";
            model.ConNo.ToString();
            model.TongTien.ToString();
            model.ThanhToan.ToString();
            model.chiTietHDBans.ToList();
            long MaKhachHang = model.MaKhachHang;
            if (MaKhachHang < 0)
            {
                MaKhachHang = new KhachHangDao().Insert(new KhachHang()
                {
                    Ten = model.Ten,
                    DiaChi = model.DiaChi,
                    STK = model.STK,
                    SoDienThoai = model.SoDienThoai,
                    SoNo = 0
                });
            }
            if (MaKhachHang < 0)
            {
                status = false;
                errorMessage = "không thể insert Khách hàng ";
            }
            else
            {
                long maHDban = new HDBanDao().Insert(new HDBan()
                {

                    TongTien = model.TongTien,
                    ThanhToan = model.ThanhToan,
                    Ngay = DateTime.Today,
                    MaKhachHang=MaKhachHang
                });
                if (maHDban < 0)
                {
                    status = false;
                    errorMessage = "không thể insert HDBan ";
                }
                else
                {
                    ChiTietHDBanDao chiTietHDBanDao = new ChiTietHDBanDao();
                    foreach(var item in model.chiTietHDBans)
                    {
                        item.MaHDBan = maHDban;
                        if (chiTietHDBanDao.Insert(item) < 0)
                        {
                            status = false;
                            errorMessage = "không thể insert chi tiet HDBan ";
                        }
                    }

                    // nếu mà giá trị còn nợ lớn hơn 0 thì insert vào bảng hóa đơn nợ khach hang
                    if (model.ConNo != 0)
                    {
                        long MaHDNo = new HDNoKhachHangDao().Insert(new HDNoKhachHang1() { 
                            MaHDBan=maHDban,
                            SoNo=model.ConNo,
                            SoTienDaThanhToanSau=0,
                            SoTienConLaiPhaiThanhToan=0
                        });
                        if (MaHDNo > 0)
                        {
                            KhachHang_HDNO kh_no;
                            if (model.ConNo > 0) kh_no = new KhachHang_HDNO() { MaKhachHang = MaKhachHang, MaHDNO = MaHDNo, TrangThai = false };
                            else kh_no = new KhachHang_HDNO() { MaKhachHang = MaKhachHang, MaHDNO = MaHDNo, TrangThai = true };
                            new KhachHang_HDNoDao().Insert(kh_no);
                        }
                        else
                        {
                            status = false;
                            errorMessage = "không thể thêm hóa đơn nợ";
                        }
                    }

                }
            }

            return Json(new { status = status, errorMessage = errorMessage });
        }
    }
}
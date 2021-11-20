using Model.EF;
using QLKho_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.Models;

namespace Model.Dal
{
    public class SoGhiNoDao
    {

        public QLKhoDbContext db = null;
        public SoGhiNoDao()
        {
            db = new QLKhoDbContext();
        }
       
        public IEnumerable<KhachHangNoModel> listKhachHangNoByPaging(long maKhachHang,int page,int pageSize)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<KhachHangNoModel> data = from a in db.KhachHang_HDNO
                                                 join b in db.HDNoKhachHang1 on a.MaHDNO equals b.Ma
                                                 join h in db.HDBans on b.MaHDBan equals h.Ma
                                                 where a.MaKhachHang == maKhachHang
                                                 orderby h.Ngay descending
                                                 select new KhachHangNoModel()
                                                 {
                                                     MaHDNo = a.MaHDNO,
                                                     MaHDBan=h.Ma,
                                                     TongTien = h.TongTien + "",
                                                     ThanhToan = h.ThanhToan + "",
                                                     SoDu_No =  b.SoNo+"",
                                                     daThanhToanSau = b.SoTienDaThanhToanSau+"",
                                                     ConNo =b.SoTienConLaiPhaiThanhToan +"",
                                                     Ngay = h.Ngay + ""
                                                 };
         

            return data.OrderByDescending(h => h.Ngay).ToPagedList(page,pageSize) ;
        }
        public IEnumerable<KhachHangNoModel> listCongTYNo(long maCongTy,int page,int pageSize)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<KhachHangNoModel> data = from a in db.Congty_HDNo
                                                join b in db.HDNoCongTies on a.MaHDNoCongTy equals b.Ma
                                                join h in db.HDNhaps on b.MaHDNhap equals h.Ma
                                                where a.MaCongTy == maCongTy
                                            
                                                select new KhachHangNoModel()
                                                {
                                                    MaHDNo = a.MaHDNoCongTy,
                                                    MaHDBan = h.Ma,
                                                    TongTien = h.TongTien + "",
                                                    ThanhToan = h.ThanhToan + "",
                                                    SoDu_No =  b.SoNo+"",
                                                    daThanhToanSau =  b.SoTienDaThanhToanSau +"",
                                                    ConNo =  b.SoTienConLaiPhaiThanhToan +"",
                                                    Ngay = h.Ngay + ""
                                                };

            return data.OrderByDescending(h => h.Ngay).ToPagedList(page, pageSize);
        }
        public List<ChiTietHDModel> GetChiTietHDBanModels(int MaHdBan)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<ChiTietHDModel> data = from a in db.ChiTietHDBans
                                                join b in db.SanPhams on a.MaSanPham equals b.Ma
                                                where a.MaHDBan == MaHdBan
                                                orderby a.MaSanPham
                                                select new ChiTietHDModel()
                                                {
                                                  MaSp=a.MaSanPham,
                                                  TenSp=b.Ten,
                                                  MoTa=b.MoTa,
                                                  SL= a.SL,
                                                  GiaSP=b.GiaBan+"",
                                                  Tien=a.ThanhTien

                                                };
            return data.ToList();

        }
        public List<ChiTietHDModel> GetChiTietHDNhapModels(int MaHdNhap)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<ChiTietHDModel> data = from a in db.ChiTietHDNhaps
                                              join b in db.SanPhams on a.MaSanPham equals b.Ma
                                              where a.MaHDNhap == MaHdNhap
                                              orderby a.MaSanPham
                                              select new ChiTietHDModel()
                                              {
                                                  MaSp = a.MaSanPham,
                                                  TenSp = b.Ten,
                                                  MoTa = b.MoTa,
                                                  SL = a.SL,
                                                  GiaSP = b.GiaNhap + "",
                                                  Tien = a.ThanhTien

                                              };
            return data.ToList();

        }
        public List<KhachHangModel> loadAllKhachHangNo()
        {
            IQueryable<KhachHangModel> data = from kh in db.KhachHangs
                                         join hd in db.KhachHang_HDNO
on kh.Ma equals hd.MaKhachHang 
                                         group hd by new
                                         {
                                            hd.MaKhachHang ,
                                             kh.Ma,
                                             kh.Ten,
                                             kh.DiaChi,
                                             kh.SoNo,
                                             kh.SoDienThoai,kh.STK
                                         }
                                         into grouped
                                         select new KhachHangModel()
                                         {
 Ma=grouped.Key.Ma,
 Ten=grouped.Key.Ten,
 SoDienThoai=grouped.Key.SoDienThoai,
 SoNo=grouped.Key.SoNo,
 DiaChi=grouped.Key.DiaChi,
 STK=grouped.Key.STK

                                         };


            return data.ToList();
        }
        public List<KhachHangModel> loadAllCongTyNo()
        {
            IQueryable<KhachHangModel> data = from ct in db.CongTies
                                              join hd in db.Congty_HDNo
     on ct.Ma equals hd.MaCongTy
                                              group hd by new
                                              {
                                                  hd.MaCongTy,
                                                  ct.Ma,
                                                  ct.Ten,
                                                  ct.DiaChi,
                                                  ct.SoNo,
                                                  ct.SoDienThoai,
                                                  ct.STK
                                              }
                                         into grouped
                                              select new KhachHangModel()
                                              {
                                                  Ma = grouped.Key.Ma,
                                                  Ten = grouped.Key.Ten,
                                                  SoDienThoai = grouped.Key.SoDienThoai+"",
                                                  SoNo = grouped.Key.SoNo,
                                                  DiaChi = grouped.Key.DiaChi,
                                                  STK = grouped.Key.STK+""

                                              };


            return data.ToList();
        }
    }
}
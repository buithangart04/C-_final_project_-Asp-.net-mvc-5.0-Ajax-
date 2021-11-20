using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using PagedList;
namespace Model.Dal
{
    public class TraNoDao
    {
        public QLKhoDbContext db = null;
        public TraNoDao()
        {
            db = new QLKhoDbContext();
        }
        public IEnumerable< TraNo> getTraNoByCusId(int id ,int isCus,int page ,int pageSize)
        {
            bool cus = false;
            if (isCus == 1)
            {
                cus = true;
            }
            return db.TraNoes.Where(x => x.IsCus == (bool?)cus && x.MaKH == (long?)id).OrderByDescending(x=>x.Ngay).ToPagedList(page,pageSize);
        }
        public long addTraNo(TraNo entity)
        {
            db.TraNoes.Add(entity);
            db.SaveChanges();
           
            if (entity.IsCus==true)
            {
                var khachHang = db.KhachHangs.Find(entity.MaKH);
                khachHang.SoNo = khachHang.SoNo - entity.Tien;
                db.SaveChanges();
                var traNo = entity.Tien;

                if (traNo > 0)
                {
                    var count = 1;
                  
                    do
                    {
                        // tìm xem có hóa đơn nào của khách hàng chưa trả nợ không 
                        count = (from a in db.KhachHang_HDNO where a.MaKhachHang == (long)entity.MaKH && a.TrangThai == false select a.MaKhachHang).Count();
                        if (count == 0) break;
                        // tìm trong khách hàng hóa đơn nợ cua khách hàng  có trạng thái là chưa trả nợ (tìm đứa đầu tiên )
                        var khachHang_NoModel = (from a in db.KhachHang_HDNO where a.MaKhachHang == (long)entity.MaKH && a.TrangThai == false select  new KhachHang_HDNoModel() {
  MaHDNO =a.MaHDNO,
  MaKhachHang=a.MaKhachHang,
  TrangThai=a.TrangThai

                        } ).First();
                        var khachHang_No = db.KhachHang_HDNO.Where(x => x.MaKhachHang == khachHang_NoModel.MaKhachHang && x.MaHDNO == khachHang_NoModel.MaHDNO).SingleOrDefault();
                        // tìm hóa đơn nợ của nó 
                        var HDNoKhachHang = db.HDNoKhachHang1.Find(khachHang_No.MaHDNO);
                        // nếu số tiền mà nó được trả + tiền thanh toán mà lớn  hơn hoặc bằng  số nợ thì set số tiền đã thanh toán và trạng thái là đã trả 
                        if (HDNoKhachHang.SoTienDaThanhToanSau + traNo>= HDNoKhachHang.SoNo)
                        {
                            HDNoKhachHang.SoTienDaThanhToanSau = HDNoKhachHang.SoNo;

                            traNo = (HDNoKhachHang.SoTienDaThanhToanSau + traNo) - HDNoKhachHang.SoNo;
                            // update trạng thái nó đã trả nợ 
                            khachHang_No.TrangThai = true;
                        }
                        // hoặc số tiền đã thanh toán sau + trả nợ <nhỏ hơn 
                        else
                        {
                            HDNoKhachHang.SoTienDaThanhToanSau = HDNoKhachHang.SoTienDaThanhToanSau + traNo;
                            traNo = 0;
                        }
                        db.SaveChanges();
                    }
                    while (count > 0 && traNo!=0);
                  
                }
                else // trường hợp mà mình vay khách hàng 
                {
                    var count = 1;
                  
                    do
                    {
                        // tìm xem có hóa đơn nào của khách hàng còn dư không (trạng thái bằng true và )
                        count = (from a in db.KhachHang_HDNO
                                 join b in db.HDNoKhachHang1 on a.MaHDNO equals b.Ma
                                 where a.MaKhachHang == (long)entity.MaKH && a.TrangThai == true && b.SoTienConLaiPhaiThanhToan!=0
                                 select a.MaKhachHang).Count();
                        if (count == 0) break;
                        // tìm trong khách hàng hóa đơn nợ cua khách hàng  có trạng thái là chưa trả nợ (tìm đứa đầu tiên )
                        var khachHang_NoModel = (from a in db.KhachHang_HDNO
                                            join b in db.HDNoKhachHang1 on a.MaHDNO equals b.Ma
                                            where a.MaKhachHang == (long)entity.MaKH && a.TrangThai == true && b.SoTienConLaiPhaiThanhToan != 0
                                            select new KhachHang_HDNoModel() {
                            MaHDNO = a.MaHDNO,
  MaKhachHang = a.MaKhachHang,
  TrangThai = a.TrangThai

                        }).First();
                        var khachHang_No = db.KhachHang_HDNO.Where(x => x.MaKhachHang == khachHang_NoModel.MaKhachHang && x.MaHDNO == khachHang_NoModel.MaHDNO).SingleOrDefault();
                        // tìm hóa đơn nợ của nó 
                        var HDNoKhachHang = db.HDNoKhachHang1.Find(khachHang_No.MaHDNO);
                        //nếu số tiền dư (âm ) mà nhỏ hơn trả nợ 
                        if ((HDNoKhachHang.SoNo-HDNoKhachHang.SoTienDaThanhToanSau) <= traNo  )
                        {
                            HDNoKhachHang.SoTienDaThanhToanSau = HDNoKhachHang.SoTienDaThanhToanSau +traNo;

                            traNo = 0;
                        }
                        // hoặc số tiền đã thanh toán sau + trả nợ <nhỏ hơn 
                        else
                        {
                            HDNoKhachHang.SoTienDaThanhToanSau = HDNoKhachHang.SoNo;
                            traNo = traNo-(HDNoKhachHang.SoNo - HDNoKhachHang.SoTienDaThanhToanSau);
                        }
                        db.SaveChanges();
                    }
                    while (count > 0 && traNo != 0);
                }
            }

           else
            {
                var congTy = db.CongTies.Find(entity.MaKH);
                congTy.SoNo = congTy.SoNo - entity.Tien;
                db.SaveChanges();
                var traNo = entity.Tien;

                if (traNo > 0)
                {
                    var count = 1;

                    do
                    {
                        // tìm xem có hóa đơn nào của khách hàng chưa trả nợ không 
                        count = (from a in db.Congty_HDNo where a.MaCongTy == (long)entity.MaKH && a.TrangThai == false select a.MaCongTy).Count();
                        if (count == 0) break;
                        // tìm trong khách hàng hóa đơn nợ cua khách hàng  có trạng thái là chưa trả nợ (tìm đứa đầu tiên )
                        var congTy_No_Models = (from a in db.Congty_HDNo where a.MaCongTy == (long)entity.MaKH && a.TrangThai == false select new CongTy_HDNoModel() {
                        MaCongTy=a.MaCongTy,
                        MaHDNoCongTy=a.MaHDNoCongTy,
                        TrangThai=a.TrangThai
                        }).First();
                        var congTy_No = db.Congty_HDNo.Where(x => x.MaCongTy == congTy_No_Models.MaCongTy && x.MaHDNoCongTy == congTy_No_Models.MaHDNoCongTy).SingleOrDefault();
                        // tìm hóa đơn nợ của nó 
                        var HDNoCongTy = db.HDNoCongTies.Find(congTy_No.MaHDNoCongTy);
                        // nếu số tiền mà nó được trả + tiền thanh toán mà lớn  hơn hoặc bằng  số nợ thì set số tiền đã thanh toán và trạng thái là đã trả 
                        if (HDNoCongTy.SoTienDaThanhToanSau + traNo >= HDNoCongTy.SoNo)
                        {
                            HDNoCongTy.SoTienDaThanhToanSau = HDNoCongTy.SoNo;

                            traNo = (HDNoCongTy.SoTienDaThanhToanSau + traNo) - HDNoCongTy.SoNo;
                            // update trạng thái nó đã trả nợ 
                            congTy_No.TrangThai = true;
                        }
                        // hoặc số tiền đã thanh toán sau + trả nợ <nhỏ hơn 
                        else
                        {
                            HDNoCongTy.SoTienDaThanhToanSau = HDNoCongTy.SoTienDaThanhToanSau + traNo;
                            traNo = 0;
                        }
                        db.SaveChanges();
                    }
                    while (count > 0 && traNo != 0);

                }
                else // trường hợp mà mình vay khách hàng 
                {
                    var count = 1;

                    do
                    {
                        // tìm xem có hóa đơn nào của khách hàng còn dư không (trạng thái bằng true và )
                        count = (from a in db.Congty_HDNo
                                 join b in db.HDNoCongTies on a.MaHDNoCongTy equals b.Ma
                                 where a.MaCongTy == (long)entity.MaKH && a.TrangThai == true && b.SoTienConLaiPhaiThanhToan != 0
                                 select a.MaCongTy).Count();
                        if (count == 0) break;
                        // tìm trong khách hàng hóa đơn nợ cua khách hàng  có trạng thái là chưa trả nợ (tìm đứa đầu tiên )
                        var congTy_No_Models = (from a in db.Congty_HDNo
                                            join b in db.HDNoCongTies on a.MaHDNoCongTy equals b.Ma
                                            where a.MaCongTy == (long)entity.MaKH && a.TrangThai == true && b.SoTienConLaiPhaiThanhToan != 0
                                            
                                            select new CongTy_HDNoModel()
                                            {
                                                MaCongTy = a.MaCongTy,
                                                MaHDNoCongTy = a.MaHDNoCongTy,
                                                TrangThai = a.TrangThai
                                            }).First();
                        var congTy_No = db.Congty_HDNo.Where(x => x.MaCongTy == congTy_No_Models.MaCongTy && x.MaHDNoCongTy == congTy_No_Models.MaHDNoCongTy).SingleOrDefault();
                        // tìm hóa đơn nợ của nó 
                        var HDNoCongTy = db.HDNoCongTies.Find(congTy_No.MaHDNoCongTy);
                        //nếu số tiền dư (âm ) mà nhỏ hơn trả nợ 
                        if ((HDNoCongTy.SoNo - HDNoCongTy.SoTienDaThanhToanSau) <= traNo)
                        {
                            HDNoCongTy.SoTienDaThanhToanSau = HDNoCongTy.SoTienDaThanhToanSau + traNo;

                            traNo = 0;
                        }
                        // hoặc số tiền đã thanh toán sau + trả nợ <nhỏ hơn 
                        else
                        {
                            HDNoCongTy.SoTienDaThanhToanSau = HDNoCongTy.SoNo;
                            traNo = traNo - (HDNoCongTy.SoNo - HDNoCongTy.SoTienDaThanhToanSau);
                        }
                        db.SaveChanges();
                    }
                    while (count > 0 && traNo != 0);
                }
            }
            return entity.Ma;
        }
    }
}

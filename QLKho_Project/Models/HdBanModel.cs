using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKho_Project.Models
{
    public class HdBanModel
    {
        public long MaKhachHang { get; set; }
        public string Ten { get; set; }      
        public string DiaChi { get; set; }
      
        public string STK { get; set; }
        public string SoDienThoai { get; set; }

        public float TongTien { get; set; }
        public float ThanhToan { get; set; }
        public float ConNo { get; set; }
        public IEnumerable<ChiTietHDBan> chiTietHDBans { get; set; }
    }
}
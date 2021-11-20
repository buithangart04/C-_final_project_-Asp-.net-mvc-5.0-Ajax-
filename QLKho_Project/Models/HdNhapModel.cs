using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
namespace QLKho_Project.Models
{
    public class HdNhapModel

    {
        public int MaCongTy { get; set; }
        public float TongTien { get; set; }
        public float ThanhToan { get; set; }
        public float ConNo { get; set; }
        public IEnumerable<ChiTietHDNhap> chiTietHDNhaps { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
namespace QLKho_Project.Models
{
    public class KhaoSatModel
    {
        public string Ngay { get; set; }
        public string TenNguoi { get; set; }
        public List<ChiTietKhaoSat> chiTietKhaoSats { get; set; }
    }
}
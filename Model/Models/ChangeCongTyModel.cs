using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ChangeCongTyModel
    {
        public string DiaChi { get; set; }
        public string STK { get; set; }
        public string SoDienThoai { get; set; }
        public List<SanPham> sanPhams { get; set; }
    }
}

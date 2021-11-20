using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
     public class ChiTietHDNhapDao
    {
        public QLKhoDbContext db = null;
        public ChiTietHDNhapDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(ChiTietHDNhap entity)
        {
            db.ChiTietHDNhaps.Add(entity);
            db.SaveChanges();
            return entity.MaHDNhap;
        }
    }
}

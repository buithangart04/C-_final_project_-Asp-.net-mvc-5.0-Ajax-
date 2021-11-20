using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
    public  class KhachHang_HDNoDao
    {
        public QLKhoDbContext db = null;
        public KhachHang_HDNoDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(KhachHang_HDNO entity)
        {
            db.KhachHang_HDNO.Add(entity);
            db.SaveChanges();
            return entity.MaKhachHang;
        }
    }
}

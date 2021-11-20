using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
     public class HDNoKhachHangDao
    {
        public QLKhoDbContext db = null;
        public HDNoKhachHangDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(HDNoKhachHang1 entity)
        {
            db.HDNoKhachHang1.Add(entity);
            db.SaveChanges();
            return entity.Ma;
        }
    }
}

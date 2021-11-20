using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
  public   class TrangTraiDao
    {
        public QLKhoDbContext db = null;
        public TrangTraiDao()
        {
            db = new QLKhoDbContext();
        }
     public TrangTrai  getInfo()
        {
            return db.TrangTrais.ToList().ElementAt(0);
        }
        public bool Login(string username, string password)
        {
            var result = db.TrangTrais.Count(x => x.TenDangNhap == username && x.MatKhau == password);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public TrangTrai getByUserName(string username)
        {
            return db.TrangTrais.SingleOrDefault(x => x.TenDangNhap == username);
        }
    }
}

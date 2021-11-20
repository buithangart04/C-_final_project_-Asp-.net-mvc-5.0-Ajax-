using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
    public   class KhachHangDao
    {
        public QLKhoDbContext db = null;
        public KhachHangDao()
        {
            db = new QLKhoDbContext();
        }
        public List<string> ListName(string keyword)
        {
            return db.KhachHangs.Where(x => x.Ten.Contains(keyword)).Select(x => x.Ten +"_"+ x.DiaChi+"_"+x.Ma).Take(5).ToList();
        }
        public KhachHang getKhachHangById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.KhachHangs.Find(id);
        }
        public long Insert(KhachHang entity)
        {
            db.KhachHangs.Add(entity);
            db.SaveChanges();
            return entity.Ma;
        }
    }
}

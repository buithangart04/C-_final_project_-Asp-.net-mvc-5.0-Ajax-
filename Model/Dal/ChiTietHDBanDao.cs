
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
    public class ChiTietHDBanDao
    {
        public QLKhoDbContext db = null;
        public ChiTietHDBanDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(ChiTietHDBan entity)
        {
            db.ChiTietHDBans.Add(entity);
            db.SaveChanges();
            return entity.MaHDBan;
        }
    }
}

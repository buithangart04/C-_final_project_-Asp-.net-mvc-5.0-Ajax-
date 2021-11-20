using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
    public class ChiTietKhaoSatDao
    {
        public QLKhoDbContext db = null;
        public ChiTietKhaoSatDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(ChiTietKhaoSat entity)
        {
            db.ChiTietKhaoSats.Add(entity);
            db.SaveChanges();
            return entity.MaKhaoSat;
        }
    }
}

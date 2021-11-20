using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
   public  class KhaoSatDao
    {
        public QLKhoDbContext db = null;
        public KhaoSatDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(KhaoSatKho entity)
        {
            db.KhaoSatKhoes.Add(entity);
            db.SaveChanges();
            return entity.MaKhaoSat;
        }
    }
}

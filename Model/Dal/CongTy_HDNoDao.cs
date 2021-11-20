using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
     public class CongTy_HDNoDao
    {
        public QLKhoDbContext db = null;
        public CongTy_HDNoDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(Congty_HDNo entity)
        {
            db.Congty_HDNo.Add(entity);
            db.SaveChanges();
            return entity.MaCongTy;
        }
    }
}

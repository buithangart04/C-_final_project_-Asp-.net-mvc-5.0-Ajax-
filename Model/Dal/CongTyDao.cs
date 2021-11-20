using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
     public  class CongTyDao
    {
        public QLKhoDbContext db = null;
        public CongTyDao()
        {
            db = new QLKhoDbContext();
        }
        public List<CongTy> listAll()
        {
            return db.CongTies.ToList();
        }
        public CongTy getById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CongTies.Find(id);
        }
    }
}

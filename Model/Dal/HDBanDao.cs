using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
   public  class HDBanDao
    {
        public QLKhoDbContext db = null;
        public HDBanDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(HDBan entity)
        {
            db.HDBans.Add(entity);
            db.SaveChanges();
            return entity.Ma;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.Dal
{
     public class HDNoCongTyDao
    {
        public QLKhoDbContext db = null;
        public HDNoCongTyDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(HDNoCongTy entity)
        {
            db.HDNoCongTies.Add(entity);
            db.SaveChanges();
            return entity.Ma;
        }
    }
}

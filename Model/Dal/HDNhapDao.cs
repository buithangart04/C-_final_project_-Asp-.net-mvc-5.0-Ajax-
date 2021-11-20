using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
    public class HDNhapDao 
    {
        public QLKhoDbContext db = null;
        public HDNhapDao()
        {
            db = new QLKhoDbContext();
        }
        public long Insert(HDNhap entity)
        {
            db.HDNhaps.Add(entity);
            db.SaveChanges();
            return entity.Ma;
        }
    }
}

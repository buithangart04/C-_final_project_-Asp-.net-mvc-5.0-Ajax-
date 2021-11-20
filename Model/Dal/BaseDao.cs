
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dal
{
   public abstract  class BaseDao<E>
    {
         public QLKhoDbContext db = null;
        public BaseDao()
        {
            db = new QLKhoDbContext();
        }
    }
}

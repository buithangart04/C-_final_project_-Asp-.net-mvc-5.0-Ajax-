using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dal
{
    public class SanPhamDao
    {
        public QLKhoDbContext db = null;
        public SanPhamDao()
        {
            db = new QLKhoDbContext();
        }
        public List<SanPham> listAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SanPhams.ToList();
        }
        public IEnumerable<SanPham> ListAllByPaging(int page,int pageSize)
        {
            return db.SanPhams.OrderBy(x => x.Ma).ToPagedList(page, pageSize);
        }
        public List<SanPham> listByCTId(int cid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = new List<SanPham>();
            list = db.SanPhams.Where(sp => sp.MaCongTy == cid).ToList();
            return list ;
        }
        public int? getSLHongById(double spid )
        {
            return db.SanPhams.Find(spid).SLHong;
        }
        public SanPham getSpByID(int id)

        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SanPhams.Where(sp => sp.Ma == id).SingleOrDefault();
        }
        public void UpdateSLXuli(double spid ,int slXuLi)
        {
            var sp = db.SanPhams.Find(spid);
            sp.SLTon = sp.SLTon - slXuLi;
            sp.SLHong = sp.SLHong - slXuLi;
            db.SaveChanges();
        }
    }
}

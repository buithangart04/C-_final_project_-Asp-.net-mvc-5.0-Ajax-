using Model.Dal;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKho_Project.Controllers
{
    public class LichSuGiaoDichController : Controller
    {
        // GET: LichSuGiaoDich
        public ActionResult Index(int id ,int isCus, int page = 1, int pageSize = 3)
        {
            ViewBag.isCus = isCus;
            if (isCus == 1)
            {
                ViewBag.UserInfo = new KhachHangDao().getKhachHangById(id);
            }
            else
            {
                ViewBag.UserInfo = new KhachHangDao().getKhachHangById(id);
            }
            IEnumerable<TraNo> models = new TraNoDao().getTraNoByCusId(id, isCus, page, pageSize);
                return View(models);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho_Project.Models
{
   public  class KhachHangNoModel
    {
        public string sodu_no;
        public string dathanhtoansau;
        public string conno;

        public long MaHDNo { get; set; }
        public long MaHDBan { get; set; }
        public string TongTien { get; set; }
        public string ThanhToan { get; set; }

        public string SoDu_No
        {
            get
            {
                return sodu_no;
            }

            set
            {
                this.sodu_no = getTextSoDu(value);
            }
        }
        public string daThanhToanSau
        {
            get
            {
                return dathanhtoansau;
            }

            set
            {
                this.dathanhtoansau = getTextBu(value);
            }

        }
        public string ConNo
        {
            get
            {
                return conno;
            }

            set
            {
                this.conno = getTextSoDu(value);
            }
        }
        public string Ngay { get; set; }
        public string getTextSoDu(string txt)
        {
            var num = Convert.ToDouble(txt);
            if (num < 0) return -num + "(dư)";
            else return num + "";
        }
        public string getTextBu(string txt)
        {
            var num = Convert.ToDouble(txt);
            if (num < 0) return -num + "(Bù)";
            else return num + "( Được bù)";
        }
    }
}

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDBan")]
    public partial class HDBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDBan()
        {
            ChiTietHDBans = new HashSet<ChiTietHDBan>();
            HDNoKhachHang1 = new HashSet<HDNoKhachHang1>();
        }

        [Key]
        public long Ma { get; set; }

        public double? TongTien { get; set; }

        public double? ThanhToan { get; set; }

        public DateTime? Ngay { get; set; }

        public long? MaKhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDBan> ChiTietHDBans { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HDNoKhachHang1> HDNoKhachHang1 { get; set; }
    }
}

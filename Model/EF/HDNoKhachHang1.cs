namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HDNoKhachHang1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDNoKhachHang1()
        {
            KhachHang_HDNO = new HashSet<KhachHang_HDNO>();
        }

        [Key]
        public long Ma { get; set; }

        public long MaHDBan { get; set; }

        public double? SoNo { get; set; }

        public double? SoTienDaThanhToanSau { get; set; }

        public double? SoTienConLaiPhaiThanhToan { get; set; }

        public virtual HDBan HDBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang_HDNO> KhachHang_HDNO { get; set; }
    }
}

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDNoCongTy")]
    public partial class HDNoCongTy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDNoCongTy()
        {
            Congty_HDNo = new HashSet<Congty_HDNo>();
        }

        [Key]
        public long Ma { get; set; }

        public long MaHDNhap { get; set; }

        public double? SoNo { get; set; }

        public double? SoTienDaThanhToanSau { get; set; }

        public double? SoTienConLaiPhaiThanhToan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Congty_HDNo> Congty_HDNo { get; set; }

        public virtual HDNhap HDNhap { get; set; }
    }
}

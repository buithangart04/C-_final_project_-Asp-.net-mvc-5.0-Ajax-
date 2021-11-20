namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDNhap")]
    public partial class HDNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDNhap()
        {
            ChiTietHDNhaps = new HashSet<ChiTietHDNhap>();
            HDNoCongTies = new HashSet<HDNoCongTy>();
        }

        [Key]
        public long Ma { get; set; }

        public double? TongTien { get; set; }

        public double? ThanhToan { get; set; }

        public DateTime? Ngay { get; set; }

        public long? MaCongTy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDNhap> ChiTietHDNhaps { get; set; }

        public virtual CongTy CongTy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HDNoCongTy> HDNoCongTies { get; set; }
    }
}

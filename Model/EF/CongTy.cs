namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongTy")]
    public partial class CongTy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongTy()
        {
            Congty_HDNo = new HashSet<Congty_HDNo>();
            HDNhaps = new HashSet<HDNhap>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public long Ma { get; set; }

        [StringLength(250)]
        public string Ten { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? STK { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SoDienThoai { get; set; }

        public double? SoNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Congty_HDNo> Congty_HDNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HDNhap> HDNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}

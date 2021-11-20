namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHDBans = new HashSet<ChiTietHDBan>();
            ChiTietHDNhaps = new HashSet<ChiTietHDNhap>();
            ChiTietKhaoSats = new HashSet<ChiTietKhaoSat>();
        }

        [Key]
        public long Ma { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(250)]
        public string MoTa { get; set; }

        public long? SLTon { get; set; }

        public double? GiaNhap { get; set; }

        public double? GiaBan { get; set; }

        public double? KhoiLuong { get; set; }

        public long? MaCongTy { get; set; }

        public int? SLHong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDBan> ChiTietHDBans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDNhap> ChiTietHDNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhaoSat> ChiTietKhaoSats { get; set; }

        public virtual CongTy CongTy { get; set; }
    }
}

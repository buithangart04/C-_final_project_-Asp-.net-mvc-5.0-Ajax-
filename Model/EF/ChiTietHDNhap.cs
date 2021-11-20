namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHDNhap")]
    public partial class ChiTietHDNhap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaHDNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaSanPham { get; set; }

        public int? SL { get; set; }

        public double? DonGia { get; set; }

        public double? ThanhTien { get; set; }

        public virtual HDNhap HDNhap { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}

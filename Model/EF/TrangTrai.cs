namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangTrai")]
    public partial class TrangTrai
    {
        [Key]
        [StringLength(250)]
        public string Ten { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? STK { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SoDienThoai { get; set; }

        [StringLength(100)]
        public string TenDangNhap { get; set; }

        [StringLength(100)]
        public string MatKhau { get; set; }
    }
}

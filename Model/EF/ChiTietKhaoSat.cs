namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietKhaoSat")]
    public partial class ChiTietKhaoSat
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaKhaoSat { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaSanPham { get; set; }

        public int? SoSanPhamHong { get; set; }

        public bool? DaDuocXuLI { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual KhaoSatKho KhaoSatKho { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}

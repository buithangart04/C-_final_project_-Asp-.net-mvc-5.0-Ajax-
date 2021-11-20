namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHDBan")]
    public partial class ChiTietHDBan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaHDBan { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaSanPham { get; set; }

        public int? SL { get; set; }

        public double? DonGia { get; set; }

        public double? ThanhTien { get; set; }

        public virtual HDBan HDBan { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}

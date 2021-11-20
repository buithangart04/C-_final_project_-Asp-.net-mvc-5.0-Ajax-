namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TraNo")]
    public partial class TraNo
    {
        [Key]
        public long Ma { get; set; }

        public long? MaKH { get; set; }

        public double? Tien { get; set; }

        public bool? IsCus { get; set; }

        public DateTime? Ngay { get; set; }
    }
}

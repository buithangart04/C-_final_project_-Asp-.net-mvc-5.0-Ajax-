namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhaoSatKho")]
    public partial class KhaoSatKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhaoSatKho()
        {
            ChiTietKhaoSats = new HashSet<ChiTietKhaoSat>();
        }

        [Key]
        public long MaKhaoSat { get; set; }

        public DateTime? Ngay { get; set; }

        [StringLength(250)]
        public string NguoiKhaoSat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhaoSat> ChiTietKhaoSats { get; set; }
    }
}

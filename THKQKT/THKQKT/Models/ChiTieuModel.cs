using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THKQKT.Models
{
    public class SoLieuTongHopViewModel
    {
        public Nullable<int> MaSoLieuChiTieu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; }

        [Required]
        public decimal SoTien { get; set; }
    }

    public class SoLieuDieuChinhViewModel
    {
        public Nullable<int> MaSoLieuChiTieu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; }

        [Required]
        public decimal SoDCTang { get; set; }
        public decimal SoDCGiam { get; set; }
    }

    public class SoLieuThucHienViewModel
    {
        public Nullable<int> MaSoLieuChiTieu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; }

        [Required]
        public decimal SoTien { get; set; }
    }

    public class SoLieuKienNghiViewModel
    {
        public SoLieuDieuChinhViewModel SoLieuDieuChinh { get; set; }
        public SoLieuThucHienViewModel SoLieuThucHien { get; set; }
    }
}
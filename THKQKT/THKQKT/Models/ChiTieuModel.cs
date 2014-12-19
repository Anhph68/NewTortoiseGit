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

        [Required]
        [DataType(DataType.Time)]
        public DateTime ThoiGian { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string SoTien { get; set; }
    }

    public class SoLieuDieuChinhViewModel
    {
        public Nullable<int> MaSoLieuChiTieu { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime ThoiGian { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string SoDCTang { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string SoDCGiam { get; set; }
    }

    public class SoLieuThucHienViewModel
    {
        public Nullable<int> MaSoLieuChiTieu { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime ThoiGian { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string SoTien { get; set; }
    }

    public class SoLieuKienNghiViewModel
    {
        public SoLieuDieuChinhViewModel SoLieuDieuChinh { get; set; }
        public SoLieuThucHienViewModel SoLieuThucHien { get; set; }
    }
}
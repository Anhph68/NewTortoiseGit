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
}
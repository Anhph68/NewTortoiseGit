//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THKQKT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mBieuMau
    {
        public mBieuMau()
        {
            this.mChiTieux = new HashSet<mChiTieu>();
        }
    
        public int MaBieuMau { get; set; }
        public string KyHieu { get; set; }
        public string TenBieuMau { get; set; }
        public string GhiChu { get; set; }
    
        public virtual ICollection<mChiTieu> mChiTieux { get; set; }
    }
}

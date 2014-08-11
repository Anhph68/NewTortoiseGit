﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDKT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TDKTEntities : DbContext
    {
        public TDKTEntities()
            : base("name=TDKTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CUOC_KT> CUOC_KT { get; set; }
        public virtual DbSet<TD_DVKT> TD_DVKT { get; set; }
        public virtual DbSet<TD_LHKT> TD_LHKT { get; set; }
        public virtual DbSet<TD_LVKT> TD_LVKT { get; set; }
    
        public virtual ObjectResult<getDonViDo_Result> getDonViDo(string namkt)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDonViDo_Result>("getDonViDo", namktParameter);
        }
    
        public virtual ObjectResult<string> genCode(string namkt, string donvi, string linhvuc, string loaihinh)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            var linhvucParameter = linhvuc != null ?
                new ObjectParameter("linhvuc", linhvuc) :
                new ObjectParameter("linhvuc", typeof(string));
    
            var loaihinhParameter = loaihinh != null ?
                new ObjectParameter("loaihinh", loaihinh) :
                new ObjectParameter("loaihinh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("genCode", namktParameter, donviParameter, linhvucParameter, loaihinhParameter);
        }
    
        public virtual ObjectResult<getLinhVuc_Result> getLinhVuc()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLinhVuc_Result>("getLinhVuc");
        }
    
        public virtual ObjectResult<getLoaiHinh_Result> getLoaiHinh()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLoaiHinh_Result>("getLoaiHinh");
        }
    
        public virtual ObjectResult<getRoles_Result> getRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getRoles_Result>("getRoles");
        }
    
        public virtual ObjectResult<getYears_Result> getYears()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getYears_Result>("getYears");
        }
    
        public virtual ObjectResult<string> getYear()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("getYear");
        }
    
        public virtual ObjectResult<getDonVi_Result> getDonVi(Nullable<bool> canAudit, string namkt)
        {
            var canAuditParameter = canAudit.HasValue ?
                new ObjectParameter("canAudit", canAudit) :
                new ObjectParameter("canAudit", typeof(bool));
    
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDonVi_Result>("getDonVi", canAuditParameter, namktParameter);
        }
    
        public virtual ObjectResult<getUsers_Result> getUsers(string donvi)
        {
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getUsers_Result>("getUsers", donviParameter);
        }
    
        public virtual ObjectResult<string> getMaDonvi(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("getMaDonvi", usernameParameter);
        }
    
        public virtual ObjectResult<getPhatHanh_Result> getPhatHanh(string namkt, Nullable<System.DateTime> ngaylapbc, string donvi)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            var ngaylapbcParameter = ngaylapbc.HasValue ?
                new ObjectParameter("ngaylapbc", ngaylapbc) :
                new ObjectParameter("ngaylapbc", typeof(System.DateTime));
    
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPhatHanh_Result>("getPhatHanh", namktParameter, ngaylapbcParameter, donviParameter);
        }
    
        public virtual ObjectResult<getTrienKhai_Result> getTrienKhai(string namkt, Nullable<System.DateTime> ngaylapbc, string donvi)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            var ngaylapbcParameter = ngaylapbc.HasValue ?
                new ObjectParameter("ngaylapbc", ngaylapbc) :
                new ObjectParameter("ngaylapbc", typeof(System.DateTime));
    
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getTrienKhai_Result>("getTrienKhai", namktParameter, ngaylapbcParameter, donviParameter);
        }
    
        public virtual ObjectResult<getCuocPlus_Result> getCuocPlus(string macuoc, Nullable<System.DateTime> ngaylapbc)
        {
            var macuocParameter = macuoc != null ?
                new ObjectParameter("macuoc", macuoc) :
                new ObjectParameter("macuoc", typeof(string));
    
            var ngaylapbcParameter = ngaylapbc.HasValue ?
                new ObjectParameter("ngaylapbc", ngaylapbc) :
                new ObjectParameter("ngaylapbc", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCuocPlus_Result>("getCuocPlus", macuocParameter, ngaylapbcParameter);
        }
    
        public virtual ObjectResult<getCuocStatus_Result> getCuocStatus(string namkt, Nullable<System.DateTime> ngaylapbc, string cat, string madonvi)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            var ngaylapbcParameter = ngaylapbc.HasValue ?
                new ObjectParameter("ngaylapbc", ngaylapbc) :
                new ObjectParameter("ngaylapbc", typeof(System.DateTime));
    
            var catParameter = cat != null ?
                new ObjectParameter("cat", cat) :
                new ObjectParameter("cat", typeof(string));
    
            var madonviParameter = madonvi != null ?
                new ObjectParameter("madonvi", madonvi) :
                new ObjectParameter("madonvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCuocStatus_Result>("getCuocStatus", namktParameter, ngaylapbcParameter, catParameter, madonviParameter);
        }
    }
}

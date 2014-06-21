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
    
        public virtual ObjectResult<getCuoc_Result> getCuoc(string namkt, string donvi)
        {
            var namktParameter = namkt != null ?
                new ObjectParameter("namkt", namkt) :
                new ObjectParameter("namkt", typeof(string));
    
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCuoc_Result>("getCuoc", namktParameter, donviParameter);
        }
    
        public virtual ObjectResult<getDonVi_Result> getDonVi(Nullable<bool> active, Nullable<bool> canAudit)
        {
            var activeParameter = active.HasValue ?
                new ObjectParameter("active", active) :
                new ObjectParameter("active", typeof(bool));
    
            var canAuditParameter = canAudit.HasValue ?
                new ObjectParameter("canAudit", canAudit) :
                new ObjectParameter("canAudit", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDonVi_Result>("getDonVi", activeParameter, canAuditParameter);
        }
    
        public virtual ObjectResult<getUsers_Result> getUsers(string donvi)
        {
            var donviParameter = donvi != null ?
                new ObjectParameter("donvi", donvi) :
                new ObjectParameter("donvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getUsers_Result>("getUsers", donviParameter);
        }
    
        public virtual ObjectResult<getDonViDo_Result> getDonViDo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDonViDo_Result>("getDonViDo");
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
    }
}

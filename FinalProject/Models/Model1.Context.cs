﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class FinalProject_DBEntities : DbContext
    {
        public FinalProject_DBEntities()
            : base("name=FinalProject_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<T_ErrorLogs> T_ErrorLogs { get; set; }
        public DbSet<T_OTP_Details> T_OTP_Details { get; set; }
        public DbSet<T_PasswordHistoryLog> T_PasswordHistoryLog { get; set; }
        public DbSet<T_Roles> T_Roles { get; set; }
        public DbSet<T_Users> T_Users { get; set; }
        public DbSet<T_Bills> T_Bills { get; set; }
        public DbSet<T_Complaints> T_Complaints { get; set; }
        public DbSet<T_FamilyMembers> T_FamilyMembers { get; set; }
        public DbSet<T_Events> T_Events { get; set; }
        public DbSet<T_Notices> T_Notices { get; set; }
    
        public virtual int proc_AddRole(string role)
        {
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_AddRole", roleParameter);
        }
    
        public virtual int proc_ModifyRole(Nullable<int> id, string role)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_ModifyRole", idParameter, roleParameter);
        }
    
        public virtual int proc_RemoveRole(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_RemoveRole", idParameter);
        }
    }
}
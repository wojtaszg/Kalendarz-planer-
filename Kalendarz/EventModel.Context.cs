﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kalendarz
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    /// <summary>
    /// Klasa umożliwiająca połączenie z bazą danych za pomocą entity framework
    /// </summary>
    public partial class EventsEntity : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public EventsEntity()
            : base("name=EventsEntity")
        {
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<ListOfEvents> ListOfEvents { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<ListOfTasks> ListOfTasks { get; set; }
    }
}

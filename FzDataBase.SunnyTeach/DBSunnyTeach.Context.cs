﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FzDataBase.SunnyTeach
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SunnyTeachEntities : DbContext
    {
        public SunnyTeachEntities()
            : base("name=SunnyTeachEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tb_Order> Tb_Order { get; set; }
        public virtual DbSet<Tb_UserScheme> Tb_UserScheme { get; set; }
    }
}

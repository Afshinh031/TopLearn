using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Entities.Stores;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Context
{
   public class TopLearnContext:DbContext
    {

        public TopLearnContext(DbContextOptions<TopLearnContext> options):base(options)
        {
            
        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion


        #region Store
        public DbSet<Store> Stores { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }
        #endregion  
    }
}

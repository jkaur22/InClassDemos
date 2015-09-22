using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using eRestaurantSystem.Entities;
using System.Data.Entity;
#endregion

namespace eRestaurantSystem.DAL
{
    //this class should be restricted to access by only the BLL methods.
    //this class should not be accessible outside of the componenet library.

    //this class inherits DbContext
   internal class eRestaurantContext: DbContext
    {
       public eRestaurantContext()
           : base("name=EatIn")
       { 
           //constructor is used to pass Web config string name.
       }

       //setup the DbSet Mappings
       //One DbSet for each Entity I create.
       public DbSet<SpecialEvent> SpecialEvents { get; set; }
       public DbSet<Reservation> Reservations { get; set; }
       public DbSet<Table> Tables { get; set; }

       //When overriding OnModelCreating() it is important to remember to call the base method's implementation before you exit the method.

       //the ManyToManyNavigationPropertyConfiguration.Map method lets you configure the tables and columns use for many to many relationships.

       //you specify the comun names b calling the MapLeftKey, MapRightKey,
       //and ToTable Methods.

       //the "Left" key is the one specified in the HasMany method.
       //the "right" key is the one specified in thw withMany method.

       //we have a many to many relationship between reservation and tables
       //a reservation may need many tables.
       //a table can have over time many reservations.

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {

           modelBuilder.Entity<Reservation>().HasMany(r => r.Tables).WithMany(x => x.Reservations).Map(mapping =>
               {
                   mapping.ToTable("ReservationTables");
                   mapping.MapLeftKey("TableID");
                   mapping.MapRightKey("ReservationID");
               });
           base.OnModelCreating(modelBuilder);
       }
    }
}

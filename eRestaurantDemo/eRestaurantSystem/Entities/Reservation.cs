﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
#endregion

namespace eRestaurantSystem.Entities
{
   public class Reservation
    {
       [Key]
       public int ReservationID { get; set; }
       [Required]
       [StringLength(40)]
       public string CustomerName { get; set; }
       public DateTime ReservationDate { get; set; }
       [Range(1,16,ErrorMessage="Party size is limited to 1 to 16.")]
       public int NumberInParty { get; set; }
       [StringLength(15)]
       public string ContactPhone { get; set; }
       [Required,StringLength(1,MinimumLength=1)]
       public string ReservationStatus { get; set; }
       [StringLength(1)]
       public string EventCode { get; set; }

       //Navigation property(s)
       public virtual SpecialEvent Event { get; set; }
       //the reservations table is a many to many relationship 
       //to tables table.
       //the Sql Reservations table resolves this problem, however 
       //Reservation tables holds only a compund primary key
       //We will not create a reservations table entity in our project.
       //but handle it via navigation mapping.
       //Therefore we will place a ICollection property in this entity
       //refering to the Tables table

       public virtual ICollection<Table> Tables { get; set; }
       public virtual ICollection<Bill> Bills { get; set; }


    }
}

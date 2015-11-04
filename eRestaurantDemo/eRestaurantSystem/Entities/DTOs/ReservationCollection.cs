﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eRestaurantSystem.Entities.POCOs;
namespace eRestaurantSystem.Entities.DTOs
{
    public class ReservationCollection
    {
        //data properties
        public int Hour { get; set; }
        public virtual ICollection<ReservationSummary> Reservations { get; set; }
        //read only property
        public TimeSpan SeatingTime { get { return new TimeSpan(Hour, 0, 0); } }
    }
}

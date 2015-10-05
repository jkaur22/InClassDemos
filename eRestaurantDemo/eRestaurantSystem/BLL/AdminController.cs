﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region
using eRestaurantSystem.Entities;
using eRestaurantSystem.DAL;
using System.ComponentModel;//use for ODS access
using eRestaurantSystem.Entities.DTOs;
using eRestaurantSystem.Entities.POCOs;

#endregion
namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class AdminController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SpecialEvent> SpecialEvent_List()
        {
            using (var context = new eRestaurantContext())
            { 
                //retrieve the data from the Special Events table

                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();
                
                //query syntax
                var results = from item in context.SpecialEvents
                              orderby item.Description
                              select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Reservation> GetReservationsByEventCode(string eventcode)
        {
            using (var context = new eRestaurantContext())
            {
                
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName, item.ReservationDate
                              select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public List<ReservationByDate> GetReservationsByDate(string reservationdate)
        {
            using (var context = new eRestaurantContext())
            { 
                //remember Linq does not like using DateTime casting
                int theYear = (DateTime.Parse(reservationdate)).Year;
                int theMonth = (DateTime.Parse(reservationdate)).Month;
                int theDay = (DateTime.Parse(reservationdate)).Day;

                //query status
                var results = from item in context.SpecialEvents
                              orderby item.Description
                              select new ReservationByDate() //DTO
                              {
                                  EventDescription = item.Description,
                                  Reservations = from row in item.Reservations  //collection of navigated rows of ICollection in SpecialEvent entity
                                                 where row.ReservationDate.Year == theYear
                                                 && row.ReservationDate.Month == theMonth
                                                 && row.ReservationDate.Day == theDay
                                                 select new ReservationDetail() //POCO class
                                                 { 
                                                     CustomerName = row.CustomerName,
                                                     ReservationDate = row.ReservationDate,
                                                     NumberInParty = row.NumberInParty,
                                                     ContactPhone = row.ContactPhone,
                                                     ReservationStatus = row.ReservationStatus
                                                 
                                                 }
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public List<CategoryMenuItems> CategoryMenuItems_List()
        {
            using (var context = new eRestaurantContext())
            {
              
                var results = from category in context.MenuCategories
                              orderby category.Description
                              select new CategoryMenuItems() //DTO
                              {
                                  Description = category.Description,
                                  MenuItems = from row in category.MenuItems  //collection of navigated rows of ICollection in SpecialEvent entity
                                                 
                                                 select new MenuItem() //POCO class
                                                 {
                                                     Description = row.Description,
                                                     Price = row.CurrentPrice,
                                                     Calories = row.Calories,
                                                     Comment = row.Comment
                                                 }
                              };
                return results.ToList();
            }
        }
    }
}

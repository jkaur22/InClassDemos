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
        #region QuerySamples
       
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
        public List<Waiter> Waiters_List()
        {
            using (var context = new eRestaurantContext())
            {
                //retrieve the data from the Special Events table

                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //query syntax
                var results = from item in context.Waiters
                              orderby item.LastName, item.FirstName
                              select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Waiter GetWaiterByID(int waiterid)
        {
            using (var context = new eRestaurantContext())
            {
                //retrieve the data from the Special Events table

                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //query syntax
                var results = from item in context.Waiters
                              where item.WaiterID == waiterid
                              select item;
                return results.FirstOrDefault();
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
            [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<WaiterBilling> GetWaiterBillingReport()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from abillrow in context.Bills
                    where abillrow.BillDate.Month == 5
                    orderby abillrow.BillDate , abillrow.Waiter.LastName, abillrow.Waiter.FirstName
                    select new WaiterBilling
                    {
                      BillDate = abillrow.BillDate.Year+"/"+
                                              abillrow.BillDate.Month+ "/" +
						                      abillrow.BillDate.Day,
						  
						                      Name =  abillrow.Waiter.LastName + ", " + abillrow.Waiter.FirstName,
						                      BillID = abillrow.BillID,
						                      BillTotal = abillrow.Items.Sum(bitem => bitem.Quantity * bitem.SalePrice),
						                      PartySize = abillrow.NumberInParty,
						                      Contact = abillrow.Reservation.CustomerName
                    };
                return results.ToList();
            }
        }


        
        #endregion

        #region Crud Insert, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void SpecialEvents_Add(SpecialEvent item)
        {
            //input into this method is at the instance level
            using (eRestaurantContext context = new eRestaurantContext())
            {
                  //create a pointer variable for the instance type
                //set this pointer to null
                SpecialEvent added = null;

                //set up the add request for the dbcontext

                added = context.SpecialEvents.Add(item);
                //saving the changes will cause the .Add to execute
                //commits the add to the databse
                //evaluates the annotations(validation) on your entity

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvents_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())

            {
                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvents_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            { 
                //look up the item instance on the database to determi8nje if the instance exist
                SpecialEvent existing = context.SpecialEvents.Find(item.EventCode);
                //set up the delete request command 
                context.SpecialEvents.Remove(existing);
                //commit the action to happen
                context.SaveChanges();
            }
        }

        
        #endregion


        //waiter crud

        #region Crud Insert, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiters_Add(Waiter item)
        {
            //input into this method is at the instance level
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //create a pointer variable for the instance type
                //set this pointer to null
                Waiter added = null;

                //set up the add request for the dbcontext

                added = context.Waiters.Add(item);
                //saving the changes will cause the .Add to execute
                //commits the add to the databse
                //evaluates the annotations(validation) on your entity

                context.SaveChanges();
                //added contains the data of the newly added waiter 
                //including the pKey value.
                return added.WaiterID;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiters_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                context.Entry<Waiter>(context.Waiters.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiters_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //look up the item instance on the database to determi8nje if the instance exist
                Waiter existing = context.Waiters.Find(item.WaiterID);
                //set up the delete request command 
                context.Waiters.Remove(existing);
                //commit the action to happen
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItems> GetReportCategoryMenuItems()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.Category.Description, cat.Description
                              select new MenuCategoryItems
                              {
                                  CategoryDescription = cat.Category.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }


        
        #endregion

        //CQRS stands for command query responsibility segregation.

        #region Front desk

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DateTime GetLastBillDateTime()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = context.Bills.Max(x => x.BillDate);
                return result;
            }
        }


        [DataObject]
        public class SeatingController
        {
            [DataObjectMethod(DataObjectMethodType.Select)]
            public List<ReservationCollection> ReservationsByTime(DateTime date)
            {
                using (var context = new eRestaurantContext())
                {
                    var result = (from data in context.Reservations
                                  where data.ReservationDate.Year == date.Year
                                  && data.ReservationDate.Month == date.Month
                                  && data.ReservationDate.Day == date.Day
                                      // && data.ReservationDate.Hour == timeSlot.Hours
                                  && data.ReservationStatus == Reservation.Booked
                                  select new ReservationSummary()
                                  {
                                      ID = data.ReservationID,
                                      Name = data.CustomerName,
                                      Date = data.ReservationDate,
                                      NumberInParty = data.NumberInParty,
                                      Status = data.ReservationStatus,
                                      Event = data.Event.Description,
                                      Contact = data.ContactPhone
                                  }).ToList();
                    // the second part of this method uses the results of the first Linq query.
                    //Linq to entity where only execute the query when it deems 
                    //necessary for having the results in memory.
                    //
                    //to get your query to execute and have the resulting data
                    //inside memory for further use, you can attach the .ToList()
                    //to the previous query.

                    //note: the second query is NOT using an entity
                    // it is using the results from a previous query.

                    //item group ios atemporary in memory data collection.
                    //this collection can be use in selecting your final data collection.

                    var finalResult = from item in result
                                      orderby item.NumberInParty
                                      group item by item.Date.Hour into itemGroup
                                      select new ReservationCollection()
                                      {
                                          Hour = itemGroup.Key,
                                          Reservations = itemGroup.ToList()
                                      };
                    return finalResult.OrderBy(x => x.Hour).ToList();
                }
            }
        }
        #endregion
    }// eof class
}//eof namespace

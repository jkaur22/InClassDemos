using System;
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
        public void Waiters_Add(Waiter item)
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
        #endregion

        //CQRS stands for command query responsibility segregation.
    }// eof class
}//eof namespace

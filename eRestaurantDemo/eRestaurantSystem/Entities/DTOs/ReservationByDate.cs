using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using System.Collections;//required by IEnumerable
#endregion
namespace eRestaurantSystem.Entities.DTOs
{
   public class ReservationByDate
    {
       public string EventDescription { get; set; }
       //The rest of the data will be a collection of POCO Rows
       //the actual POCO will be define in the linq query
       public IEnumerable Reservations { get; set; }

    }
}

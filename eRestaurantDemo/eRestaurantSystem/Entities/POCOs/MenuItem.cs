using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region AdditionalNameSpaces
//using System.Collections;
#endregion
namespace eRestaurantSystem.Entities.POCOs
{
  public class MenuItem
    {
      public string Description { get; set; }
      public decimal Price { get; set; }
      public int? Calories { get; set; }
      public string Comment { get; set; }
    }
}

<Query Kind="Expression">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//anonymous type
from food in Items 
where food.MenuCategory.Description.Equals("Entree") &&
      food.Active
orderby food.CurrentPrice descending 
select new
		{ 
			Description = food.Description,
			Price = food.CurrentPrice,
			Cost = food.CurrentCost,
			Profit = food.CurrentPrice - food.CurrentCost
		}
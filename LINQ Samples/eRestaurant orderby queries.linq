<Query Kind="Expression">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//order by

//default ascending 
from food in Items
orderby food.Description
select food

//default descending
from food in Items
orderby food.CurrentPrice descending
select food

//default descending and ascending
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending
select food

//default descending and ascending with a where clause
from food in Items
where food.MenuCategory.Description.Equals("Entree")
orderby food.CurrentPrice descending, food.Calories ascending
select food

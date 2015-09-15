<Query Kind="Expression">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//grouping 

from food in Items
group food by food.MenuCategory.Description

//requires recreation of a an anonymous type
from food in Items
group food by new {food.MenuCategory.Description, food.CurrentPrice}


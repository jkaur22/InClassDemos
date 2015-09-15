<Query Kind="Expression">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// where clause


//list all tables that hold more than 3 people
from row in Tables
where row.Capacity > 3
select row

//list all items that are with more than 500 calories
from row in Items
where row.Calories > 500
select row

//list all items that are with more than 500 calories and selling for more t5han 10.00
//we use m with 10.00 because it ia decimal and this is C# language
from foodies in Items
where foodies.Calories > 500 && foodies.CurrentPrice > 10.00m
select foodies

//list all items that are with more than 500 calories and are Entrees on the menu.
//hint: navigational properties of the database and LINQPad knowlwedge 
from foodies in Items
where foodies.Calories > 500 &&
     foodies.MenuCategory.Description.Equals("Entree")
select foodies
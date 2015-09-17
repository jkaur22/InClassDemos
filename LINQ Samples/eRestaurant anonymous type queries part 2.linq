<Query Kind="Expression">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from category in MenuCategories
select new
{
    Category = category.Description,
    Items = category.Items.Count()
}



from category in MenuCategories
select new
{
    Category = category.Description,
    Items = (from x in category.Items
	            select x).Count()
}

(from theBill in BillItems
where theBill.BillID == 104
select theBill.SalePrice * theBill.Quantity).Sum()

BillItems
    .Where (theBill => theBill.BillID == 104)
    .Select(theBill => theBill.SalePrice * theBill.Quantity)
    .Sum()
	
	
(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum (theBill => theBill.SalePrice * theBill.Quantity)).Min()	


//(from customer in Bills
//where customer.PaidStatus
//select customer.BillItems.Average(theBill => theBill.SalePrice * theBill.Quantity))


//(from theBill in Bills
//where theBill.PaidStatus
//select theBill.SalePrice * theBill.Quantity).Average()



(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Count()).Average()







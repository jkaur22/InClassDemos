<Query Kind="Expression">
  <Connection>
    <ID>d5f7ab4e-c1d9-4b16-8c35-f1047ae5e666</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from abillrow in Bills
where abillrow.BillDate.Month == 5
orderby abillrow.BillDate , abillrow.Waiter.LastName, abillrow.Waiter.FirstName
select new
{
  BillDate = new DateTime(abillrow.BillDate.Year,
                          abillrow.BillDate.Month,
						  abillrow.BillDate.Day),
						  
						  Name =  abillrow.Waiter.LastName + ", " + abillrow.Waiter.FirstName,
						  BillID = abillrow.BillID,
						  BillTotal = abillrow.BillItems.Sum(bitem => bitem.Quantity * bitem.SalePrice),
						  PartySize = abillrow.NumberInParty,
						  Contact = abillrow.Reservation.CustomerName
}
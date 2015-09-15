<Query Kind="Statements">
  <Connection>
    <ID>cf935270-7770-4a63-aa5e-9d33e714fa8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//step 1 connect to the desired Database
//click on Add Connection
//take defaults press next 
//change server to . (dot) select existing database from drop down list 
//optionally press test Connection
//press OK


//remember to check the connection drop down list to see which database is the active connection.


//result should show database tables in connection object area.
//expanding a table will reveal the table attributes and any relationships.

//view Waiter data
Waiters

//query syntax to also view waiter data ---------
from item in Waiters
select item

//method syntax to view Waiter data
Waiters.Select (item => item)

//alter the query syntax into a C# statement
 var results = from item in Waiters
                select item;
				
	results.Dump();
	
//once the query is create,tested, you will be able to 
//transfer the query with minor modifications into your
//
	public List<pcocObject>SomeBLLMethodName()
	{
	
	//connect to your DAL pbject:var contextvariable
	//do your Query
	
	var results = from item in contextvariable.Waiters
                select item;
				
	 return results.ToList();
	}
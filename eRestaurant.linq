<Query Kind="Statements">
  <Connection>
    <ID>f5089613-546a-4552-94e6-0ee6c5a3a72c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var mostBills = from a in Waiters
				where a.Bills.Count() ==
					Waiters.Select( bc => bc.Bills.Count()).Max()
				select new{
					  ID = a.WaiterID
					, FName = a.FirstName
					, LName = a.LastName
				};
				
mostBills.Dump();

var maxBills = (from a in Waiters
				select a.Bills.Count()).Max();
				
var bestWaiter = from a in Waiters
					where a.Bills.Count() == maxBills
					select new{
						  Name = a.FirstName + " " + a.LastName
						, tBills = a.Bills.Count()
					};
						
bestWaiter.Dump();


//Report: Create a Dataset that contains the summary bill info by waiter
var waiterBills = from a in Waiters
					orderby a.LastName, a.FirstName
					select new
					{
						  Name = a.LastName + ", " + a.FirstName
						, BillInfo = (from b in a.Bills
										where b.BillItems.Count() > 0
										select new
										{
											BillID = b.BillID
											, BillDate = b.BillDate
											, TableId = b.TableID
											, Total = b.BillItems.Sum(bi => bi.SalePrice * bi.Quantity)
										}
									 )
					};
					
waiterBills.Dump();
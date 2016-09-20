<Query Kind="Program">
  <Connection>
    <ID>f5089613-546a-4552-94e6-0ee6c5a3a72c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	//List of bill counts for all waiters.
	//This Query will create a flat data set.
	//The collumns are native data types (ex int, string, etc...)
	//One is not concerned with repeated data in a column.
	//Instead of using an anonymous datatype (new{})
	//we wish to use a defined class
	var bestWaiter = from a in Waiters
						select new WaiterBillCounts{
							name = a.FirstName + " " + a.LastName
							, tCount = a.Bills.Count()
						};
	bestWaiter.Dump();
	
	
	var paramMonth = 4;
	var paramYear = 2014;
	
	var waiterBills = from a in Waiters
					where a.LastName.Contains("a")
					orderby a.LastName, a.FirstName
					select new
					{
						  name = a.LastName + ", " + a.FirstName
						, totalBillCount = a.Bills.Count()
						, billInfo = (from b in a.Bills
										where b.BillItems.Count() > 0
											&& b.BillDate.Month == DateTime.Today.Month - paramMonth
											&& b.BillDate.Year == paramYear
										select new BillItemSummary
										{
											billID = b.BillID
											, billDate = b.BillDate
											, tableID = b.TableID
											, total = b.BillItems.Sum(bi => bi.SalePrice * bi.Quantity)
										}
									 ).ToList()
					};
					
	waiterBills.Dump();
}

// Define other methods and classes here
//An example of a POCO Class. (Plain Ordinary Common Object)
public class WaiterBillCounts
{
	//What ever reciving feild on your query in your Select
	//appears as a property in this class
	public string name{get; set;}
	public int tCount{get; set;}
}

public class BillItemSummary
{
	public int billID {get; set;}
	public DateTime billDate {get; set;}
	public int? tableID {get; set;}
	public decimal total {get; set;}
}

//An example of a DTO class(structured)
public class WaiterBills
{
	public string name {get; set;}
	public int totalBillCount {get; set;}
	public List<BillItemSummary> billInfo {get; set;}
}
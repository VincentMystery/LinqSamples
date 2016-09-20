<Query Kind="Expression">
  <Connection>
    <ID>d7510688-cd5b-4e85-a3f6-ca2ed6b5513c</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sample of entity subset
//sample of entity navigation from child to parent on where
//reminder that code is C# and thus appropriate methods can be used .Equals()
from a in Customers
where a.SupportRepIdEmployee.FirstName == ("Jane") //a.SupportRepIdEmployee.FirstName.Equals("Jane")
   && a.SupportRepIdEmployee.LastName == ("Peacock")
select new 
	{
		  Name = a.LastName + ", " + a.FirstName
		, City = a.City
		, State = a.State
		, Phone = a.Phone
		, Email = a.Email
	}
	
//Use of aggregrates in queries
//Count() count the number of instances of the collection reference
//Sum() totals a specific field/expression, thus you will likely need to use a delagate
//		to indicate the caollection instance attribute to be used.
//Average() averages a specific field/expression, thus you will likely need to use a delagate
//		to indicate the caollection instance attribute to be used.
from a in Albums
orderby a.Title
where a.Tracks.Count() > 0
select new
	{
		  Titles = a.Title
		, numTracks = a.Tracks.Count()
		, priceOfAlbum = "$" + a.Tracks.Sum(b => b.UnitPrice)
		, averageTrackLengthSec = a.Tracks.Average(c => c.Milliseconds) / 1000
		, averageTrackLengthSecNoD = a.Tracks.Average(c => c.Milliseconds / 1000)
	}

//When you need to use mulitple steps to solve a problem.
//switch your language choice to either: -Statement(s) or -Program
//the results from each query will now be saved in a variable
//the var can then be used in future querries
//Media Type With ost tracks

var maxcount = (from x in MediaTypes
				select x.Tracks.Count()).Max();
				
//To display the contents of a var in linqpad
//Use the method .Dump()

maxcount.Dump();

//use a value from a preceeding variable
var popMediaType =  from x in MediaTypes
					where x.Tracks.Count() == maxcount
					select new{
						   	  Type = x.Name
							, TCount = x.Tracks.Count()
							};
								
popMediaType.Dump();

//Can this set of statements be done as one complete query.
//The answer is possibly, in this case yes.
//In this Example maxcount could be exchanged for the query
//	that actually create the value in the first place
//	this subsituted quwey is a subquery.
var popMediaTypeSubQ =  from a in MediaTypes
					where a.Tracks.Count() == 
						( (from b in MediaTypes
						 select b.Tracks.Count()).Max() )
					select new{
						   	  Type = a.Name
							, TCount = a.Tracks.Count()
							};
								
popMediaTypeSubQ.Dump();



//using the method syntax to determine the count calue for the where expression
//this demonstrates that quweiws can be constructed using both query syntax and method syntax
var popMediaTypeSubMethod =  from a in MediaTypes
							 where a.Tracks.Count() ==
								MediaTypes.Select (mt => mt.Tracks.Count()).Max()
							 select new{
									Type = a.Name
									, TCount = a.Tracks.Count()
									};
								
popMediaTypeSubMethod.Dump();

//union demo
//(
//	from a in Albums
//	orderby a.Title
//	where a.Tracks.Count() > 0
//	select new
//		{
//			Titles = a.Title
//			, numTracks = a.Tracks.Count()
//			, priceOfAlbum = "$" + a.Tracks.Sum(b => b.UnitPrice)
//			, averageTrackLengthSec = a.Tracks.Average(c => c.Milliseconds) / 1000
//			, averageTrackLengthSecNoD = a.Tracks.Average(c => c.Milliseconds / 1000)
//		}
//)
//.Union
//(
//	from b in Albums
//	orderby b.Title
//	where b.Tracks.Count() > 0
//	select new
//		{
//			Titles = b.Title
//			, numTracks = 0
//			, priceOfAlbum = 0.00m
//			, averageTrackLengthSec = 0.00m
//			, averageTrackLengthSecNoD = 0
//		}
//)

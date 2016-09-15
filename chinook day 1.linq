<Query Kind="Expression">
  <Connection>
    <ID>d7510688-cd5b-4e85-a3f6-ca2ed6b5513c</ID>
    <Persist>true</Persist>
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
		, averageTrackLengthSecNoD = a.Tracks.Average(c => c.Milliseconds/1000)
	}

//Media TYpe With ost tracks
from a in MediaTypes
where a.Tracks.Count() > 0
select new
	{
		  MediaType = a.Name
		, Count = a.Tracks.Count().Max()
	}


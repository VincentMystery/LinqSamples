<Query Kind="Expression">
  <Connection>
    <ID>214da41c-e0aa-42da-a1df-913abc27dde7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Waiters
from x in Items
where x.CurrentPrice > 10.00m
select new {  x.ItemID
			, x.Description
			, x.CurrentPrice }
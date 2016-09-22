<Query Kind="Expression">
  <Connection>
    <ID>19145b4e-512c-432d-89c4-772332966791</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//THis is a multi column group
//grouping data placed in a local temp dataset for further processing
//.key allows you to have acess to the value(s) in your group key(s)
//if you have multiple group colums they must be in an anonymous datatype
//to create a new DTO type collection you can use .ToList() on the temp data set
//you can have a custom data collection by using a nested query

//Step A:
from food in Items
    group food by new{food.MenuCategoryID, food.CurrentPrice}
	
//Step B: DTO style dataset
from food in Items
    group food by new{food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new 
		{
			MenuCategoryID = tempdataset.Key.MenuCategoryID
			, CurrentPrice = tempdataset.Key.CurrentPrice
			, FoodItems = tempdataset.ToList()
		}
		
//Step C: DTO custom style dataset	
from food in Items
    group food by new{food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new 
		{
			MenuCategoryID = tempdataset.Key.MenuCategoryID
			, CurrentPrice = tempdataset.Key.CurrentPrice
			, FoodItems = from x in tempdataset
						  select new 
						  	  {
								  ItemId = x.ItemID
								  , FoodDescription = x.Description
								  , TimeServed = x.BillItems.Count()
							  } 
		}
<Query Kind="Expression">
  <Connection>
    <ID>f5089613-546a-4552-94e6-0ee6c5a3a72c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//MenuCategoryFoodItems
from food in Items
	group food by new{food.MenuCategory.Description} into tempdataset
	select new
	{
		MenuCategoryDescription = tempdataset.Key.Description
		, FoodItems = from a in tempdataset
					  select new
					  {
					  		ItemID = a.ItemID
							, FoodDescription = a.Description
							, CurrentPrice = a.CurrentPrice
							, TimesServed = a.BillItems.Count()
					  }
	};
	
//MenuCategory	
from food in Items
	orderby food.MenuCategory.Description
	select new
	{
		MenuCategoryDescription = food.MenuCategory.Description
		, ItemID = food.Description
		, FoodDescription = food.Description
		, CurrentPrice = food.CurrentPrice
		, TimesServed = 10
		//, TimesServed = food.BillItems.Count()
	};
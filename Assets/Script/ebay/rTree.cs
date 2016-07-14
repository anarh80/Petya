using System.Collections.Generic;
using System;

public class rTree {
	
	public GetCategoriesResponse getCategoriesResponse { get; set; }

	public class Category
	{
		public string BestOfferEnabled { get; set; }
		public string AutoPayEnabled { get; set; }
		public string CategoryID { get; set; }
		public string CategoryLevel { get; set; }
		public string CategoryName { get; set; }
		public string CategoryParentID { get; set; }
		public string LeafCategory { get; set; }
	}

	public class CategoryArray
	{
		public List<Category> Category { get; set; }
	}

	public class GetCategoriesResponse
	{
		public string Timestamp { get; set; }
		public string Ack { get; set; }
		public string Version { get; set; }
		public string Build { get; set; }
		public CategoryArray CategoryArray { get; set; }
		public string CategoryCount { get; set; }
		public string UpdateTime { get; set; }
		public string CategoryVersion { get; set; }
		public string ReservePriceAllowed { get; set; }
		public string MinimumReservePrice { get; set; }
		public string _xmlns { get; set; }
	}


}

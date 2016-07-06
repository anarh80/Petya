using System;

class FindByKeyword
{

	public Finditemsbykeywordsresponse[] findItemsByKeywordsResponse { get; set; }
    
	public class Finditemsbykeywordsresponse
    {
        public string[] ack { get; set; }
        public string[] version { get; set; }
        public DateTime[] timestamp { get; set; }
        public Searchresult[] searchResult { get; set; }
        public Paginationoutput[] paginationOutput { get; set; }
        public string[] itemSearchURL { get; set; }
    }

    public class Searchresult
    {
        public string count { get; set; }
        public Item[] item { get; set; }
    }

    public class Item
    {
        public string[] itemId { get; set; }
        public string[] title { get; set; }
        public string[] globalId { get; set; }
        public Primarycategory[] primaryCategory { get; set; }
        public Secondarycategory[] secondaryCategory { get; set; }
        public string[] galleryURL { get; set; }
        public string[] viewItemURL { get; set; }
        public string[] paymentMethod { get; set; }
        public string[] autoPay { get; set; }
        public string[] location { get; set; }
        public string[] country { get; set; }
        public Shippinginfo[] shippingInfo { get; set; }
        public Sellingstatu[] sellingStatus { get; set; }
        public Listinginfo[] listingInfo { get; set; }
        public string[] returnsAccepted { get; set; }
        public Condition[] condition { get; set; }
        public string[] isMultiVariationListing { get; set; }
        public string[] topRatedListing { get; set; }
    }

    public class Primarycategory
    {
        public string[] categoryId { get; set; }
        public string[] categoryName { get; set; }
    }

    public class Secondarycategory
    {
        public string[] categoryId { get; set; }
        public string[] categoryName { get; set; }
    }

    public class Shippinginfo
    {
        public Shippingservicecost[] shippingServiceCost { get; set; }
        public string[] shippingType { get; set; }
        public string[] shipToLocations { get; set; }
        public string[] expeditedShipping { get; set; }
        public string[] oneDayShippingAvailable { get; set; }
        public string[] handlingTime { get; set; }
    }

    public class Shippingservicecost
    {
        public string currencyId { get; set; }
        public string __value__ { get; set; }
    }

    public class Sellingstatu
    {
        public Currentprice[] currentPrice { get; set; }
        public Convertedcurrentprice[] convertedCurrentPrice { get; set; }
        public string[] bidCount { get; set; }
        public string[] sellingState { get; set; }
        public string[] timeLeft { get; set; }
    }

    public class Currentprice
    {
        public string currencyId { get; set; }
        public string __value__ { get; set; }
    }

    public class Convertedcurrentprice
    {
        public string currencyId { get; set; }
        public string __value__ { get; set; }
    }

    public class Listinginfo
    {
        public string[] bestOfferEnabled { get; set; }
        public string[] buyItNowAvailable { get; set; }
        public DateTime[] startTime { get; set; }
        public DateTime[] endTime { get; set; }
        public string[] listingType { get; set; }
        public string[] gift { get; set; }
    }

    public class Condition
    {
        public string[] conditionId { get; set; }
        public string[] conditionDisplayName { get; set; }
    }

    public class Paginationoutput
    {
        public string[] pageNumber { get; set; }
        public string[] entriesPerPage { get; set; }
        public string[] totalPages { get; set; }
        public string[] totalEntries { get; set; }
    }


}

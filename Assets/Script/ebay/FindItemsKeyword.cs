using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using Newtonsoft.Json;

public class FindItemsKeyword : MonoBehaviour {

	public GameObject item;

	public List<ItemInList> itemL;



    HTTPRequest request;

    // Use this for initialization
    public void FindItemsKeywords(Text key)
    {
        if (key.text.Length > 2) {
            StartCoroutine(SendRequest(UrlFindItems(key.text)));
        }
        else { PageManager.Instance.ChangePage(0); }

        
    }


    string UrlFindItems(string keyword)
    {
        string url = "http://svcs.ebay.com/services/search/FindingService/v1";
        url += "?OPERATION-NAME=findItemsByKeywords";
        url += "&SERVICE-VERSION=1.0.0";
        url += "&SECURITY-APPNAME=Alexandr-bf11-4dac-8d4b-413af2eec067";
        url += "&GLOBAL-ID=EBAY-US";
        url += "&RESPONSE-DATA-FORMAT=JSON";
        url += "&callback=_cb_findItemsByKeywords";
        url += "&REST-PAYLOAD";
        url += "&keywords=" + keyword;
        url += "&paginationInput.entriesPerPage=30";
        return url;
    }

    IEnumerator SendRequest(string url)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url));
        request.Send();
        yield return StartCoroutine(request);
        RespondRecived(request.Response.DataAsText);
    }

	//public FindByKeyword itemList;
    void RespondRecived(string str)
    {
		string ss = str;
		ss = ss.Remove (0, 28);
		ss = ss.Remove (ss.Length-1);

		Debug.Log(ss);

		rFindByKeyword itemList = JsonConvert.DeserializeObject<rFindByKeyword>(ss);

		int countItem = itemList.findItemsByKeywordsResponse [0].searchResult [0].item.Count();

		for (int i=0; i<countItem; i++) {
			GameObject go = Instantiate(item) as GameObject;
			go.transform.SetParent(this.transform);
			go.transform.localScale = Vector3.one;

			go.GetComponent<ItemInList>().ff = this;
			itemL.Add(go.GetComponent<ItemInList>());
			//	public void InitItem(string itemNum, string imageUrl, string titleStr, string priceStr){
			go.GetComponent<ItemInList>().InitItem(itemList.findItemsByKeywordsResponse [0].searchResult [0].item[i].itemId[0],
			                                       itemList.findItemsByKeywordsResponse [0].searchResult [0].item[i].galleryURL[0],
			                                       itemList.findItemsByKeywordsResponse [0].searchResult [0].item[i].title[0],
			                                       "1155 hhh");

		}

    }


}

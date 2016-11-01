using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Linq;

using BestHTTP;
using Newtonsoft.Json;

public class prCategory : prBase {

	public Text name;
	public Transform itemParrent;
	rTree.Category category = null;
	List<rTree.Category> subcategory = new List<rTree.Category> ();
	List<rFindAdvanced.Item> itemCat = new List<rFindAdvanced.Item> ();
	int responceCount = 1;


	void Start(){
		category = Tree.Instance.GetCategory(unitID);

		if (category == null) {
			subcategory.AddRange (Tree.Instance.GetCategoryList(""));
			CreateSubcategory ();
			itemParrent.gameObject.SetActive (false);
		} else {
			name.text = category.CategoryName;
			gameObject.SetActive (true);
			transform.LookAt (transform.parent, transform.parent.up);
			transform.localRotation *= Quaternion.Euler (-90,0,0);
			subcategory.AddRange (Tree.Instance.GetCategoryList(category.CategoryID));
			itemParrent.gameObject.SetActive (true);
			LoadItems();
		}
	}

	void CreateSubcategory(){
		for (int i = 0; i < subcategory.Count; i++) {
			OM.Inst.Create ("category", subcategory [i].CategoryID, transform, Geometry.Inst.GetPosition(i,subcategory.Count), false);
		}
	}

	public void LoadItems(){
		FindItems (this, " ", responceCount);
		responceCount++;
	}


	public override void PressThis ()
	{
		base.PressThis ();
		List<prCategory> cat = GetComponentsInChildren<prCategory> ().ToList ();
		if (cat.Count < 2) {
			CreateSubcategory ();
		} else {
			for (int i = 0; i < cat.Count; i++) {
				if (cat [i] != this) {
					RayControl.Inst.target = cat [i].transform;
					RayControl.Inst.state = CameraState.MoveToTarget;
					return;
				}
			}
		}
	}


	public void FindItems(prCategory cat, string keyword, int responceNumber){
		HTTPRequest request = new HTTPRequest(new Uri("http://svcs.ebay.com/services/search/FindingService/v1?"),
			HTTPMethods.Post, OnFindItemsByKeywordFinished);

		//request.AddHeader ("X-EBAY-SOA-SERVICE-NAME", "FindingService");
		request.AddHeader ("X-EBAY-SOA-OPERATION-NAME", "findItemsAdvanced");
		request.AddHeader ("X-EBAY-SOA-SERVICE-VERSION", "1.0.0");
		request.AddHeader ("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
		request.AddHeader ("X-EBAY-SOA-SECURITY-APPNAME", EbayAPI.Inst.AppID);
		request.AddHeader ("X-EBAY-SOA-REQUEST-DATA-FORMAT", "XML");
		request.AddHeader ("X-EBAY-SOA-RESPONSE-DATA-FORMAT", "JSON");

		string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
			+ "<findItemsAdvancedRequest xmlns=\"http://www.ebay.com/marketplace/search/v1/services\"><keywords>"
			+ keyword 
			+ "</keywords>"
			+ "<categoryId>"
			+ cat.category.CategoryID
			+ "</categoryId>"
			+ "<paginationInput>"
			+ "<entriesPerPage>"
			+ EbayAPI.Inst.countPerResponce.ToString() // per page
			+ "</entriesPerPage>"
			+ "<pageNumber>"
			+ responceNumber.ToString() // page number
			+ "</pageNumber>"
			+ "</paginationInput>"
			+ "</findItemsAdvancedRequest>";
		byte[] xmlByte = Encoding.UTF8.GetBytes(xmlString);

		request.RawData = xmlByte;

		request.Send();

	}

	void OnFindItemsByKeywordFinished(HTTPRequest request, HTTPResponse response)
	{

		/*Debug.Log("Save to file - "+ Application.persistentDataPath + "/fkeyword" + category.CategoryID + ".txt");

		#if UNITY_IOS || UNITY_ANDROID
		File.WriteAllBytes(Application.persistentDataPath + "/fkeyword"+category.CategoryID+".txt", response.Data);
		#else
		File.WriteAllBytes(Application.dataPath + "/fkeyword"+category.CategoryID+".txt", response.Data);
		#endif
		/**/


		rFindAdvanced itemList = JsonConvert.DeserializeObject<rFindAdvanced>(response.DataAsText);

		foreach (rFindAdvanced.Item item in itemList.findItemsAdvancedResponse[0].searchResult[0].item.ToList()) {
			if (NewItem (item)) {
				itemCat.Add (item);
				OM.Inst.Create ("item", JsonConvert.SerializeObject(item) , itemParrent, Vector3.zero, false);
			}
		}

	}

	bool NewItem(rFindAdvanced.Item newItem){
		bool result = true;
		foreach (rFindAdvanced.Item item in itemCat) {
			if (newItem.itemId == item.itemId) {
				return false;
			}
		}
		return result;
	}

}

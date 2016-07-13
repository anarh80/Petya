using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Linq;


using BestHTTP;
using Newtonsoft.Json;


public class EbayAPI : MonoBehaviour {

	[SerializeField] string DevID = "77c57a28-8259-492b-8f1c-13056824569b";
	[SerializeField] string AppID = "Alexandr-bf11-4dac-8d4b-413af2eec067";
	[SerializeField] string CertID = "f72f2f6e-4693-44f6-bf72-25413544f077";

	// Token Expires: Fri, 29 Dec 2017 08:47:11 GMT
	[SerializeField] string Token = "AgAAAA**AQAAAA**aAAAAA**Dxd+Vw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wDmYumC5eCoQ+dj6x9nY+seQ**j/wAAA**AAMAAA**fdhYBb5awDln09VPsXNzB3K+M1+2IqyLHfhE/1Rji/daPgHJut3uf9tkw6iBjVmEkEjKbN24UhvL7h3TMq1yuthDXz+6jFOmr2Jm5UsaznNILF/uV7HcMG23jd0EFO8UVCuU5DpDcvpSUkHW6zZKO2hHuKSIB+pzExNixYkfL6v4s8DDgzfCBDoKgAkfr3pZw19YwiWW7AozM32dBSbrHeMzgXlfEU+/Dh1iLRatpsYR/TzfzHT6WMx+3am8Ufrui1I+NTlkcdPhVc5iETz6kZ2wTX0HOLn3q9HOOO4wEDecsfxcR9fo6D8IFD9ACb+aV0u0/ACjcAeIpLSN77HQmlkYv9v3YJBhZQsQ6vCHaVYVwxtYgore/8vnTIYMOhar7xHzwuHZX85A79Z0F0yFmcl1SCOiq+q6j2FTDXuf6Jz2cfspogfztzzHjzIrHAUJGfI+hGsAbqKEE+GpweJj5dw1rUP+94hmmz1Zj6u+bFE8M3YESMh02I5MUFk2N3rIf9oVc4ElNkQSnRC46TsyNNpLkEp89swi1Kjydl1TMBcrSJkucXTt1uZK2O2w9s+qyDTBp7WDm0Zrayz9q0kG08mM0qHOloDZ/5/vwbICYKr6DNcqdh/gV27ztug9BRUmvygq1iiqCb/8bOaznQX6fRrMeDZzp6rwCdwG7VbuT6nvIBGbXl0RiEes3NSUR3G6/6Hf/9d4BEEvyaKKKujBa/XAH8tEVp4QTUJ6a1pQ3v6sZ03gStjT5cMtMdNGwxAB";





	public Text resultText;



	public void FindItemsByKeyword( string keyword ){
		HTTPRequest request = new HTTPRequest(new Uri("http://svcs.ebay.com/services/search/FindingService/v1?"),
			HTTPMethods.Post, OnFindItemsByKeywordFinished);

		request.AddHeader ("X-EBAY-SOA-SERVICE-NAME", "FindingService");
		request.AddHeader ("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
		request.AddHeader ("X-EBAY-SOA-SERVICE-VERSION", "1.13.0");
		request.AddHeader ("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
		request.AddHeader ("X-EBAY-SOA-SECURITY-APPNAME", AppID);
		request.AddHeader ("X-EBAY-SOA-REQUEST-DATA-FORMAT", "XML");
		request.AddHeader ("X-EBAY-SOA-RESPONSE-DATA-FORMAT", "JSON");

		string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
			+ "<findItemsByKeywordsRequest xmlns=\"http://www.ebay.com/marketplace/search/v1/services\"><keywords>"
			+ keyword 
			+ "</keywords>"
			+ "<paginationInput>"
			+ "<entriesPerPage>"
			+ "5" // per page
			+ "</entriesPerPage>"
			+ "<pageNumber>"
			+ "1" // page number
			+ "</pageNumber>"
			+ "</paginationInput>"
			+ "</findItemsByKeywordsRequest>";
		byte[] xmlByte = Encoding.UTF8.GetBytes(xmlString);

		request.RawData = xmlByte;

		request.Send();
	}



	void OnFindItemsByKeywordFinished(HTTPRequest request, HTTPResponse response)
	{
		
		Debug.Log("Save to file");

		#if UNITY_IOS || UNITY_ANDROID
		File.WriteAllBytes(Application.persistentDataPath + "/fkeyword.txt", response.Data);
		#else
		File.WriteAllBytes(Application.dataPath + "/fkeyword.txt", response.Data);
		#endif
		/**/

		//Debug.Log("Send to Text");
		Debug.Log("Request Finished! Text received: " + response.DataAsText);
		resultText.text = response.DataAsText;
		//resultString = response.DataAsText;


		Debug.Log("Serrialize to <rFindByKeyword>");
		rFindByKeyword itemList = JsonConvert.DeserializeObject<rFindByKeyword>(response.DataAsText);




	}

	//TODO: 
	public void LoadTree(){
		HTTPRequest request = new HTTPRequest(new Uri("https://api.ebay.com/ws/api.dll"),
			HTTPMethods.Post, OnGetTreeFinished);

		request.AddHeader ("X-EBAY-API-COMPATIBILITY-LEVEL", "971");
		request.AddHeader ("X-EBAY-API-DEV-NAME", DevID);
		request.AddHeader ("X-EBAY-API-APP-NAME", AppID);
		request.AddHeader ("X-EBAY-API-CERT-NAME", CertID);
		request.AddHeader ("X-EBAY-API-SITEID", "0");
		request.AddHeader ("X-EBAY-API-CALL-NAME", "GetCategories");

		//request.AddHeader ("X-EBAY-SOA-REQUEST-DATA-FORMAT", "XML"); 
		//request.AddHeader ("X-EBAY-SOA-RESPONSE-DATA-FORMAT", "JSON");

		string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
			+ "<GetCategoriesRequest xmlns=\"urn:ebay:apis:eBLBaseComponents\">"
			+ "<RequesterCredentials>"
			+ "<eBayAuthToken>"
			+ Token
			+ "</eBayAuthToken>"
			+ "</RequesterCredentials>"
			+ "<CategorySiteID>0</CategorySiteID>"
			+ "<DetailLevel>ReturnAll</DetailLevel>"
			+ "</GetCategoriesRequest>​";
		byte[] xmlByte = Encoding.UTF8.GetBytes(xmlString);

		request.RawData = xmlByte;

		request.Send();

	}
	/*
	<?xml version="1.0" encoding="utf-8"?>
		<GetCategoriesRequest xmlns="urn:ebay:apis:eBLBaseComponents">
		<RequesterCredentials>
		<eBayAuthToken>ABC...123</eBayAuthToken>
		</RequesterCredentials>
		
		<CategorySiteID>0</CategorySiteID>
		<DetailLevel>ReturnAll</DetailLevel>
		</GetCategoriesRequest> /**/


	void OnGetTreeFinished(HTTPRequest request, HTTPResponse response)
	{

		Debug.Log("Save to file");

		#if UNITY_IOS || UNITY_ANDROID
		File.WriteAllBytes(Application.persistentDataPath + "/gettree.txt", response.Data);
		#else
		File.WriteAllBytes(Application.dataPath + "/gettree.txt", response.Data);
		#endif
		/**/

		//Debug.Log("Send to Text");
		Debug.Log("Request Finished! Text received: " + response.DataAsText);
		resultText.text = response.DataAsText;
		//resultString = response.DataAsText;


	}


	public void SeeTree(){
		i = 0;
		see = true;
	}

	bool see = false;
	int i;

	void Update(){

		if (see) {
			resultText.text += 
			Tree.Instance.tree.getCategoriesResponse.CategoryArray.Category [i].CategoryID
			+ " " + Tree.Instance.tree.getCategoriesResponse.CategoryArray.Category [i].CategoryName + " ";
			i++;
		}
	}

}


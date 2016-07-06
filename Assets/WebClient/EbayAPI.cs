using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Linq;
using BestHTTP;


public class EbayAPI : MonoBehaviour {

	public Text resultText;


	public void GetResponce(){
		HTTPRequest request = new HTTPRequest(new Uri("http://svcs.ebay.com/services/search/FindingService/v1?"),
			HTTPMethods.Post, OnRequestFinished);

		request.AddHeader ("X-EBAY-SOA-SERVICE-NAME", "FindingService");
		request.AddHeader ("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
		request.AddHeader ("X-EBAY-SOA-SERVICE-VERSION", "1.13.0");
		request.AddHeader ("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
		request.AddHeader ("X-EBAY-SOA-SECURITY-APPNAME", "Alexandr-bf11-4dac-8d4b-413af2eec067");
		request.AddHeader ("X-EBAY-SOA-REQUEST-DATA-FORMAT", "XML");
		request.AddHeader ("RESPONSE-DATA-FORMAT", "JSON");

		string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><findItemsByKeywordsRequest xmlns=\"http://www.ebay.com/marketplace/search/v1/services\"><keywords> smd </keywords></findItemsByKeywordsRequest>";
		byte[] xmlByte = Encoding.UTF8.GetBytes(xmlString);

		request.RawData = xmlByte;

		request.Send();
	}



	void OnRequestFinished(HTTPRequest request, HTTPResponse response)
	{
		
		Debug.Log("Save to file");
		#if UNITY_IOS || UNITY_ANDROID
		File.WriteAllBytes(Application.persistentDataPath + "/result.txt", response.DataAsText);
		#else
		File.WriteAllBytes(Application.dataPath + "/result.txt", response.Data);
		#endif

		Debug.Log("Send to Text");
		//Debug.Log("Request Finished! Text received: " + response.DataAsText);
		resultText.text = response.DataAsText;
		//resultString = response.DataAsText;
		Debug.Log("End send to Text");

	}

}

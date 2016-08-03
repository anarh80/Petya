using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using BestHTTP;
using Newtonsoft.Json;

public class ItemInList : MonoBehaviour {

	public string itemNumber;
	public RawImage image;
	public Text title;
	public Text price;
	public FindItemsKeyword ff;

	public void InitItem(string itemNum, string imageUrl, string titleStr, string priceStr){
		itemNumber = itemNum;
		StartCoroutine(SendRequestImage(imageUrl));
		title.text = titleStr;
		price.text = priceStr;
		UpdatePosition ();
	}

	IEnumerator SendRequestImage(string url)
	{
		HTTPRequest request = new HTTPRequest(new Uri(url));
		request.Send();
		yield return StartCoroutine(request);
		RequestImageRecived(request.Response.DataAsTexture2D);
	}
	
	void RequestImageRecived(Texture2D image2D)
	{
		image.texture = image2D;
	}

	void UpdatePosition(){
		int num = ff.itemL.IndexOf (this);
		int Xcount = Screen.width / 250;
		int Xnum = num % Xcount;
		int Ynum = num / Xcount;
		transform.localPosition = new Vector3 (Xnum*250f+10f, -Ynum*400f-10f);
	}

}

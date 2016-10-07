using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using BestHTTP;
using Newtonsoft.Json;

public class prItem : prBase {

	public RawImage image;
	public Text title;
	public Text price;

	void Start(){
		//StartCoroutine(SendRequestImage());
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

}

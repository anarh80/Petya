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

	rFindAdvanced.Item item;

	void Start(){
		transform.localRotation = Quaternion.identity;
		if (unitID == "") {
			
		} else {
			item = JsonConvert.DeserializeObject<rFindAdvanced.Item> (unitID);
			if (item != null)
				StartCoroutine (SendRequestImage (item.galleryURL [0]));
		}
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

	public override void PressThis (){
		//if (unitID == "") GetComponentInParent<prCategory> ().LoadItems ();
	}

}

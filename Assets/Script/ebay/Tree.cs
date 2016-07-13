using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class Tree : MonoBehaviour {

	public TextAsset asset;
	XmlTextReader reader;

	// Use this for initialization
	void Start () {
		reader = new XmlTextReader (new StringReader(asset.text));



	}



}

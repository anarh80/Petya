using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Tree : MonoBehaviour {
	static Tree _instance;
	public static Tree Instance{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}

	public TextAsset json;

	public rTree tree;

	// Use this for initialization
	void Start () {

		tree = JsonConvert.DeserializeObject<rTree> (json.text);

	}



}

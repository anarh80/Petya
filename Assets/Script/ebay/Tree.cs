using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Tree : MonoBehaviour {
	static Tree _instance;
	public static Tree Instance{get {return _instance;}}
	void Awake(){
		if (_instance == null)_instance = this;
		rtree = JsonConvert.DeserializeObject<rTree> (json.text);
		tree.AddRange(rtree.getCategoriesResponse.CategoryArray.Category);
	}

	public TextAsset json;
	public List<rTree.Category> tree = new List<rTree.Category> ();


	rTree rtree;

	public List<rTree.Category> GetCategory(string parent = ""){
		List<rTree.Category> result = new List<rTree.Category> ();
		if (parent == "") {
			foreach (rTree.Category rc in tree) {
				if (rc.CategoryID == rc.CategoryParentID)
					result.Add (rc);
			}
		} else {
			foreach (rTree.Category rc in tree) {
				if (parent == rc.CategoryParentID)
					result.Add (rc);
			}
		}
		return result;
	}

}

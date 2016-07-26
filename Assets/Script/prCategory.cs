using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class prCategory : MonoBehaviour {

	public Text name;
	public bool root = false;
	rTree.Category category;

	public void InitCategory(rTree.Category cat){
		category = cat;
		name.text = category.CategoryName;
		gameObject.SetActive (true);
	}

}

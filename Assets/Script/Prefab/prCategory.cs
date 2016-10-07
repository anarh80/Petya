using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class prCategory : prBase {

	public Text name;
	rTree.Category category = null;
	List<rTree.Category> subcategory = new List<rTree.Category> ();
	List<rFindByKeyword.Item> item = new List<rFindByKeyword.Item> ();

	void Start(){
		category = Tree.Instance.GetCategory(unitID);

		if (category == null) {

			subcategory.AddRange (Tree.Instance.GetCategoryList(""));
			CreateSubcategory ();

		} else {
			name.text = category.CategoryName;
			gameObject.SetActive (true);
			transform.LookAt (transform.parent, Vector3.up);
			transform.localRotation *= Quaternion.Euler (-90,0,0);
			subcategory.AddRange (Tree.Instance.GetCategoryList(category.CategoryID));
			LoadItems();
		}
	}

	void CreateSubcategory(){
		for (int i = 0; i < subcategory.Count; i++) {
			OM.Inst.Create ("category", subcategory [i].CategoryID, transform, Geometry.Inst.GetPosition(i,subcategory.Count), false);
		}
	}

	public void CategoryPressed(){
		CreateSubcategory ();
	}

	public void LoadItems(){
		// add item to ITEM
	}

}

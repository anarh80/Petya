using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class prCategory : prBase {

	public Text name;
	rTree.Category category = null;
	List<rTree.Category> subcategory = new List<rTree.Category> ();

	public override void InitUnit(string cat){
		unitID = cat;
		category = Tree.Instance.GetCategory(cat);
		name.text = category.CategoryName;
		gameObject.SetActive (true);
	}

	void Start(){
		if (category == null) {

			subcategory.AddRange (Tree.Instance.GetCategoryList(""));
			CreateSubcategory ();

		} else {
			transform.LookAt (transform.parent, Vector3.up);
			transform.localRotation *= Quaternion.Euler (-90,0,0);
			subcategory.AddRange (Tree.Instance.GetCategoryList(category.CategoryID));
		}
	}

	void CreateSubcategory(){
		for (int i = 0; i < subcategory.Count; i++) {
			OM.Inst.Create (0, subcategory [i].CategoryID, transform, Geometry.Inst.GetPosition(i), false);
		}
	}

	public void CategoryPressed(){
		CreateSubcategory ();
	}
}

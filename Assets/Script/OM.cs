using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OM : MonoBehaviour {
	static OM _instance;
	public static OM Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	public List<GameObject> prefab;


	List<TurnCreateObject> TurnCreate = new List<TurnCreateObject>();

	public void Create(int prefabNum, string unitID, Transform parent, Vector3 position, bool insert = false){
		TurnCreateObject turn = new TurnCreateObject ();
		turn.prefabNum = prefabNum;
		turn.unitID = unitID;

		if (parent == null) {
			turn.parent = transform;
		} else {
			turn.parent = parent;
		}
		turn.position = position;

		if (insert) {
			TurnCreate.Insert (0, turn);
		} else {
			TurnCreate.Add (turn);
		}
	}

	TurnCreateObject currentTurn = null;
	GameObject currentGO = null;
	void Update(){

		if (TurnCreate.Count > 0) {
			
			currentTurn = TurnCreate [0];
			TurnCreate.Remove (currentTurn);

			currentGO =(GameObject)Instantiate (prefab[currentTurn.prefabNum]);
			currentGO.transform.SetParent (currentTurn.parent);
			currentGO.transform.localScale = Vector3.one;
			currentGO.transform.localPosition = currentTurn.position;
			currentGO.transform.localRotation = currentTurn.rotation;
			currentGO.GetComponent<prBase> ().InitUnit (currentTurn.unitID);
		}
	}

}

public class TurnCreateObject {
	public int prefabNum;
	public string unitID;
	public Transform parent;
	public Vector3 position;
	public Quaternion rotation;
}

public enum Prefab {
	Category,
	Item
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OM : MonoBehaviour {
	static OM _instance;
	public static OM Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	List<TurnCreateObject> TurnCreate = new List<TurnCreateObject>();

	public void Create(string prefabName, string unitID, Transform parent, Vector3 position, bool insert = false){
		TurnCreateObject turn = new TurnCreateObject ();

		turn.prefabName = prefabName;
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

			GameObject prefab = Resources.Load (currentTurn.prefabName) as GameObject;

			currentGO =(GameObject)Instantiate (prefab);
			currentGO.transform.SetParent (currentTurn.parent);
			currentGO.transform.localScale = Vector3.one;
			currentGO.transform.localPosition = currentTurn.position;
			//currentGO.transform.localRotation = currentTurn.rotation;
			//currentGO.transform.LookAt(currentTurn.parent);
			currentGO.GetComponent<prBase> ().unitID = currentTurn.unitID;
		}
	}

}

public class TurnCreateObject {
	public string prefabName;
	public string unitID;
	public Transform parent;
	public Vector3 position;
	//public Quaternion rotation;
}

public enum Prefab {
	Category,
	Item
}
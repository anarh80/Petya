using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OM : MonoBehaviour {
	static OM _instance;
	public static OM Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance


	public List<TurnCreateObject> TurnCreate = new List<TurnCreateObject>();

	public void Create(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, bool insert = false){
		TurnCreateObject turn = new TurnCreateObject ();
		turn.prefab = prefab;
		if (parent == null) {
			turn.parent = transform;
		} else {
			turn.parent = parent;
		}
		turn.position = position;
		turn.rotation = rotation;

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

			currentGO =(GameObject)Instantiate (currentTurn.prefab);
			currentGO.transform.SetParent (currentTurn.parent);
			currentGO.transform.localScale = Vector3.one;
			currentGO.transform.localPosition = currentTurn.position;
			currentGO.transform.localRotation = currentTurn.rotation;
		}
	}

}

public class TurnCreateObject {
	public GameObject prefab;
	public Transform parent;
	public Vector3 position;
	public Quaternion rotation;
}

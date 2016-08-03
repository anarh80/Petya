using UnityEngine;
using System.Collections;

public class Geometry : MonoBehaviour {
	static Geometry _instance;
	public static Geometry Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	public float angleStep = 0.1f;

	public float radius = 10;
	public float height = 10;

	public Vector3 GetPosition(int num){
		Vector3 result = new Vector3 (Mathf.Sin (num * angleStep) * radius, height, Mathf.Cos (num * angleStep) * radius);

		return result;
	}

}

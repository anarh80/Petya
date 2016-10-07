using UnityEngine;
using System.Collections;

public class Geometry : MonoBehaviour {
	static Geometry _instance;
	public static Geometry Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	public float angle = 10f;

	public float width = 10;

	public Vector3 GetPosition(int num, int total){
		Vector3 result = Vector3.zero;

		float beta = Mathf.PI / total;

		float R = width / (2 * Mathf.Cos (beta));
		float H = R / Mathf.Tan (Mathf.PI * angle / 180f);

		result.y = H;
		result.x = (Mathf.Sin (num * beta * 2) + Mathf.Sin ((num + 1) * beta * 2)) * R / 2;
		result.z = (Mathf.Cos (num * beta * 2) + Mathf.Cos ((num + 1) * beta * 2)) * R / 2;

		return result;
	}



}

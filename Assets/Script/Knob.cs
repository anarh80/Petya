using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Knob : MonoBehaviour {
	static Knob _instance;
	public static Knob Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	public float fill = 0;

	Image image;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		fill = 0;
		image.fillAmount = fill;
	}
	
	// Update is called once per frame
	void Update () {
		fill = RayControl.Inst.curTime / RayControl.Inst.pressTime;
		if (fill > 1f) {
			image.fillAmount = 1f;
		} else {
			image.fillAmount = fill;
		}

	}
}

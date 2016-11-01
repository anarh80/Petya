using UnityEngine;
using System.Collections;

public class MouseLookCam : MonoBehaviour {
	
	public float XSensitivity = 15f;
	public float YSensitivity = 15f;
	public float MinimumX = -360F;
	public float MaximumX = 360F;
	public float MinimumY = -90F;
	public float MaximumY = 90F;

	private float xvel = 0f;
	private float yvel = 0f;
	public bool smooth;
	public float smoothtime;

	private float yRotation = 0f;
	private float xRotation = 0f;

	// Use this for initialization
	void Start () {
		transform.localRotation = Quaternion.identity;
	}


	// Update is called once per frame
	void Update () {

		xRotation += Input.GetAxis("Mouse X") * XSensitivity;
		//yRotation = Mathf.Min (yRotation,MaximumY);
		//yRotation = Mathf.Max (yRotation,MinimumY);

		yRotation -= Input.GetAxis("Mouse Y") * YSensitivity;
		//xRotation = Mathf.Min (xRotation,MaximumX);
		//xRotation = Mathf.Max (xRotation,MinimumX);

		transform.localRotation = Quaternion.Euler (yRotation, xRotation, 0f);

	}
}

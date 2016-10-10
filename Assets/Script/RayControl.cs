using UnityEngine;
using System.Collections;

public class RayControl : MonoBehaviour {
	static RayControl _instance;
	public static RayControl Inst{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
	// end Instance

	Transform cam;

	public CameraState state;

	public Transform target;

	public float distance = 100f;

	public float fastSpeed = 100f;
	public float slowSpeed = 10f;
	public float rotationSpeed = 10f;
	public float pressTime = 1f;
	public float curTime = 0;
	// Use this for initialization
	void Start () {
		cam = Camera.main.transform; //
		curTime = 0;
		if (target == null) {
			state = CameraState.FreeFly;
		} else {
			state = CameraState.MoveToTarget;
		}
	}

	Transform lastTarget;
	// Update is called once per frame
	void Update () {
		switch (state) {
		case CameraState.MoveToTarget:
			if (target == null) {
				state = CameraState.FreeFly;
				return;
			}
			Vector3 targetPos = target.position + target.forward * distance;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, fastSpeed * Time.deltaTime);

			Quaternion startRot = transform.rotation;
			transform.LookAt (target);
			Quaternion targetRot = transform.rotation;

			transform.rotation = Quaternion.RotateTowards (startRot, targetRot, Time.deltaTime * rotationSpeed);

			if (Vector3.SqrMagnitude (transform.position - targetPos) < 1f) {
				target = null;
				state = CameraState.FreeFly;
			}
			break;
		case CameraState.FreeFly:
			RaycastHit hit;
			Vector3 fwd = cam.TransformDirection (Vector3.forward);

			if (Physics.Raycast (transform.position, fwd, out hit, 10000f)) {
				if (lastTarget == null) {
					lastTarget = hit.transform;
				} else {
					if (lastTarget == hit.transform) {
						lastTarget.GetComponent<prBase> ().AddSeeTime (Time.deltaTime);
						curTime = lastTarget.GetComponent<prBase> ().seeTime;
						if (curTime > pressTime) {
							curTime = 0;
							lastTarget.GetComponent<prBase> ().PressThis ();
						}
						Vector3 targetFly = hit.transform.position + hit.transform.forward * distance;
						transform.position = Vector3.MoveTowards (transform.position, targetFly, slowSpeed * Time.deltaTime);

					} else {
						lastTarget = hit.transform;
					}
				}
			} else {
				curTime = 0;
				if (lastTarget != null) {
					lastTarget.GetComponent<prBase> ().ClearSeeTime ();
					lastTarget = null;
				}
			}


			break;
		}
	}
}

public enum CameraState {
	MoveToTarget,
	FreeFly,
}
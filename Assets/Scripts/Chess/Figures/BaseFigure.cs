using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseFigure : MonoBehaviour {
	public bool side;

	public string baf;

	public bool first = true;
	public bool targetActive = false;
	GameObject activator;
	GameObject target;

	public List<Variant> variant = new List<Variant> ();
	public List<BaseFigure> kill = new List<BaseFigure> ();


	// Use this for initialization
	void Start () {
		transform.rotation = side ? Quaternion.Euler (0, 180, 0) : Quaternion.identity;
		StartInit ();
		UnActivateFigure ();
		UnActivateTarget ();
		variant.Clear ();
		kill.Clear ();
	}

	/// <summary>
	/// Starts init.
	/// </summary>
	virtual public void StartInit(){
		activator = Instantiate (TurnControl.Instance.activatorPR, transform.position, Quaternion.identity, transform) as GameObject;
		target = Instantiate (TurnControl.Instance.targetPR, transform.position, Quaternion.identity, transform) as GameObject;
		target.SetActive (false);
		activator.SetActive (false);
	}

		void OnMouseUpAsButton(){
		if (TurnControl.Instance.active && side == TurnControl.Instance.turn) {
						//Debug.Log ("Tap");
						if (TurnControl.Instance.activateFigure == null) {
								if (ShowVariants () > 0) {
										TurnControl.Instance.activateFigure = this;
										ActivateFigure ();
								}
						} else {
								if (TurnControl.Instance.activateFigure == this) {
										TurnControl.Instance.activateFigure = null;
										UnActivateFigure ();
								} else {
										TurnControl.Instance.activateFigure.UnActivateFigure ();
										TurnControl.Instance.activateFigure = null;
								}
						}
				} else {
						if (targetActive) {
								TurnControl.Instance.activateFigure.KillThis (this);
						}
				}
		}

	public void ActivateFigure (){
		activator.SetActive (true);
	}


	/// <summary>
	/// Shows the variants. create - 
	/// </summary>
	virtual public int ShowVariants (bool create = true){
		Debug.Log ("BaseFigure ShowVariants");
		return 0;
	}

	public void UnActivateFigure (){
		activator.SetActive (false);
		ClearVariants ();
	}

	public void CreateVariant (Vector3 dir, string baf = ""){
		GameObject go = Instantiate (TurnControl.Instance.variantPR,
			                transform.position + transform.TransformDirection (dir * TurnControl.Instance.distance),
			                Quaternion.identity) as GameObject;
		Variant vr = go.GetComponent<Variant> ();
		vr.baf = baf;
		vr.Init (this);
		variant.Add (vr);
	}

		public void ClearVariants (){
				foreach (Variant vr in variant) {
						Destroy (vr.gameObject);
				}
				variant.Clear ();
				foreach (BaseFigure bf in kill) {
						bf.UnActivateTarget ();
				}
				kill.Clear ();

		}

	/// <summary>
	/// Moves to variant.
	/// </summary>
	/// <param name="vr">Vr.</param>
	virtual public void MoveToVariant (Variant vr){
		first = false;
		MoveToPosition(vr.transform.position);
		ClearVariants ();
		UnActivateFigure ();
		TurnControl.Instance.TurnComplete (side);
	}

	/// <summary>
	/// Moves to position.
	/// </summary>
	/// <param name="pos">Position.</param>
	virtual public void MoveToPosition(Vector3 pos){
		//transform.position = pos;
		TurnControl.Instance.BusyTime ((transform.position - pos).magnitude / TurnControl.Instance.moveSpeed);
		StartCoroutine(MoveCorotine(pos));
	}

	virtual public IEnumerator MoveCorotine(Vector3 pos){
		
		yield return new WaitForEndOfFrame ();
		while ((transform.position - pos).sqrMagnitude > 0.1f) {
			float step = TurnControl.Instance.moveSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, pos, step);
			yield return new WaitForEndOfFrame ();
		}
		transform.position = pos;
	}

	public void ActivateTarget(){
		targetActive = true;
		target.SetActive (true);
	}

	public void UnActivateTarget(){
		targetActive = false;
		target.SetActive (false);
	}

	public void KillThis(BaseFigure bf){
		first = false;
		transform.position = bf.transform.position;
		ClearVariants ();
		UnActivateFigure ();
		bf.DestroySelf ();
		//Destroy (bf.gameObject,0);
		TurnControl.Instance.TurnComplete (side);
	}

	/// <summary>
	/// Ends the turn.
	/// </summary>
	virtual public void StartTurn(bool sd){
		ClearVariants ();
	}

	/// <summary>
	/// Destroies the self.
	/// </summary>
	virtual public void DestroySelf(){
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseFigure : MonoBehaviour {
	public bool side;

	public string baf;

	public bool first = true;
	public bool targetActive = false;
	public GameObject activator;
	public GameObject target;

	public List<Variant> variant = new List<Variant> ();
	public List<BaseFigure> kill = new List<BaseFigure> ();
	// Use this for initialization
	void Start () {
		transform.rotation = side ? Quaternion.Euler (0, 180, 0) : Quaternion.identity;
		UnActivateFigure ();
		UnActivateTarget ();
		variant.Clear ();
		kill.Clear ();
	}

		void OnMouseUpAsButton(){
				if (side == TurnControl.Instance.turn) {
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
				activator.SetActive (false);
		}


	/// <summary>
	/// Shows the variants. create - 
	/// </summary>
	virtual public int ShowVariants (bool create = true){
		return 0;
	}

		public void UnActivateFigure (){
				activator.SetActive (true);
				ClearVariants ();
		}

	public void CreateVariant (Vector3 dir, string baf = ""){
		GameObject go = Instantiate (TurnControl.Instance.variant,
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
				transform.position = vr.transform.position;
				ClearVariants ();
				UnActivateFigure ();
				TurnControl.Instance.TurnComplete (side);
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
				Destroy (bf.gameObject,0);
				TurnControl.Instance.TurnComplete (side);
		}

		/// <summary>
		/// Ends the turn.
		/// </summary>
		virtual public void StartTurn(bool sd){
				
		}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Korol : BaseFigure {

	Lodiya lLodiya, rLodiya;

	public override void StartInit (){
		base.StartInit ();
		lLodiya = null;
		rLodiya = null;
	}

	override public int ShowVariants (bool create = true){
		int count = 0;
		Ray ray;
		RaycastHit hit;

		if (first) {
			ray = new Ray (transform.position, transform.TransformDirection (Vector3.left));
			if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * TurnControl.Instance.size)) {
				Lodiya ld = hit.transform.GetComponent<Lodiya> ();
				if (ld != null && ld.first && ld.side == side) {
					if (create) {
						CreateVariant (Vector3.left * 2, "leftrock");
						lLodiya = ld;
					}
					count++;
				}
			}
			ray = new Ray (transform.position, transform.TransformDirection (Vector3.right));
			if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * TurnControl.Instance.size)) {
				Lodiya ld = hit.transform.GetComponent<Lodiya> ();
				if (ld != null && ld.first && ld.side == side) {
					if (create) {
						CreateVariant (Vector3.right * 2, "rightrock");
						rLodiya = ld;
					}
					count++;
				}
			}

		}

		List<Vector3> dir = GetDirList ();
		for (int i = 0; i < dir.Count; i++) {
			ray = new Ray (transform.position, transform.TransformDirection (dir [i]));

				if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir [i].magnitude)) {
					BaseFigure bf = hit.transform.GetComponent<BaseFigure> ();
					if (bf != null) {
						if (bf.side != side) {
							
							if (create) {
								bf.ActivateTarget ();
							}
							kill.Add (bf);
							count++;
					
						}
					}

				} else {
				if (create)CreateVariant (dir [i]);
					count++;
				}

		}
		Debug.Log (count.ToString()+" variants for "+gameObject.name);
		return count;
	}

	List<Vector3> GetDirList(){
		List<Vector3> res = new List<Vector3> ();
		res.Add (Vector3.forward);
		res.Add (Vector3.right);
		res.Add (Vector3.back);
		res.Add (Vector3.left);
		res.Add (Vector3.forward + Vector3.left);
		res.Add (Vector3.forward + Vector3.right);
		res.Add (Vector3.back + Vector3.right);
		res.Add (Vector3.back + Vector3.left);
		return res;
	}

	public override void MoveToVariant (Variant vr){
		switch (vr.baf) {
		case "leftrock":
			lLodiya.MoveToPosition (transform.position + 
				transform.TransformDirection (Vector3.left * TurnControl.Instance.distance));
			base.MoveToVariant (vr);
			break;
		case "rightrock":
			rLodiya.MoveToPosition (transform.position + 
				transform.TransformDirection (Vector3.right * TurnControl.Instance.distance));
			base.MoveToVariant (vr);
			break;
		case "":
			base.MoveToVariant (vr);
			break;
		}

	}

	override public void StartTurn (bool sd){
		base.StartTurn (sd);
		if (side == sd) {
			bool check = false;
			//bool checkMat = false;
			List<BaseFigure> allFigures = TurnControl.Instance.GetAllFigures ().ToList();
			List<BaseFigure> enemy = new List<BaseFigure> ();
			Debug.Log ("all figures - "+allFigures.Count.ToString());
			foreach (BaseFigure bf in allFigures) {
				if (bf.side != side) {
					enemy.Add (bf);
				}
			}
			Debug.Log ("enemy figures - "+enemy.Count.ToString());
			foreach (BaseFigure bf in enemy) {
				bf.ShowVariants (false);
				Debug.Log (bf.kill.Count.ToString () + " kill for " + bf.gameObject.name);
				if (bf.kill.Contains (this)) {
					check = true;
					Debug.Log ("Check from - "+bf.gameObject.name);
				}
				//bf.ClearVariants ();
			}

			if (check) {
				TurnControl.Instance.Checked ();
			}

		}
	}

	public override void DestroySelf (){
		TurnControl.Instance.GameOver (!side);
		//base.DestroySelf ();
	}

}

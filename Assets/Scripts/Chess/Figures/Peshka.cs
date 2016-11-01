using UnityEngine;
using System.Collections;

public class Peshka : BaseFigure {

	Peshka xvost = null;

	override public int ShowVariants (bool create = true){
		int count = 0;
		Ray ray;
		RaycastHit hit;
		Vector3 dir;

		if (first) {
			dir = Vector3.forward * 2;
			ray = new Ray (transform.position, transform.TransformDirection (dir));
			if (!Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir.magnitude)) {
				if (create)CreateVariant (dir, "xvost");
				count++;
			}
		}

		dir = Vector3.forward;
		ray = new Ray(transform.position, transform.TransformDirection(dir));
		if (!Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir.magnitude)) {
			if (create)CreateVariant (dir);
			count++;
		}

		ray = new Ray(transform.position, transform.TransformDirection( Vector3.forward+Vector3.right));
		if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance*1.3f)) {
				BaseFigure bf = hit.transform.GetComponent<BaseFigure> ();
				if (bf != null) {
						if (bf.side != side) {
					if (create)bf.ActivateTarget ();
								kill.Add (bf);
								count++;
						}
				}

		}

		ray = new Ray(transform.position, transform.TransformDirection( Vector3.forward+Vector3.left));
		if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance*1.3f)) {
				BaseFigure bf = hit.transform.GetComponent<BaseFigure> ();
				if (bf != null) {
						if (bf.side != side) {
					if (create)bf.ActivateTarget ();
								kill.Add (bf);
								count++;
						}
				}

		}

		dir = Vector3.left;
		ray = new Ray(transform.position, transform.TransformDirection(dir));
		if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir.magnitude)) {
			Peshka bf = hit.transform.GetComponent<Peshka> ();
			if (bf != null) {
				if (bf.side != side && bf.baf == "xvost") {
					if (create)bf.ActivateTarget ();
					if (create)bf.targetActive = false;
					xvost = bf;
					kill.Add (bf);
					if (create)CreateVariant (dir + Vector3.forward, "kill xvost");
					count++;
				}
			}
		}

		dir = Vector3.right;
		ray = new Ray(transform.position, transform.TransformDirection(dir));
		if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir.magnitude)) {
			Peshka bf = hit.transform.GetComponent<Peshka> ();
			if (bf != null) {
				if (bf.side != side && bf.baf == "xvost") {
					if (create)bf.ActivateTarget ();
					bf.targetActive = false;
					xvost = bf;
					kill.Add (bf);
					if (create)CreateVariant (dir + Vector3.forward, "kill xvost");
					count++;
				}
			}
		}


		Debug.Log (count.ToString());
		return count;
	}

	public override void MoveToVariant (Variant vr){
		switch (vr.baf){
		case "kill xvost":
			KillThis (xvost);
			base.MoveToVariant (vr);
			break;
		case "xvost":
			baf = "xvost";
			base.MoveToVariant (vr);
			break;
		case "":
			base.MoveToVariant (vr);
			break;
		}
	}

	public override void StartTurn(bool sd){
		if (sd == side) baf = "";
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Korol : BaseFigure {

	public Transform ghost;

	override public int ShowVariants (bool create = true){
		int count = 0;
		Ray ray;
		RaycastHit hit;

		List<Vector3> dir = GetDirList ();
		for (int i = 0; i < dir.Count; i++) {
			ray = new Ray (transform.position, transform.TransformDirection (dir [i]));

				if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance * dir [i].magnitude)) {
					BaseFigure bf = hit.transform.GetComponent<BaseFigure> ();
					if (bf != null) {
						if (bf.side != side) {
							
							if (create) bf.ActivateTarget ();
								kill.Add (bf);
								count++;
							
						}
					}

				} else {
				if (create)CreateVariant (dir [i]);
					count++;
				}

		}
		Debug.Log (count.ToString());
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

	override public void StartTurn (bool sd){
		if (side == sd) {
			bool check = false;
			bool checkMat = false;
			List<BaseFigure> allFigures = TurnControl.Instance.GetAllFigures ().ToList();
			List<BaseFigure> enemy = new List<BaseFigure> ();

			foreach (BaseFigure bf in allFigures) {
				if (bf.side != side) {
					enemy.Add (bf);
				}
			}

			foreach (BaseFigure bf in enemy) {
				bf.ShowVariants (false);
				if (bf.kill.Contains (this)) {
					check = true;
				}
				bf.ClearVariants ();
			}

			if (check) {
				TurnControl.Instance.Checked ();
			}

		}
	}

}

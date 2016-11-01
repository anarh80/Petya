using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Korol : BaseFigure {

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
							
						if (create)bf.ActivateTarget ();
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

}

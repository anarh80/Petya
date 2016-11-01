using UnityEngine;
using System.Collections;

public class Peshka : BaseFigure {

		//public GameObject hvost;

		override public int ShowVariants (){
				int count = 0;
				Ray ray;
				RaycastHit hit;

				if (first) {
						ray = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
						if (!Physics.Raycast (ray, out hit, TurnControl.Instance.distance * 2f)) {
								CreateVariant (Vector3.forward * 2f);
								count++;
						}
				}

				ray = new Ray(transform.position, transform.TransformDirection( Vector3.forward));
				if (!Physics.Raycast (ray, out hit, TurnControl.Instance.distance)) {
						CreateVariant (Vector3.forward);
						count++;
				}

				ray = new Ray(transform.position, transform.TransformDirection( Vector3.forward+Vector3.right));
				if (Physics.Raycast (ray, out hit, TurnControl.Instance.distance*1.3f)) {
						BaseFigure bf = hit.transform.GetComponent<BaseFigure> ();
						if (bf != null) {
								if (bf.side != side) {
										bf.ActivateTarget ();
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
										bf.ActivateTarget ();
										kill.Add (bf);
										count++;
								}
						}

				}

				Debug.Log (count.ToString());
				return count;
		}

		public override void EndTurn (bool sd){
				
		}

}

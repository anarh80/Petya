using UnityEngine;
using System.Collections;

public class prBase : MonoBehaviour {

	public string unitID;

	public float seeTime = 0;

	public void AddSeeTime(float delta){
		seeTime += delta;
	}

	public void ClearSeeTime(){
		seeTime = 0;
	}

	public virtual void PressThis(){
		
	}

}

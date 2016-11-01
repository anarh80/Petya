using UnityEngine;
using System.Collections;

public class Variant : MonoBehaviour {
	
	public BaseFigure baseFigure;
	public string baf;
		public void Init(BaseFigure bf){
				baseFigure = bf;
		}
	
		void OnMouseUpAsButton(){
				baseFigure.MoveToVariant (this);
		}

}

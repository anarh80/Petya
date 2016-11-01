using UnityEngine;
using System.Collections;

public class Variant : MonoBehaviour {
		BaseFigure baseFigure;
		public void Init(BaseFigure bf){
				baseFigure = bf;
		}
	
		void OnMouseUpAsButton(){
				baseFigure.MoveToVariant (this);
		}

}

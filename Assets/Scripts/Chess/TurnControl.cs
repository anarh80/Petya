using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnControl : MonoBehaviour {
		static TurnControl _instance;
		public static TurnControl Instance{get {return _instance;}}
		void Awake(){if (_instance == null)_instance = this;}
		// end Instance
		/// <summary>
		/// The bool turn. true = white turn, false = black turn
		/// </summary>
		public bool turn = true;
		public BaseFigure activateFigure = null;

		public Color clrWhite = Color.white;
		public Color clrBlack = Color.black;

		public GameObject variant;

	public float distance = 16f;
	public int size = 8;

		public Transform desk;

		Image image;

	// Use this for initialization
	void Start () {
				image = GetComponent<Image> ();
				TurnComplete (false);
	}
	
	public void TurnComplete(bool nt){
		turn = !nt;
		image.color = turn ? clrWhite : clrBlack;
		activateFigure = null;
		BaseFigure[] figures = desk.GetComponentsInChildren<BaseFigure> ();
		foreach (BaseFigure bf in figures) {
			bf.StartTurn (turn);
		}
	}

		public void UnActiveFigures (){
				if (activateFigure != null) {
						activateFigure.UnActivateFigure ();
						activateFigure = null;
				}
		}
}

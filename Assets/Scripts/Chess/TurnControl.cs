using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TurnControl : MonoBehaviour {
		static TurnControl _instance;
		public static TurnControl Instance{get {return _instance;}}
		void Awake(){if (_instance == null)_instance = this;}
		// end Instance

	public GameObject variantPR;
	public GameObject targetPR;
	public GameObject activatorPR;
	public GameObject ghostPR;

	/// <summary>
	/// The bool turn. true = white turn, false = black turn
	/// </summary>
	public bool turn = true;
	public bool active = false;
	public bool gameOver = false;

	public BaseFigure activateFigure = null;

	public Color clrWhite = Color.white;
	public Color clrBlack = Color.black;

	public GameObject checkMarker;
	public GameObject matMarker;

	public float distance = 16f;
	public int size = 8;
	public float moveSpeed = 20f;

	public Transform desk;

	Image image;
	float busy = 0f;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		active = true;
		gameOver = false;
		matMarker.SetActive (false);
		TurnComplete (false);
	}
	
	public void TurnComplete(bool nt){
			checkMarker.SetActive (false);
			turn = !nt;
			image.color = turn ? clrWhite : clrBlack;
			activateFigure = null;
	}

	void InitStartTurn(){
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

	public BaseFigure[] GetAllFigures(){
		return desk.GetComponentsInChildren<BaseFigure> ();
	}

	public void Checked(){
		Debug.Log ("Show Check !!!");
		checkMarker.SetActive (true);
	}

	public void GameOver(bool winer){
		gameOver = true;
		BusyTime (1f);
		active = false;
		image.color = winer ? clrWhite : clrBlack;
		matMarker.SetActive (true);
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void BusyTime(float tt){
		active = false;
		if (tt > busy)
			busy = tt;
	}

	void Update(){
		if (!gameOver){ 
			if (busy > 0) {
				busy -= Time.deltaTime;
			} else {
				if (!active) {
					active = true;
					InitStartTurn ();
				}
			}
		}
	}

}

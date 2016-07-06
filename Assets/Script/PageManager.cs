using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageManager : MonoBehaviour {
	static PageManager _instance;
	public static PageManager Instance{get {return _instance;}}
	void Awake(){if (_instance == null)_instance = this;}
    // end Instance

    public List<GameObject> page;

	// Use this for initialization
	void Start () {
        page[0].SetActive(true);
        for (int i = 1; i < page.Count; i++ ) {
            page[i].SetActive(false);
        }
	}

	public void ChangePage(int pg) {
        for (int i = 0; i < page.Count; i++)
        {
            page[i].SetActive(false);
        }
		page[pg].SetActive(true);
    }

}

public enum Page{
	Home = 0,
	Find,
}

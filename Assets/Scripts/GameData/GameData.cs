using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GameData {
	
	public float moveSpeed;


	public GameData() {
		moveSpeed = 20;
	}

	/// <summary>
	/// =====================================
	/// </summary>
	private static string _version = "v_0.2";
	private static GameData _instance;
	public static GameData Instance	{
		get	{

			if (_instance == null) {
				GameData gdata = null;
				string savedGameData = PlayerPrefs.GetString (_version, null);
				Debug.Log (savedGameData);
				if (savedGameData != null && savedGameData.Length > 0) {
					gdata = convertData (savedGameData);
					if (gdata == null) {
						PlayerPrefs.DeleteKey (_version);
						PlayerPrefs.Save ();
						gdata = new GameData ();
					}
					_instance = gdata;
				}
				if (gdata == null) {
					gdata = new GameData ();
					_instance = gdata;
				}
			}


			return _instance;

		}
	}

	static GameData convertData (string json){
		return JsonConvert.DeserializeObject<GameData>(json);
	}

	public void Save()
	{
		string jsonToSave = JsonConvert.SerializeObject(GameData.Instance);
		PlayerPrefs.SetString(_version, jsonToSave);
		PlayerPrefs.Save();
	}

	public void ResetProgress()
	{
		PlayerPrefs.DeleteKey(_version);
		PlayerPrefs.Save();        
	}

}

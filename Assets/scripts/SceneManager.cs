using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Loads a level and shows the loading screen
	public static void LoadLevel(string level) {
		GameData.nextLevel = level;
		Application.LoadLevel("loading_scene");
	}


}

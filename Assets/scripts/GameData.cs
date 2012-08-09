using UnityEngine;
using System.Collections;

// This is a singleton class that carries data that should be shared across scenes
public class GameData : MonoBehaviour {
	public static string currentLevel;
	public static string previousLevel;
	public static string nextLevel;
}

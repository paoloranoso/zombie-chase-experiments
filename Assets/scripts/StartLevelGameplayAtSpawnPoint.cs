using UnityEngine;
using System.Collections;

public class StartLevelGameplayAtSpawnPoint : MonoBehaviour {
	
	public Transform levelGameplayPrefab;
	
	private Transform _transform;	
	private GameObject _hero;
		
	void Awake(){
		_transform = transform;
		_hero = GameObject.FindGameObjectWithTag("hero");	
	}
	
	void Start () {
		//start the hero's position at the spawnPoint's position
		_hero.transform.position = _transform.position;
	}

}

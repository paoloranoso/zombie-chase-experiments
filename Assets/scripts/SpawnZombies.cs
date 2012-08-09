using UnityEngine;
using System.Collections;

public class SpawnZombies : MonoBehaviour {
	
	public Transform zombiePrefab;
	
	private GameObject _hero;
	
	void Awake() {
		_hero = GameObject.FindGameObjectWithTag("hero");
		
		InvokeRepeating("SpawnSomeZombies", 1, 8);
	}
	

	private void SpawnSomeZombies(){
		//spawn one or morezombies a few feet behind the hero
			
		int numZombies = Random.Range(1,1);
				
		for (var i = 0; i < numZombies; i++) {
			Vector3 spawnPoint = _hero.transform.position;
			Vector3 spaceBehindHero = new Vector3(i,0,-5);
			spawnPoint += _hero.transform.TransformDirection(spaceBehindHero);
			
			Instantiate(zombiePrefab, spawnPoint, _hero.transform.rotation);
		}		
	


		GameObject[] allZombies = GameObject.FindGameObjectsWithTag("zombie");

		// have all zombies catch up to player and eat him if he hasn't killed any in a while
		if (allZombies.Length >= 5){
			Debug.Log("SPEED BOOST!");

			foreach (GameObject zombie in allZombies){
				ZombieController zscript = zombie.GetComponent<ZombieController>();
				zscript.GetMassiveSpeedBoost();
			}

		}


	}
	
}

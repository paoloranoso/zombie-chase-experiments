using UnityEngine;
using System.Collections;

public class LevelCompleted : MonoBehaviour {
	
	public AudioClip celebrationSound;
	
	private GameObject _hero;
	private GameObject _inputListener;
	private GameObject _zombieSpawner;
	
	private HeroController _heroController;
	private IncreaseScore _heroScore;	
	private WinMenuHUD _winMenuHUD;
	
	void Awake(){
		_hero = GameObject.FindGameObjectWithTag("hero");
		_inputListener = GameObject.Find("InputListener");
		_zombieSpawner = GameObject.Find("ZombieSpawner");
		
		_heroController = _hero.GetComponent<HeroController>();
		
		GameObject score = GameObject.FindGameObjectWithTag("score");
		_heroScore = score.GetComponent<IncreaseScore>();
		
		GameObject winMenuHUDObj = GameObject.Find("WinMenuHUD");
		_winMenuHUD = winMenuHUDObj.GetComponent<WinMenuHUD>();
	}

	void Start () {
	
	}	
	
	public void OnTriggerEnter(Collider other){
		if ( other.gameObject == _hero ){
			//kill all zombies/remove zombie spawner, stop hero, celebrate, & show HUD/menu stuff
			Destroy(_zombieSpawner);
			var allZombies = GameObject.FindGameObjectsWithTag("zombie");
			
			foreach (GameObject zombie in allZombies){
				//Destroy(zombie);
				ZombieController currentZC = zombie.GetComponent<ZombieController>();
				currentZC.SetState(ZombieState.Dying);
			}		
			
			Destroy(_inputListener);
			
			_heroController.moveSpeed = 0.0f;
			
			
			//get 1000 points by finishing the level and stop the score counter
			_heroScore.AddPoints(1000);
			_heroScore.FinishedLevel();
			
			_winMenuHUD.ShowWinText();
			_winMenuHUD.ShowMenu();
			
			
			//TODO: standing/celebration animation...
			//	maybe i should also move all of this logic into hero controller and have a HeroWon() function?
			
			
			//celebrate
			AudioSource.PlayClipAtPoint(celebrationSound, _hero.transform.position);
		
		}
		
	}

	
}

using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour {	
	
	public AudioClip noBulletsSound;
	public AudioClip pistolShootSound;
	public AudioClip shotgunShootSound;
	public AudioClip heroJumpSound;
	public AudioClip heroScreamSound;
	public AudioClip heroScream2Sound;
	public ParticleSystem bloodParticleSystem;
	
	private Animation _animation;
	private GameObject _gameObject;
	private HeroController _heroController;
	private HeroInventory _heroInventory;
	private DieMenuHUD _dieMenuHUD;
	
	void Start () {
		bloodParticleSystem.enableEmission = false;
		
		_animation = animation;
		_gameObject = gameObject;
		_heroController = GetComponent<HeroController>();
		_heroInventory = GetComponent<HeroInventory>();
		
		GameObject dieMenuHUDObj = GameObject.Find("DieMenuHUD");
		_dieMenuHUD = dieMenuHUDObj.GetComponent<DieMenuHUD>();
	
		// By default loop all animations
		_animation.wrapMode = WrapMode.Loop;
	
		_animation["shoot"].wrapMode = WrapMode.Once;
		_animation["die"].wrapMode = WrapMode.Once;
	
		// The jump animation is clamped and overrides all others
		var jump = _animation["jump"];
		jump.layer = 1;
		jump.enabled = false;
		jump.wrapMode = WrapMode.Clamp;
		
		var shoot = _animation["shoot"];
		shoot.layer = 1;
		shoot.enabled = false;
		shoot.wrapMode = WrapMode.Clamp;
	}

	
	public IEnumerator DidJump(){
		_animation.CrossFadeQueued("jump", 0.3f, QueueMode.PlayNow);
		AudioSource.PlayClipAtPoint(heroJumpSound, _gameObject.transform.position);		
		
		yield return null;
	}
	
	public IEnumerator DidLand(){
		// (nothing yet here...maybe have a land sound?)
		
		yield return null;
	}
	
	public IEnumerator DidShoot(){		
		_animation.CrossFade("shoot");
		
		AudioClip clipToPlay;
		WeaponTypes currentWeapon = _heroInventory.GetCurrentWeapon();
		bool canShoot = _heroInventory.CanShoot();
	
		if (!canShoot){
			clipToPlay = noBulletsSound;
		}
		else if (currentWeapon == WeaponTypes.pistol) {
			clipToPlay = pistolShootSound;
		}else if (currentWeapon == WeaponTypes.shotgun){
			clipToPlay = shotgunShootSound;
		}else{
			//should not happen, but just in case play a random sound
			clipToPlay = pistolShootSound;		
		}	
		
		
		// 0.2 seconds into the animation is when our hero aims his gun directly(6 o'clock) at the zombies
		yield return new WaitForSeconds(0.2f);
		AudioSource.PlayClipAtPoint(clipToPlay, _gameObject.transform.position);	
	
		//yield return new WaitForSeconds(0.5f);
		// wait for the guy to complete the shooting animation
		while ( _animation.IsPlaying("shoot") ){
			yield return null;
		}
				
		_heroController.FinishedShooting();	
		
		yield return null;
	}
	
	
	public IEnumerator DidDie(){
		_animation.CrossFade("die");
		bloodParticleSystem.enableEmission = true;	
		
		yield return new WaitForSeconds (0.2f);
		
		AudioClip clipToPlay;
		
		if (Random.Range(1,10) % 2 == 0){
			clipToPlay = heroScreamSound;	
		}else{
			clipToPlay = heroScream2Sound;
		}
		
		AudioSource.PlayClipAtPoint(clipToPlay, _gameObject.transform.position);	

		GameObject zombieSpawner = GameObject.Find("ZombieSpawner");
		Destroy(zombieSpawner);
		
		
		yield return new WaitForSeconds(2.0f);
		
		//Application.LoadLevel("jon_level");	
		_dieMenuHUD.ShowMenu();
		
		
		yield return null;
	}
}

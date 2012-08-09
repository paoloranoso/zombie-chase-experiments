using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour {
	
	public AudioClip zombieGrowlSound;
	public AudioClip zombieGrowlSound2;
	public AudioClip zombieEatingSound;
	public AudioClip zombieHurtSound;
	
	public ParticleSystem bloodParticleSystem;
	
	private Animation _animation;
	private GameObject _gameObject;
	private Transform _transform;
	private ZombieController _zombieController;
	private CharacterController _characterController;
	private IncreaseScore _heroScore;
	
	void Awake(){
		_animation = animation;
		_gameObject = gameObject;
		_transform = transform;
		_zombieController = GetComponent<ZombieController>();
		_characterController = _gameObject.GetComponent<CharacterController>();
		
		GameObject score = GameObject.FindGameObjectWithTag("score");
		_heroScore = score.GetComponent<IncreaseScore>();
		
		// By default loop all animations
		_animation.wrapMode = WrapMode.Loop;
	
		_animation["zombiehurt"].wrapMode = WrapMode.Once;
		_animation["zombieeat"].wrapMode = WrapMode.Once;
		_animation["die"].wrapMode = WrapMode.Once;
		
		var hurt = animation["zombiehurt"];
		hurt.layer = 1;
		hurt.enabled = false;
		hurt.wrapMode = WrapMode.Clamp;		
	}
	
	
	void Start () {
		bloodParticleSystem.enableEmission = false;
		
		// start off with a growl when come to life		
		AudioClip clipToPlay;
		if (Random.Range(1,10) % 2 == 0){
			clipToPlay = zombieGrowlSound;	
		}else{
			clipToPlay = zombieGrowlSound2;
		}		
		AudioSource.PlayClipAtPoint(clipToPlay, _transform.position);
	}

	

	public IEnumerator DidEatHero(){
		_animation.CrossFade("zombieeat");
	
		yield return new WaitForSeconds(1.0f);
	
		// we need to add an audiosource here so we can loop a sound
	    AudioSource source = _gameObject.AddComponent<AudioSource>();
	    source.clip = zombieEatingSound;
	    //source.loop = true;  //commented out for now since it keeps playing even if we press pause
	    source.Play();
	
		_animation.CrossFade("zombiekeepeating");	
		
		yield return null;
	}

	
	public IEnumerator DidGetHurt(){
		// wait for the bullet to hit us
		yield return new WaitForSeconds(0.4f);

		AudioSource.PlayClipAtPoint(zombieHurtSound, _transform.position);
		
		bloodParticleSystem.enableEmission = true;
		
		if (_zombieController.GetHealth() <= 0) {		
			//successfully killed zombie, get some points!
			_heroScore.AddPoints(50);
			_zombieController.SetState(ZombieState.Dying);
		}else{
			_animation.CrossFade("zombiehurt");		
			//show blood only for a bit then turn it back off
			yield return new WaitForSeconds(0.3f);
			bloodParticleSystem.enableEmission = false;
			
		}
				
		yield return null;
	}

	
	public IEnumerator DidDie(){
		// destroy controllers so that other zombies don't get blocked
		//Destroy(_zombieController);
		//Destroy(_characterController);
		//TODO: temporary fix for now until we find out a better way(this produces a warning)
		_characterController.enabled = false;
		
		animation.CrossFade("die");
		bloodParticleSystem.enableEmission = true;
		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
		
		yield return null;
	}

	
	
}

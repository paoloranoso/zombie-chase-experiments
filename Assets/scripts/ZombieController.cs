using UnityEngine;
using System.Collections;

public enum ZombieState {Running=0, GettingHurt, Dying, EatingHero};

[RequireComponent(typeof (CharacterController))]
public class ZombieController : MonoBehaviour {

	public int health = 3;
	public float moveSpeed = 5.0f;

	private ZombieState _state = ZombieState.Running;

	private int _damageTaken = 0;
	private bool _isDead = false;
	private bool _eatingHero = false;

	private Vector3 _moveDirection = Vector3.zero;

	private Transform _transform;
	private CharacterController _characterController;
	private GameObject _hero;
	private HeroController _heroController;
	private ZombieAnimation _zombieAnimations;


	void Awake() {

		_transform = transform;
		_characterController = GetComponent<CharacterController>();
		_zombieAnimations = GetComponent<ZombieAnimation>();

		_hero = GameObject.FindGameObjectWithTag("hero");
		_heroController = _hero.GetComponent<HeroController>();

		moveSpeed = _heroController.GetMoveSpeed();

		InvokeRepeating("IncreaseZombieSpeed", 5, 10);
		InvokeRepeating("DieIfTooFar", 5, 5);
		InvokeRepeating("LookAtHero", 0.1f, 0.1f); //only call every .2 seconds to save on performance

		_moveDirection = _hero.transform.TransformDirection(Vector3.forward);

	}

	void Start(){
		StartCoroutine( GetTemporarySpeedBoost() );

		//just like hero controller, use a coroutine for zombie's state machine slightly better performance
		StartCoroutine(ZombieStateMachine());
	}


	private IEnumerator ZombieStateMachine(){
		while ( !_isDead && !_eatingHero ){
			yield return null;

			// first we'll add on any code to our normal running/tilting code based on hero's state
			switch (_state){
				case ZombieState.Running:
					break;
				case ZombieState.GettingHurt:
					GetHurt();
					break;
				case ZombieState.Dying:
					Die();
					break;
				case ZombieState.EatingHero:
					EatHero();
					break;
			}

			// then finally, on every frame, do the running code and reset state back to running
			if (!_isDead){
				ProcessMovement();
				SetState(ZombieState.Running);
			}

		}

	}


	private void ProcessMovement(){

		_moveDirection = transform.TransformDirection(Vector3.forward);
		_characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);

	}


	public IEnumerator GetTemporarySpeedBoost(){
		//give the zombie a temporary speed boost of 1 second to catch up to the player
		moveSpeed = _heroController.GetMoveSpeed() + 2.0f;
		yield return new WaitForSeconds(1.0f);
		moveSpeed = _heroController.GetMoveSpeed();

		yield return null;

	}


	private void IncreaseZombieSpeed(){
		//moveSpeed = _heroController.GetMoveSpeed();
		moveSpeed += 1.0f;
	}



	private void DieIfTooFar(){
		Vector3 zombiePosition = _transform.position;
		Vector3 heroPosition = _hero.transform.position;

		Vector3 difference = zombiePosition - heroPosition;
		float distanceSquared = difference.sqrMagnitude;

		//die if way behind the player
		if ( distanceSquared > 15.0 ){
			SetState(ZombieState.Dying);
		}
	}


	private void LookAtHero(){
		_transform.LookAt(_hero.transform.position);
	}


	private void GetHurt(){
		health -= _damageTaken;
		StartCoroutine( _zombieAnimations.DidGetHurt() );
	}


	private void Die(){
		moveSpeed = 0.0f;
		CancelInvoke();
		_isDead = true;
		StartCoroutine( _zombieAnimations.DidDie() );
	}


	private void EatHero(){
		moveSpeed = 0.0f;
		_eatingHero = true;
		_heroController.SetState(HeroState.Dying);
		CancelInvoke();
		StartCoroutine( _zombieAnimations.DidEatHero() );
	}


	//--------------------
	//	 PUBLIC METHODS
	//--------------------
	public void HeroShotZombieWithDamage(int damage){
		_damageTaken = damage;
		SetState(ZombieState.GettingHurt);
	}


	public int GetHealth(){
		return health;
	}


	public void SetState(ZombieState state){
		_state = state;
	}

	public void GetMassiveSpeedBoost(){
		moveSpeed = _heroController.GetMoveSpeed() + 5.0f;
	}



	//--------------------------------
	//  Triggers/Collision Detection
	//--------------------------------
	public void OnTriggerEnter(Collider other){
		// only kill player if he didn't shoot and kill us first, since it takes time for our body to disappear
		if ( ( health > 0 ) && (other.gameObject.tag == "hero") ){
			if ( _heroController.IsInvincible() ){
				Die();
			}else{
				SetState(ZombieState.EatingHero);
			}
		}
	}


}

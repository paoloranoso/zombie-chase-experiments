using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController))]

public class HeroAction : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private HeroActions _currentAction = HeroActions.Running;
	private SwipeInput _swipeInput = SwipeInput.Nothing;

	private Vector3 _moveDirection = Vector3.zero;
	private Vector3 _tiltDirection = Vector3.zero;

	private bool _isInvincible = false;
	private bool _isDead = false;
	private bool _isShooting = false;

	private float _tiltSpeed = 0.0f;
	private float _verticalSpeed = 0.0f;
	private bool _isGrounded = true;
	private bool _isJumping = false;

	//caching vars for optimization
	private Transform _transform;
	private Animation _animation;
	private CharacterController _characterController;
	private HeroInput _heroInputScript;
//	private HeroAnimation _heroAnimations;
//	private HeroInventory _heroInventory;


	void Awake() {
		Screen.orientation = ScreenOrientation.Portrait; // init for tilting code

		//cache stuff here that unity tries to get often, especially stuff in update loop
		_transform = transform;
		_animation = animation;
		_characterController = GetComponent<CharacterController>();
		_heroInputScript = GetComponent<HeroInput>();
		//_heroAnimations = GetComponent<HeroAnimation>();
		//_heroInventory = GetComponent<HeroInventory>();

		_moveDirection = _transform.TransformDirection(Vector3.forward);

		//InvokeRepeating("IncreaseHeroSpeed", 5, 10);


		_animation["jump"].layer = 1;
		_animation["shoot"].layer = 1;
	}

	void Start(){
		// we'll use a coroutine instead of Update() to deal with our hero's state for slightly better performance
		StartCoroutine( ProcessHeroActions() );
	}


	private IEnumerator ProcessHeroActions(){
		while ( !_isDead ){

			// TODO: only get swipe input if we are not currently processing
			//if ( (_currentAction == HeroActions.Running) || (_currentAction == HeroActions.Jumping) ){
			_swipeInput = _heroInputScript.GetSwipeInput();
			//}



			// first we'll add on any code to our normal running/tilting code based on hero's state
			switch ( _swipeInput ){
				case SwipeInput.Nothing:
					break;
				case SwipeInput.SwipedLeft:
					TurnHero(TurnDirections.Left);
					break;
				case SwipeInput.SwipedRight:
					TurnHero(TurnDirections.Right);
					break;
				case SwipeInput.SwipedUp:
					Jump();
					break;
				case SwipeInput.SwipedDown:
					//ShootNearestZombie();
					break;
				//case HeroActions.Dying:
					//Die();
				//	break;
				default: break;
			}

			// then finally, on every frame, do actual tilting/running code
			ProcessTilting();
			ProcessMovement();

			//reset state back to running <-- TODO: this will be done in individual actions to tell us when they are done
			//_currentAction =  HeroActions.Running;

			yield return null;
		}
	}


	private void ProcessTilting(){
		if ( Input.acceleration.x != 0.0f ){
		    Vector3 dir = Vector3.zero;
		    dir.x = Input.acceleration.x;

		    // clamp acceleration if too little(to avoid jittering) or too large(to avoid jerking)
			if (dir.sqrMagnitude < 0.005){
				dir = new Vector3(0,0,0);
				_tiltSpeed = 5.0f;
			}
		    if (dir.sqrMagnitude > 0.75){
		        dir.Normalize();
		        _tiltSpeed = 10.0f;
		    }

		    _tiltDirection = dir;
		    _tiltDirection = _transform.TransformDirection(_tiltDirection);
		    _tiltDirection *= _tiltSpeed;
		}
	}

	private void ProcessMovement(){
		// Apply gravity
		_verticalSpeed -= gravity * Time.deltaTime;

		// how to actually move the character, based on their actions (tilting, turning, jumping, etc)
		Vector3 movement = _moveDirection * moveSpeed + new Vector3(0, _verticalSpeed, 0) + _tiltDirection;
		movement *= Time.deltaTime;

		//reset tilting
		_tiltDirection = Vector3.zero;

		// Move the controller
		CollisionFlags flags = _characterController.Move(movement);
		_isGrounded = (flags & CollisionFlags.CollidedBelow) != 0;

		if (_isGrounded){
			_verticalSpeed = 0.0f;
		}

		// Set rotation to the move direction
		_transform.rotation = Quaternion.LookRotation(_moveDirection);


		// We are in jump mode but just became grounded
		if (_isGrounded && _isJumping){
			_isJumping = false;
			_currentAction = HeroActions.Running;
		}

	}


	private void TurnHero(TurnDirections direction){
		float directionToTurn = (float)direction;
		_moveDirection = Quaternion.Euler(0, directionToTurn, 0) * _moveDirection;
	}


	private void Jump(){
		// only jump if the hero is on the ground
		if ( _isGrounded ) {

			_verticalSpeed = jumpSpeed;
			_isJumping = true;




			//StartCoroutine(_heroAnimations.DidJump() );

			_animation.CrossFadeQueued("jump", 0.3f, QueueMode.PlayNow);
			//AudioSource.PlayClipAtPoint(heroJumpSound, _gameObject.transform.position);

			//yield return null;



		}
	}












/*


	private void ShootNearestZombie(){

		// only shoot if we aren't in the middle of shooting already
		if ( !_isShooting ) {
			StartCoroutine(_heroAnimations.DidShoot() );
			_isShooting = true;

			//find closest zombie and hurt him, but only if we have bullets!
			if ( _heroInventory.CanShoot() ) {
				GameObject closestZombie = FindClosestZombie();

				if (closestZombie != null){
					ZombieController zombieController = closestZombie.GetComponent<ZombieController>();
					if (!zombieController){
						return;
					}
					int damage = _heroInventory.GetCurrentWeaponDamage();
					zombieController.HeroShotZombieWithDamage(damage);
				}
			}

		}
	}


	private void Die(){
		moveSpeed = 0.0f;
		CancelInvoke();
		_isDead = true;
		StartCoroutine(_heroAnimations.DidDie() );
	}


	private GameObject FindClosestZombie(){
		GameObject[] allZombies;
		GameObject closest = null;

		float distance = Mathf.Infinity;
		Vector3 position = _transform.position;

		allZombies = GameObject.FindGameObjectsWithTag("zombie");

		foreach (GameObject zombie in allZombies){
	        Vector3 diff = (zombie.transform.position - position);
	        float curDistance = diff.sqrMagnitude;
	        if (curDistance < distance) {
	            closest = zombie;
	            distance = curDistance;
	        }
		}
		return closest;
	}


	// this permanently increases hero speed over time(since it is run as an invoke repeating function)
	private void IncreaseHeroSpeed(){
		moveSpeed += 1;
	}




	//----------------------------
	//	PUBLIC METHODS
	//----------------------------
	public void FinishedShooting(){

		_isShooting = false;
		_heroInventory.UsedABullet();
	}

	public void SetState(HeroState state){
		_currentAction = state;
	}

	public float GetMoveSpeed(){
		return moveSpeed;
	}

	public bool IsInvincible(){
		return _isInvincible;
	}


	//SPECIAL/PAID ITEMS
	public IEnumerator UseSuperSpeed(float additionalSpeed){
		//get super speed for 10 secs
		moveSpeed += additionalSpeed;
		yield return new WaitForSeconds (10.0f);
		moveSpeed -= additionalSpeed;
	}

	public IEnumerator UseInvincibility(){
		//get super speed for 10 secs
		_isInvincible = true;
		yield return new WaitForSeconds (10.0f);
		_isInvincible = false;
	}

	public IEnumerator UseTeleportToFinish(){
		//drop right above the finish zone
		GameObject finishZone = GameObject.Find("finishZone");
		_transform.position = finishZone.transform.position;
		yield return null;
	}

	public IEnumerator ReceiveAShotgun(){
		//get a shotgun!
		_heroInventory.PickedUpWeapon(WeaponTypes.shotgun);
		//AudioSource.PlayClipAtPoint(shotgunPickupSound, _transform.position);
		yield return null;
	}






*/

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "turnTrigger"){
			Debug.Log("ENTERED TURN TRIGGER");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "turnTrigger"){
			Debug.Log("EXITED TURN TRIGGER");
		}
	}


}

using UnityEngine;
using System.Collections;

public class InputListener : MonoBehaviour {

	public float minSwipeThreshold = 20.0f;
	
	private Touch _touch;
	private Vector2 _swipeStart = Vector2.zero;
	private Vector2 _swipeEnd = Vector2.zero;
	private bool _swipeWasActive = false;
	private float _swipeDirectionVertical = 0.0f;
	private float _swipeDirectionHorizontal = 0.0f;
	
	public bool gamePaused = false;
	
	private GameObject _hero;
	private HeroController _heroController;
		
	void Awake () {
		_hero = GameObject.FindGameObjectWithTag("hero");
		_heroController = _hero.GetComponent<HeroController>();
	}
	
	void Start(){
		//use a coroutine for slightly better performance
		if ( SystemInfo.deviceType == DeviceType.Handheld ){
			StartCoroutine(ProcessMobileInput());
		}else{
			StartCoroutine( ProcessKeyboardInput() );
		}		
	}
	
	private IEnumerator ProcessKeyboardInput(){
		while (true){
			
			yield return null;
			
			if ( !gamePaused ){
				// Keyboard input 
				if ( Input.GetButtonUp("Jump") ){
					_heroController.SetState(HeroState.Shooting);
				}
				if ( Input.GetButtonUp("Up") ){
					_heroController.SetState(HeroState.Jumping);
				}
				if ( Input.GetButtonUp("Down") ){
					_heroController.SetState(HeroState.Shooting);
				}
				if ( Input.GetButtonUp("Left") ){
					_heroController.SetState(HeroState.TurningLeft);
				}
				if ( Input.GetButtonUp("Right") ){
					_heroController.SetState(HeroState.TurningRight);
				}			
			}
		
		}
	}
	
	private IEnumerator ProcessMobileInput() {
		while (true){
			
			yield return null;
			
		    if (Input.touchCount == 1) {
		        _touch = Input.touches[0];
	
				
				//did not hit pause button...possible swipe, but only check if game not paused
				if ( !gamePaused ){

			        switch (_touch.phase) {
			            case TouchPhase.Began:
							_swipeStart = _touch.position;                                				
							_swipeWasActive = true;
			                break;
			        
			            case TouchPhase.Moved:
				            _swipeEnd = _touch.position;		
				            _swipeDirectionVertical = _swipeEnd.y - _swipeStart.y;
				            _swipeDirectionHorizontal = _swipeEnd.x - _swipeStart.x;
			
				            //not considered a swipe since we didn't swipe wide enough, break out
				            if ( ( Mathf.Abs(_swipeDirectionHorizontal) < minSwipeThreshold ) 
				            		&& ( Mathf.Abs(_swipeDirectionVertical) < minSwipeThreshold ) ){
				            	break;
				            	//_swipeWasActive = false;
				            }
			
			            	if ( _swipeWasActive ){
								if ( Mathf.Abs(_swipeDirectionHorizontal) > Mathf.Abs(_swipeDirectionVertical) ){
						            if ( _swipeDirectionHorizontal < 0.0f ){			        
						            	_heroController.SetState(HeroState.TurningLeft);
						            }else{
						                _heroController.SetState(HeroState.TurningRight);
						            }	
								}else{
						            if ( _swipeDirectionVertical < 0.0f ){
						            	_heroController.SetState(HeroState.Shooting);
						            }else{
										_heroController.SetState(HeroState.Jumping);
						            }	
								}
								_swipeWasActive = false;
			            	}				
			                break;
			                                
			        	default: break;
			        	
			        } 					
					
				}				
				
		
		    }		
							
			
		}			
	}
	
	
	
	public void pauseGame(){
		gamePaused = true;
	}
	
	public void unpauseGame(){
		gamePaused = false;
	}
	
}

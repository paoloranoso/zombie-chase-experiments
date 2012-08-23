using UnityEngine;
using System.Collections;

public class HeroInput : MonoBehaviour {

	public float minSwipeThreshold = 20.0f;

	private SwipeInput _swipeInput = SwipeInput.Nothing;

	private Touch _touch;
	private Vector2 _swipeStart;
	private Vector2 _swipeEnd;
	private bool _swipeWasActive;
	private float _swipeDirectionVertical;
	private float _swipeDirectionHorizontal;


	void Start(){
		//use a coroutine for slightly better performance
		if ( SystemInfo.deviceType == DeviceType.Handheld ){
			StartCoroutine(ProcessMobileInput());
		}else{
			StartCoroutine( ProcessKeyboardInput() );
		}

		// TODO: start coroutine that checks if game is paused or not
	}

	public SwipeInput GetSwipeInput(){
		return _swipeInput;
	}

	private IEnumerator ProcessKeyboardInput(){
		while (true){

			if ( !LevelData.gamePaused ){
				// Keyboard input
				if ( Input.GetButtonUp("Jump") ){
					_swipeInput = SwipeInput.SwipedDown;
				}
				if ( Input.GetButtonUp("Up") ){
					_swipeInput = SwipeInput.SwipedUp;
				}
				if ( Input.GetButtonUp("Down") ){
					_swipeInput = SwipeInput.SwipedDown;
				}
				if ( Input.GetButtonUp("Left") ){
					_swipeInput = SwipeInput.SwipedLeft;
				}
				if ( Input.GetButtonUp("Right") ){
					_swipeInput = SwipeInput.SwipedRight;
				}
			}

			yield return null;
		}
	}

	private IEnumerator ProcessMobileInput() {
		while (true){

		    if (Input.touchCount == 1) {
		        _touch = Input.touches[0];

				if ( !LevelData.gamePaused ){

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
						            	_swipeInput = SwipeInput.SwipedLeft;
						            }else{
						                _swipeInput = SwipeInput.SwipedRight;
						            }
								}else{
						            if ( _swipeDirectionVertical < 0.0f ){
						            	_swipeInput = SwipeInput.SwipedDown;
						            }else{
										_swipeInput = SwipeInput.SwipedUp;
						            }
								}
								_swipeWasActive = false;
			            	}
			                break;

			        	default: break;

			        }

				}


		    }

			yield return null;

		}
	}

}

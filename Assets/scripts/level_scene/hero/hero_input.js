#pragma strict


var minSwipeThreshold : float = 20.0;

public enum SwipeInput {Nothing=0, SwipedLeft, SwipedRight, SwipedUp, SwipedDown};

private var _gamePaused : boolean = false;
private var _swipeInput : SwipeInput = SwipeInput.Nothing;

private var _touch : Touch;
private var _swipeStart :Vector2 = Vector2.zero;
private var _swipeEnd : Vector2 = Vector2.zero;
private var _swipeWasActive : boolean = false;
private var _swipeDirectionVertical : float = 0.0;
private var _swipeDirectionHorizontal : float = 0.0;


function Start(){
	if ( SystemInfo.deviceType == DeviceType.Handheld ){
		ProcessMobileInput();
	}else{
		ProcessKeyboardInput();
	}
}


public function GetSwipeInput(){
	return _swipeInput;
}


private function ProcessKeyboardInput(){
	while (true){

		yield;

		if ( !_gamePaused ){
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

	}
}

private function ProcessMobileInput() {
	while (true){

		yield;

	    if (Input.touchCount == 1) {
	        _touch = Input.touches[0];


			//did not hit pause button...possible swipe, but only check if game not paused
			if ( !_gamePaused ){

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
					            if ( _swipeDirectionHorizontal < 0.0 ){
					            	_swipeInput = SwipeInput.SwipedLeft;
					            }else{
					                _swipeInput = SwipeInput.SwipedRight;
					            }
							}else{
					            if ( _swipeDirectionVertical < 0.0 ){
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


	}
}


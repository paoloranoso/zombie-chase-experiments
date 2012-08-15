#pragma strict

public var tapDownSound : AudioClip;

private var _touch : Touch;

private var _ray : Ray;
private var _hit : RaycastHit;

function Start () {
	if ( SystemInfo.deviceType == DeviceType.Handheld ){
		ProcessMobileInput();
	}else{
		ProcessMouseInput();
	}
}


function ProcessMouseInput(){
	while (true){

		yield;

		if ( Input.GetButtonDown("Fire1") ){
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		    if (Physics.Raycast (_ray, _hit, 50.0)) {
		        _hit.transform.gameObject.SendMessage("Selected", null, SendMessageOptions.DontRequireReceiver);
		    }
		}

	}
}


function ProcessMobileInput(){
	while (true){

		yield;

	    if (Input.touchCount == 1) {
	        _touch = Input.touches[0];

	 		if (_touch.phase == TouchPhase.Began){

				_ray = Camera.main.ScreenPointToRay(Vector3(_touch.position.x, _touch.position.y));

			    if (Physics.Raycast (_ray, _hit, 50.0)) {
			        _hit.transform.gameObject.SendMessage("Selected", null, SendMessageOptions.DontRequireReceiver);
			    }
	 		}
	    }

	}
}

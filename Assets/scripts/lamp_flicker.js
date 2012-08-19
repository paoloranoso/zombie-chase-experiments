#pragma strict

private var _isOn : boolean = true;
private var _light : Light;

function Awake() {
	_light = gameObject.GetComponent('Light');
}

function Start () {
	FlickerLightRandomly();
}


function FlickerLightRandomly(){
	while (true){
		var randomWaitMultiplier : int = Random.Range(1,3);

		var waitTime : float = randomWaitMultiplier * 0.2;

		yield WaitForSeconds( waitTime );

		if (_isOn){
			_light.range = 0.0;
			_isOn = false;
		}else{
			_light.range = 10.0;
			_isOn = true;
		}
	}
}

#pragma strict

var theTrigger : GameObject;

private var _gameObject : GameObject;

function Awake(){
	_gameObject = gameObject;
}

function OnTriggerEnter(other : Collider) {
	if ( other.gameObject == theTrigger ){
		_gameObject.SetActive(false);
	}
}

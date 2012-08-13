#pragma strict

var theTrigger : GameObject;

function OnTriggerEnter(other : Collider) {
	if ( other.gameObject == theTrigger ){
		gameObject.SetActiveRecursively(false);
	}
}

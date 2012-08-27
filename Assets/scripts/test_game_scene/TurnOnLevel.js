#pragma strict

var spawnMarkerTransform : Transform;
var straightPiece : GameObject;
var tPiece : GameObject;


function OnTriggerEnter(other : Collider){

	if (other.gameObject.name == "hero"){
		//Debug.Log("COLLIDED!");
		// levelPiece.SetActiveRecursively(true);
		tPiece.transform.position = spawnMarkerTransform.position;

	}
}


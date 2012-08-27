#pragma strict

var levelPiece : GameObject;
var lengthGameOjbect : GameObject;
var tPiece : GameObject;

function OnTriggerEnter(other : Collider){

	if (other.gameObject.name == "hero"){
		Debug.Log("COLLIDED!");
		// levelPiece.SetActiveRecursively(true);
		tPiece.transform.position = Vector3(lengthGameOjbect.transform.position.x, lengthGameOjbect.transform.position.y,
											lengthGameOjbect.transform.position.z + lengthGameOjbect.renderer.bounds.size.z / 2 );

	}
}


function OnCollisionEnter(collision : Collision) {
	Debug.Log("YEAH BITCH!");

}

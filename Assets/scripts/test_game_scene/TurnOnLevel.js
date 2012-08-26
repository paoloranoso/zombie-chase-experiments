#pragma strict

var levelPiece : GameObject;
var lengthGameOjbect : GameObject;

function OnTriggerEnter(other : Collider){

	if (other.gameObject.name == "hero"){
		Debug.Log("COLLIDED!");
		// levelPiece.SetActiveRecursively(true);
		levelPiece.transform.position = Vector3(lengthGameOjbect.transform.position.x, lengthGameOjbect.transform.position.y,
											lengthGameOjbect.transform.position.z + lengthGameOjbect.renderer.bounds.size.z);

	}
}


function OnCollisionEnter(collision : Collision) {
	Debug.Log("YEAH BITCH!");

}

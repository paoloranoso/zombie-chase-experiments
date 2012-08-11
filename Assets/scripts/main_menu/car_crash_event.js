#pragma strict

var logoBillboard : GameObject;
var lamp : GameObject;
var carCrashSound : AudioClip;
var billboardFallSound : AudioClip;

function OnTriggerEnter(other : Collider){
	// Debug.Log("car hit post!");
	Destroy(GetComponent('Rigidbody'));
	Destroy(other.gameObject);

	DropDownBillBoard();
}

function DropDownBillBoard(){
	//drop the billboard by adding a rigid body to it
	AudioSource.PlayClipAtPoint(carCrashSound, transform.position);
	yield WaitForSeconds(1);

	logoBillboard.AddComponent('Rigidbody');
	lamp.AddComponent('Rigidbody');

	yield WaitForSeconds(0.5);
	AudioSource.PlayClipAtPoint(billboardFallSound, logoBillboard.transform.position);

}

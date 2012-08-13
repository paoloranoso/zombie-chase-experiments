#pragma strict

var logoBillboard : GameObject;
var lamp : GameObject;
var characters : GameObject;

var carCrashSound : AudioClip;
var billboardFallSound : AudioClip;

function OnTriggerEnter(other : Collider){
	// Debug.Log("car hit post!");
	Destroy(GetComponent('Rigidbody'));
	Destroy(other.gameObject);

	DropDownBillBoard();
}

function DropDownBillBoard(){
	//car crashes first
	AudioSource.PlayClipAtPoint(carCrashSound, transform.position);
	yield WaitForSeconds(1);

	//billboard and lamp start falling
	logoBillboard.AddComponent('Rigidbody');
	lamp.AddComponent('Rigidbody');

	//billboard falls and plays sound
	yield WaitForSeconds(0.5);
	AudioSource.PlayClipAtPoint(billboardFallSound, logoBillboard.transform.position);

	//hero runs across screen being chased by zombie herd
	yield WaitForSeconds(2);
	characters.SetActiveRecursively(true);
}

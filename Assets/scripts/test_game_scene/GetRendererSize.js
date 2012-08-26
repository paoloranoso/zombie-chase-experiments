#pragma strict

var target : GameObject;

function Start () {
	Debug.Log("Size is " + target.renderer.bounds.size * 12 );
}

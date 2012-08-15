#pragma strict

var menuItemSelectedSound : AudioClip;
var inputListener : GameObject;


private var _transform : Transform;


function Awake(){
	_transform = transform;
}

function Selected(){
	renderer.material.color = Color.red;
	AudioSource.PlayClipAtPoint(menuItemSelectedSound, _transform.position);

	//destroy the input listener so user cannot tap anymore and mess things up
	Destroy(inputListener);
}

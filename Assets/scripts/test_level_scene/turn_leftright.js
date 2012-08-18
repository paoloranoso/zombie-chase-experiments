#pragma strict

var rotateSpeed : float;

private var _transform : Transform;

function Start () {
	_transform = transform;
}

function Update () {
	// if ( Input.GetAxis("Horizontal") ){


	_transform.Rotate( 0, (Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime), 0 );
	// }
}

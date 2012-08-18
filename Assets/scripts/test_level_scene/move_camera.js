#pragma strict

var moveSpeed :float;

private var _transform : Transform;

function Start () {
	_transform = transform;
}

function Update () {
	_transform.Translate(Vector3(0,0, 1) * moveSpeed * Time.deltaTime );
}

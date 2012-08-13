#pragma strict

var moveSpeed : float;

private var _transform : Transform;

function Start () {
	_transform = transform;
}

function Update () {
	transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
}

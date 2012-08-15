#pragma strict

var moveSpeed : float;

private var _transform : Transform;

function Awake () {
	_transform = transform;
}

function Update () {
	_transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
}

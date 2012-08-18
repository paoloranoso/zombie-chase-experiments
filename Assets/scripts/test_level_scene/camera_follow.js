#pragma strict

var target : Transform;

private var _transform : Transform;

function Awake () {
	_transform = transform;
}

function LateUpdate () {
	_transform.LookAt(target);
}

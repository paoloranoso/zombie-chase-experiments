#pragma strict

private var _gameObject : GameObject;

function Awake(){
	_gameObject = gameObject;
}

function Start () {
	iTween.MoveBy(_gameObject, Vector3(-1.2, 0, 0), 1.0);
}

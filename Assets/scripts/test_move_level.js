#pragma strict

var moveSpeed : float = 10.0;
var rotationSpeed : float = 100.0;

private var _transform : Transform;

function Start () {
	_transform = transform;
}

function Update () {
	var translation : float = Input.GetAxis ("Vertical") * moveSpeed;

	translation *= Time.deltaTime;
	///rotation *= Time.deltaTime;

	_transform.Translate (0, 0, translation);



	if (Input.GetButtonUp("Jump")){
		//_transform.Rotate (0, -90, 0);
		var cameraGO : GameObject = GameObject.Find("Main Camera");
		cameraGO.transform.Rotate(Vector3(0,90,0) );
	}

}

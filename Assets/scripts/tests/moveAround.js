#pragma strict

var moveSpeed : float = 10.0;
var rotationSpeed : float = 200.0;

private var _transform : Transform;

function Start () {
	_transform = transform;
}

function Update () {
	var translation : float = Input.GetAxis ("Vertical") * moveSpeed;
	var rotation : float = Input.GetAxis ("Horizontal") * rotationSpeed;


	translation *= Time.deltaTime;
	rotation *= Time.deltaTime;

	_transform.Translate (0, 0, translation);
	_transform.Rotate (0, rotation, 0);


	// if (Input.GetButtonUp("Jump")){
	// 	_transform.Rotate(Vector3(0,90,0) );
	// }

}

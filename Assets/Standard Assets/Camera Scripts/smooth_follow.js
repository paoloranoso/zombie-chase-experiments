#pragma strict

var target : Transform;
var distance : float = 10.0;
var height :float = 5.0;
var heightDamping : float = 2.0;
var rotationDamping : float = 8.0;

private var _wantedRotationAngle : float;
private var _wantedHeight : float;
private var _currentRotationAngle : float;
private var _currentHeight : float;
private var _currentRotation : Quaternion;

private var _transform : Transform;

function Awake () {
	_transform = transform;
}

function LateUpdate () {
	if (!target)
		return;

	// Calculate the current rotation angles
	_wantedRotationAngle = target.eulerAngles.y;
	_wantedHeight = target.position.y + height;

	_currentRotationAngle = _transform.eulerAngles.y;
	_currentHeight = _transform.position.y;

	// Damp the rotation around the y-axis
	_currentRotationAngle = Mathf.LerpAngle(_currentRotationAngle, _wantedRotationAngle, rotationDamping * Time.deltaTime);

	// Damp the height
	_currentHeight = Mathf.Lerp(_currentHeight, _wantedHeight, heightDamping * Time.deltaTime);

	// Convert the angle into a rotation
	_currentRotation = Quaternion.Euler(0, _currentRotationAngle, 0);

	// Set the position of the camera on the x-z plane to:
	// distance meters behind the target
	_transform.position = target.position;
	_transform.position -= _currentRotation * Vector3.forward * distance;

	// Set the height of the camera
	_transform.position = Vector3(_transform.position.x, _currentHeight, _transform.position.z);

	_transform.LookAt(target);
}

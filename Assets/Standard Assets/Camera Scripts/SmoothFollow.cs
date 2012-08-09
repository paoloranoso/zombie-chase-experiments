using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Smooth Follow")]
public class SmoothFollow : MonoBehaviour {
	public Transform target;
	public float distance = 10.0f;
	public float height = 5.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 8.0f;
	
	private float _wantedRotationAngle = 0.0f;
	private float _wantedHeight = 0.0f;	
	private float _currentRotationAngle = 0.0f;
	private float _currentHeight = 0.0f;
	private Quaternion _currentRotation;
	
	private Transform _myTransform;
	
	void Awake(){
		_myTransform = GetComponent<Transform>();
	}
	
	void LateUpdate () {
		if (!target)
			return;
		
		// Calculate the current rotation angles
		_wantedRotationAngle = target.eulerAngles.y;
		_wantedHeight = target.position.y + height;
			
		_currentRotationAngle = _myTransform.eulerAngles.y;
		_currentHeight = _myTransform.position.y;
		
		// Damp the rotation around the y-axis
		_currentRotationAngle = Mathf.LerpAngle (_currentRotationAngle, _wantedRotationAngle, rotationDamping * Time.deltaTime);
	
		// Damp the height
		_currentHeight = Mathf.Lerp (_currentHeight, _wantedHeight, heightDamping * Time.deltaTime);
	
		// Convert the angle into a rotation
		_currentRotation = Quaternion.Euler (0, _currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		_myTransform.position = target.position;
		_myTransform.position -= _currentRotation * Vector3.forward * distance;
	
		// Set the height of the camera
		//transform.position.y = 8;
		_myTransform.position = new Vector3(_myTransform.position.x, _currentHeight, _myTransform.position.z);
		
		_myTransform.LookAt(target);	
	}
}

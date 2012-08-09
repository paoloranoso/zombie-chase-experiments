using UnityEngine;
using System.Collections;

public class IntroCameraMoves : MonoBehaviour {
	public float totalDistanceToMove = 5.0f;
	public float distancePerStepToMoveBy = 0.01f;

	private Transform _transform;
	
	void Awake() {
		_transform = transform;	
	}
	
	
	void Start () {
		StartCoroutine( CameraMovesForIntro() );
	}

	
	private IEnumerator CameraMovesForIntro(){
		while (totalDistanceToMove > 0.0f){
			yield return null;
			_transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + distancePerStepToMoveBy);
			totalDistanceToMove-= distancePerStepToMoveBy;
		}
	}
	
}

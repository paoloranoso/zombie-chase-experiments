#pragma strict

var straightPrefab : GameObject;
var roadFwdMultiplier : float = 10.0;


private var _endPoint : Transform;
private var _firstRoad : Transform;
private var _firstRoadPosition : Vector3;

function Start () {
	_endPoint = GameObject.Find("road_container").transform;

	MakeMoreRoad();

		Debug.Log(_endPoint.position);
}



function MakeMoreRoad(){

	_endPoint = GameObject.Find("endpoint").transform;

	while (true){

		yield WaitForSeconds(2);

		// var newPosition : Vector3 = _endPoint.position + (Vector3.forward * roadFwdMultiplier);
		// var newRoad : GameObject = Instantiate(straightPrefab, newPosition, _endPoint.rotation);
		// _endPoint = newRoad.transform;


		// var newPosition : Vector3 = _endPoint.position + (Vector3.forward * roadFwdMultiplier);
		var newRoad : GameObject = Instantiate(straightPrefab, _endPoint.position, _endPoint.rotation);
		_endPoint = newRoad.transform.Find("endpoint").transform;

		Debug.Log(_endPoint.position);

	}

}

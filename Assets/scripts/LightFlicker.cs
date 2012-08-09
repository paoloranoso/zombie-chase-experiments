using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	private bool _isOn = true;
	private Light _light;
		
	void Awake() {
		_light = gameObject.GetComponent<Light>();
	}
	
	void Start () {
		StartCoroutine( FlickerLightRandomly() );
	}

	
	private IEnumerator FlickerLightRandomly(){
		while (true){
			int randomWaitMultiplier = Random.Range(1,3);
			
			float waitTime = randomWaitMultiplier * 0.2f;
			
			yield return new WaitForSeconds( waitTime );
			
			if (_isOn){				
				_light.range = 0.0f;
				_isOn = false;
			}else{
				_light.range = 10.0f;
				_isOn = true;
			}
		}
	}
	
}

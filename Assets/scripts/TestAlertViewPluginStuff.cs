using UnityEngine;
using System.Collections;

public class TestAlertViewPluginStuff : MonoBehaviour {

	void Start(){
		//use a coroutine for slightly better performance
		if ( SystemInfo.deviceType == DeviceType.Handheld ){
			StartCoroutine(ProcessMobileInput());
		}else{
			Debug.Log("This app needs to be run on your iOS device in order to test the AlertViewPlugin");
		}		
	}
	
	
	private IEnumerator ProcessMobileInput() {
		while (true){
			
			yield return null;
			
		    if (Input.touchCount == 1) {
				//test normal alert message
				if ( Input.touches[0].phase == TouchPhase.Ended){
					AlertViewPlugin.ShowMessage("test title", "wo0t!  This is a normal alert view working in unity!");				
				}
				
		    }else if (Input.touchCount == 2) {
				//test confirm alert message
				if ( Input.touches[1].phase == TouchPhase.Ended){
					AlertViewPlugin.ShowConfirmMessage("Confirm In-App Purchase", "Are you sure you want to buy the shotgun for $0.99?", "Buy");
				}
			}
										
		}			
	}
	
	
	
}

using UnityEngine;
using System.Collections;

public enum TutorialActions {Nothing, SwipeLeft, SwipeRight, SwipeUp, SwipeDown, Done};


public class Tutorial : MonoBehaviour {
	//whether or not to even have the tutorial
	public bool tutorialIsOn = true;
	public TutorialActions requiredTutorialAction = TutorialActions.Nothing;
	public string hudMessageText;
	public AudioClip swooshSound;
	
	private Transform _transform;
	
	private UIText _text;
	private UITextInstance _hudMessage;
	
	void Awake() {
		_transform = transform;
	}
	
	void Start(){
		_text = new UIText( "prototype", "prototype.png" );
	}
		
	
	public void OnTriggerEnter(Collider other){
		if ( tutorialIsOn && (other.gameObject.tag == "hero") ) {
			AudioSource.PlayClipAtPoint(swooshSound, _transform.position);
			StartCoroutine(ShowTutorialText());
		}	
	}
		
	
	private IEnumerator ShowTutorialText(){
		_hudMessage = _text.addTextInstance(hudMessageText, 0, 0 );
        _hudMessage.pixelsFromTopLeft(100,10);

		if (requiredTutorialAction == TutorialActions.Done){
			yield return new WaitForSeconds(2.0f);
		}else{
	        yield return new WaitForSeconds(0.5f);
		}
        DismissTutorialText();
	}
	
	private void DismissTutorialText(){
		_hudMessage.clear();
		Destroy(gameObject);		
	}
	
}

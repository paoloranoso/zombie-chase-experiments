using UnityEngine;
using System.Collections;

public class DieMenuHUD : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;

	private Transform _transform;

	private UIVerticalLayout _vBox;

	void Awake(){
		_transform = transform;
	}

	void Start () {
		var retryButton = UIButton.create( "retry-up.png", "retry-down.png", 0, 0 );
		var backButton = UIButton.create( "back-up.png", "back-down.png", 0, 0 );
		var bragButton = UIButton.create( "brag-up.png", "brag-down.png", 0, 0 );

		//touch down sounds
		retryButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		backButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		bragButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);

		retryButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel(Application.loadedLevel);
		};
		backButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel("jon_level");
		};
		bragButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);

			//Tweet, with screenshot!
			if ( TwitterPlugin.isAvailable ){
				//get score and brag about it
				GameObject scoreObj = GameObject.FindGameObjectWithTag("score");
				IncreaseScore scoreScript = scoreObj.GetComponent<IncreaseScore>();
				int currentScore = scoreScript.currentScore;

				TwitterPlugin.ComposeTweetWithScreenshot("I scored " + currentScore + " points in Zombie Chase!  Beat that!", "http://bit.ly/KZrFJn");
			}else{
				//GUI.Label(Rect(0, 0, Screen.width, 0.15 * Screen.height), "Twitter API is not available.");
		    }

		};


		_vBox = new UIVerticalLayout(20);
		_vBox.addChild( retryButton, backButton, bragButton );
		_vBox.positionCenter();

		//initially hide off-screen, but w/out animation
		Vector3 offScreenPos = _vBox.position;
		offScreenPos.y += 1000.0f;
		_vBox.position = offScreenPos;
	}

	public void ShowMenu(){
		var target = _vBox.position;
		float moveBy = -1000.0f;
		target.y += moveBy;
		_vBox.positionTo( 2.0f, target, Easing.Quintic.easeIn );
	}

	public void HideMenu(){
		var target = _vBox.position;
		float moveBy = 1000.0f;
		target.y += moveBy;
		_vBox.positionTo( 2.0f, target, Easing.Quintic.easeIn );
	}
}

using UnityEngine;
using System.Collections;

public class WinMenuHUD : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;
	
	private Transform _transform;
	
	private UIVerticalLayout _vBox;
	
	void Awake(){					
		_transform = transform;
	}
	
	void Start () {
		var nextButton = UIButton.create( "next-up.png", "next-down.png", 0, 0 );
		var storeButton = UIButton.create( "store-up.png", "store-down.png", 0, 0 );
		var menuButton = UIButton.create( "menu-up.png", "menu-down.png", 0, 0 );
		var bragButton = UIButton.create( "brag-up.png", "brag-down.png", 0, 0 );
		
		//touch down sounds
		nextButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		storeButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		menuButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		bragButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		
		nextButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel(Application.loadedLevel);			
		};				
		storeButton.onTouchUpInside += sender => {		
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);						
		};				
		menuButton.onTouchUpInside += sender => {		
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
				
				TwitterPlugin.ComposeTweetWithScreenshot("I scored " + currentScore + " points AND survived in Zombie Chase!  Beat that!", "http://bit.ly/KZrFJn");
			}else{
				//GUI.Label(Rect(0, 0, Screen.width, 0.15 * Screen.height), "Twitter API is not available.");
		    }
			
		};				
		
		
		_vBox = new UIVerticalLayout(20);
		_vBox.addChild( nextButton,storeButton, menuButton, bragButton );		
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
	

	public void ShowWinText(){
		//YOU WIN text
		var text = new UIText( "prototype", "prototype.png" );
		
		// spawn new text instances showing off the relative positioning features by placing one text instance in each corner
		// Uses default color, scale, alignment, and depth.
		UITextInstance winText = text.addTextInstance( "SURVIVED!", 0, 0 );
        winText.pixelsFromTopLeft(100,10);
	
	}
}

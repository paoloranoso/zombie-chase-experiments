using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;
	
	private Transform _transform;
		
	void Awake(){					
		_transform = transform;
	}
	
	void Start () {
		var backButton = UIButton.create( "back-up.png", "back-down.png", 0, 0 );
		
		//touch down sounds
		backButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		
		backButton.onTouchUpInside += sender => {		
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);			
			Application.LoadLevel("jon_level");
		};				
		
		backButton.pixelsFromBottomRight(10, 10);


		// info text
		string instructions = "SWIPE LEFT\n\tto turn left\n\n" 
			+ "SWIPE RIGHT\n\tto turn right\n\n"
			+ "SWIPE UP\n\tto jump\n\n"
			+ "SWIPE DOWN\n\tto shoot\n\n";

		var text = new UIText( "prototype", "prototype.png" );
		UITextInstance howToPlayText = text.addTextInstance( instructions, 0, 0 );

		howToPlayText.positionCenter(); //pixelsFromTopLeft(50,20);



	}
	
}

using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
	
	void Start () {
		var playButton = UIButton.create( "play-up.png", "play-down.png", 0, 0 );
		playButton.onTouchUpInside += sender => {			
			int x = 1;
			int y = 2;
			
			Debug.Log("Play button pressed!  Also, value is " + (x+y));
			
		};
		
		//playButton.highlightedTouchOffsets = new UIEdgeOffsets( 30 );
		
		var scores = UIButton.create( "scores-up.png", "scores-down.png", 0, 0 );
		//scores.highlightedTouchOffsets = new UIEdgeOffsets( 30 );
			
		var vBox = new UIVerticalLayout( 20 );
		vBox.addChild( playButton, scores );		
	}

	
}

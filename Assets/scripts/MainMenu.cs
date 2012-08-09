using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public AudioClip tapDownSound;
	public AudioClip tapDecidedSound;

	private Transform _transform;


	void Start () {
		_transform = transform;


		var playButton = UIButton.create( "play-up.png", "play-down.png", 0, 0 );
		var scoresButton = UIButton.create( "scores-up.png", "scores-down.png", 0, 0 );
		var storeButton = UIButton.create( "store-up.png", "store-down.png", 0, 0 );
		var helpButton = UIButton.create( "help-up.png", "help-down.png", 0, 0 );

		//touch down sounds
		playButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(tapDownSound, _transform.position);
		storeButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(tapDownSound, _transform.position);
		scoresButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(tapDownSound, _transform.position);
		helpButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(tapDownSound, _transform.position);

		playButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(tapDecidedSound, _transform.position);
			//Application.LoadLevel("loading_scene");
			//SceneManager.LoadLevel("game_level");
			Application.LoadLevel("level_selector");
		};



		storeButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(tapDecidedSound, _transform.position);
			GameData.previousLevel = "jon_level";
			Application.LoadLevel("store_scene");
		};



		scoresButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(tapDecidedSound, _transform.position);
			//Application.LoadLevel("twitter_test");
		};



		helpButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(tapDecidedSound, _transform.position);
			Application.LoadLevel("how_to_play");
		};



		var vBox = new UIVerticalLayout(20);
		vBox.addChild( playButton, storeButton, scoresButton, helpButton );
		vBox.positionCenter();


		//initially hide off-screen, then ease into from top
		Vector3 offScreenPos = vBox.position;
		offScreenPos.y += 1000.0f;
		vBox.position = offScreenPos;

		offScreenPos.y += -1000.0f;
		vBox.positionTo( 2.0f, offScreenPos, Easing.Quintic.easeIn );
	}


}

using UnityEngine;
using System.Collections;

public class LevelSelectorHUD : MonoBehaviour {

	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;

	private Transform _transform;


	void Awake(){
		_transform = transform;
	}


	void Start () {

		var scrollable = new UIScrollableHorizontalLayout( 50 );

		// we wrap the addition of all the sprites with a begin updates so it only lays out once when complete
		scrollable.beginUpdates();


		var height = Screen.width / 1.1f;
		var width = Screen.width / 1.1f;


		// if you plan on making the scrollable wider than the item width you need to set your edgeInsets so that the
		// left + right inset is equal to the extra width you set
		//scrollable.edgeInsets = new UIEdgeInsets( 0, 75, 0, 75 );
		scrollable.setSize( width, height );

		// paging will snap to the nearest page when scrolling
		//scrollable.pagingEnabled = true;
		//scrollable.pageWidth = 250f * UI.scaleFactor;

		// center the scrollable horizontally
		scrollable.position = new Vector3( ( Screen.width - width ) / 2, 0, 0 );



		var level1Button = UIButton.create( "level1-up.png", "level1-down.png", 0, 0 );
		scrollable.addChild( level1Button );


		for( var i = 0; i < 9; i++ ){
			var lockedLevelButton = UIButton.create( "level1-locked.png", "level1-locked.png", 0, 0 );
			scrollable.addChild( lockedLevelButton );
		}


		scrollable.endUpdates();

		scrollable.positionCenter();

		var backButton = UIButton.create( "back-up.png", "back-down.png", 0, 0 );
		backButton.pixelsFromBottomRight(10, 10);

		var storeButton = UIButton.create( "store-up.png", "store-down.png", 0, 0 );
		storeButton.pixelsFromBottomLeft(10, 10);

		level1Button.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
		backButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
		storeButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);

		level1Button.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
			SceneManager.LoadLevel("game_level");
		};

		storeButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
			GameData.previousLevel = "level_selector";
			Application.LoadLevel("store_scene");
		};

		backButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
			Application.LoadLevel("jon_level");
		};




	}

}

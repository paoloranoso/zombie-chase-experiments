using UnityEngine;
using System.Collections;

public class PauseMenuHUD : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;

	private InputListener _inputListener;
	private AudioSource _backgroundMusicAudioSource;

	private Transform _transform;

	private UIVerticalLayout _vBox;

	void Awake(){
		GameObject inputListenerObj = GameObject.Find("InputListener");
		GameObject backgroundMusicObj = GameObject.Find("backgroundMusic");
		_inputListener = inputListenerObj.GetComponent<InputListener>();
		_backgroundMusicAudioSource = backgroundMusicObj.GetComponent<AudioSource>();

		_transform = transform;
	}

	void Start () {
		var resumeButton = UIButton.create( "resume-up.png", "resume-down.png", 0, 0 );
		var restartButton = UIButton.create( "restart-up.png", "restart-down.png", 0, 0 );
		var menuButton = UIButton.create( "menu-up.png", "menu-down.png", 0, 0 );

		//touch down sounds
		resumeButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		restartButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		menuButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);

		resumeButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			_inputListener.unpauseGame();
			_backgroundMusicAudioSource.volume = 0.5f;
			Time.timeScale = 1.0f;
			HideMenu();
		};
		restartButton.onTouchUpInside += sender => {
			Time.timeScale = 1.0f;
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel(Application.loadedLevel);
		};
		menuButton.onTouchUpInside += sender => {
			//TODO: eventually we'll need to remove this timescale stuff and pause using a different method
			Time.timeScale = 1.0f;
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel("jon_level");
		};


		_vBox = new UIVerticalLayout(20);
		_vBox.addChild( resumeButton, restartButton, menuButton );
		_vBox.positionCenter();

		//initially hide off-screen, but w/out animation
		Vector3 offScreenPos = _vBox.position;
		offScreenPos.y += -1000.0f;
		_vBox.position = offScreenPos;
	}

	public void ShowMenu(){
		_vBox.positionCenter();
	}

	public void HideMenu(){
		// old animation stuff
		//var target = _vBox.position;
		//float moveBy = 1000.0f;
		//target.y += moveBy;
		//_vBox.positionTo( 0.4f, target, Easing.Quintic.easeIn );
		//---------------------------------------

		// hide without animation since it gets off the screen faster
		Vector3 offScreenPos = _vBox.position;
		offScreenPos.y += -1000.0f;
		_vBox.position = offScreenPos;

	}
}

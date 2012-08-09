using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;
	
	private InputListener _inputListener;
	private AudioSource _backgroundMusicAudioSource;
	private Transform _transform;
	
	private PauseMenuHUD _pauseMenuHUD;
	
	void Awake(){
		GameObject inputListenerObj = GameObject.Find("InputListener");
		GameObject backgroundMusicObj = GameObject.Find("backgroundMusic");
		GameObject pauseMenuHUDObj = GameObject.Find("PauseMenuHUD");
		
		_inputListener = inputListenerObj.GetComponent<InputListener>();
		_backgroundMusicAudioSource = backgroundMusicObj.GetComponent<AudioSource>();
		_pauseMenuHUD = pauseMenuHUDObj.GetComponent<PauseMenuHUD>();
		
		_transform = transform;
	}
	
	void Start () {
		Time.timeScale = 1.0f; //just in case we restart game and are frozen
		
		var pauseButton = UIButton.create( "pause-up.png", "pause-down.png", 0, 0 );
		
		pauseButton.positionFromTopRight(0.05f, 0.05f);
		
		pauseButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		
		pauseButton.onTouchUpInside += sender => {		
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			
			if ( _inputListener.gamePaused ){
				_pauseMenuHUD.HideMenu();
				_inputListener.unpauseGame();
				_backgroundMusicAudioSource.volume = 0.5f;
				Time.timeScale = 1.0f;				
			}else{
				_pauseMenuHUD.ShowMenu();
				_inputListener.pauseGame();
				_backgroundMusicAudioSource.volume = 0.0f;
				Time.timeScale = 0.0f;
			}
		};				
	}

}

#pragma strict

var menuItemSelectedSound : AudioClip;
var inputListener : GameObject;
var blackScreenGameObject : GameObject;

private var _transform : Transform;
private var _gameObject : GameObject;

function Awake(){
	_transform = transform;
	_gameObject = gameObject;
}

function Selected(){
	renderer.material.color = Color.red;
	AudioSource.PlayClipAtPoint(menuItemSelectedSound, _transform.position);

	blackScreenGameObject.SendMessage("FadeOut", null, SendMessageOptions.DontRequireReceiver);

	//destroy the input listener so user cannot tap anymore and mess things up
	Destroy(inputListener);

	//allow time for fadeout
	yield WaitForSeconds(1);

	switch (_gameObject.tag){
		case 'play':
			Application.LoadLevelAsync('game_scene');
			break;
		case 'objectives':
			Application.LoadLevelAsync('game_scene');
			break;
		case 'store':
			Application.LoadLevelAsync('game_scene');
			break;
		default: break;
	}

}

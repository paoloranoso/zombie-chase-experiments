using UnityEngine;
using System.Collections;

public enum SpecialItems {Nothing=0, Shotgun, SuperSpeed, Invincible, TeleportToFinish};

public class SpecialItemHUD : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;

	private SpecialItems _specialItem;

	private UIButton _specialItemButton;
	private Transform _transform;
	private HeroController _heroScript;

	void Awake(){
		_transform = transform;

		//============ TESTING ============
		//PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.TeleportToFinish );
		//==================================

		_specialItem = (SpecialItems)PlayerPrefs.GetInt("SpecialItem", 0);

		GameObject hero = GameObject.FindGameObjectWithTag("hero");
		_heroScript = hero.GetComponent<HeroController>();

	}

	void Start () {
		_specialItemButton = UIButton.create( "special-up.png", "special-down.png", 0, 0 );

		//touch down sounds
		_specialItemButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);

		_specialItemButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			UseSpecialItem();
		};


		_specialItemButton.positionFromBottomLeft(0.05f, 0.05f);

		//hide special item button if have no item
		if (_specialItem == SpecialItems.Nothing){
			HideHUD();
		}

	}

	public void ShowHUD(){
		Vector3 offScreenPos = _specialItemButton.position;
		offScreenPos.y += 1000.0f;
		_specialItemButton.position = offScreenPos;
	}

	public void HideHUD(){
		Vector3 offScreenPos = _specialItemButton.position;
		offScreenPos.y += -1000.0f;
		_specialItemButton.position = offScreenPos;
	}




	private void UseSpecialItem(){

		switch ( _specialItem ){
			case SpecialItems.Shotgun:
				Debug.Log("SHOTGUN!");
				StartCoroutine( _heroScript.ReceiveAShotgun() );
				break;
			case SpecialItems.SuperSpeed:
				Debug.Log("SUPER SPEED!");
				StartCoroutine( _heroScript.UseSuperSpeed(10.0f) );
				break;
			case SpecialItems.Invincible:
				Debug.Log("INVINCIBLE!");
				StartCoroutine( _heroScript.UseInvincibility() );
				break;
			case SpecialItems.TeleportToFinish:
				Debug.Log("TELEPORT!");
				StartCoroutine( _heroScript.UseTeleportToFinish() );
				break;
			default:
				Debug.Log("Has no special item...");
				break;
		}


		PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.Nothing );
		HideHUD();

	}

}

using UnityEngine;
using System.Collections;

public class StoreHUD : MonoBehaviour {
	public AudioClip buttonUpSound;
	public AudioClip buttonDownSound;

	private Transform _transform;

	void Awake(){
		_transform = transform;
	}

	void Start () {
		var buyShotgunButton = UIButton.create( "buyshotgun-up.png", "buyshotgun-down.png", 0, 0 );
		var buySuperSpeedButton = UIButton.create( "buysuperspeed-up.png", "buysuperspeed-down.png", 0, 0 );
		var buyInvincibilityButton = UIButton.create( "buyinvincibility-up.png", "buyinvincibility-down.png", 0, 0 );
		var buyTeleportButton = UIButton.create( "buyteleport-up.png", "buyteleport-down.png", 0, 0 );
		var backButton = UIButton.create( "back-up.png", "back-down.png", 0, 0 );

		//touch down sounds
		buyShotgunButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		buySuperSpeedButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		buyInvincibilityButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		buyTeleportButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);
		backButton.onTouchDown += sender => AudioSource.PlayClipAtPoint(buttonUpSound, _transform.position);



		buyShotgunButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			AlertViewPlugin.ShowConfirmMessage("Confirm In-App Purchase", "Do you want to buy one Shotgun Powerup for $0.99?", "Buy");
			PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.Shotgun );
			PlayerPrefs.Save();
		};
		buySuperSpeedButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			AlertViewPlugin.ShowConfirmMessage("Confirm In-App Purchase", "Do you want to buy one Super Speed Powerup for $0.99?", "Buy");
			PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.SuperSpeed );
			PlayerPrefs.Save();
		};
		buyInvincibilityButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			AlertViewPlugin.ShowConfirmMessage("Confirm In-App Purchase", "Do you want to buy one Invincibility Powerup for $1.99?", "Buy");
			PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.Invincible );
			PlayerPrefs.Save();
		};
		buyTeleportButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			AlertViewPlugin.ShowConfirmMessage("Confirm In-App Purchase", "Do you want to buy one Teleport to Finish Powerup for $4.99?", "Buy");
			PlayerPrefs.SetInt( "SpecialItem", (int)SpecialItems.TeleportToFinish );
			PlayerPrefs.Save();
		};

		backButton.onTouchUpInside += sender => {
			AudioSource.PlayClipAtPoint(buttonDownSound, _transform.position);
			Application.LoadLevel(GameData.previousLevel);
		};


		backButton.pixelsFromBottomRight(10, 10);


		// info text
		string instructions = "Buy powerups to\n\taid yourself in-game";

		var text = new UIText( "prototype", "prototype.png" );
		UITextInstance howToPlayText = text.addTextInstance( instructions, 0, 0 );

		howToPlayText.pixelsFromTopLeft(10,20);


		var vBox = new UIVerticalLayout(50);
		vBox.addChild( buyShotgunButton, buySuperSpeedButton, buyInvincibilityButton, buyTeleportButton );
		vBox.alignMode = UIAbstractContainer.UIContainerAlignMode.Center;
		vBox.positionCenter();
		vBox.pixelsFromTop(125);

	}
}

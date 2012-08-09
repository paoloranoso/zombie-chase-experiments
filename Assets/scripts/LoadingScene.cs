using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

	void Start () {
		// loading text
		string loadingMessage = "LOADING...";

		var text = new UIText( "prototype", "prototype.png" );
		UITextInstance loadingText = text.addTextInstance( loadingMessage, 0, 0 );

		loadingText.positionCenter(); //pixelsFromTopLeft(50,20);

		Application.LoadLevel(GameData.nextLevel);
	}



}

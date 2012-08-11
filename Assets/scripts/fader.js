#pragma strict

enum FadeTypes {None=0, FadeIn, FadeOut};


var fadeType : FadeTypes;
var fadeDuration : float;

 function Start () {

 	if (!fadeType || (fadeType == FadeTypes.None) ) {
 		Debug.LogError("You need to set the fade type on the fade screen object!");
 	}

 	if (!fadeDuration || fadeDuration <= 0.0) {
 		Debug.LogError("You need to set the fade duration on the fade screen object!");
 	}


 	switch (fadeType){
 		case FadeTypes.FadeIn:
			Fade.use.Alpha(guiTexture, 0.0, 1.0, fadeDuration, EaseType.In);
			break;
 		case FadeTypes.FadeOut:
			Fade.use.Alpha(guiTexture, 1.0, 0.0, fadeDuration, EaseType.In);
			break;
 		default:
			Debug.LogError("Unknown fade type!  Please check if you set a fade type on the fade screen object!");
			break;
 	}

 }


#pragma strict

function Start () {
	DelayFadeIn();
}

function DelayFadeIn(){
	//delay the fade in of the logo by waiting a few seconds before turning it on
	//var fadeScript = gameObject.GetComponent('fader');
	yield WaitForSeconds(5);
	gameObject.AddComponent('fader');
}

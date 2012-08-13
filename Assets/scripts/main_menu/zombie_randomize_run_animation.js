#pragma strict

private var _animation : Animation;

function Start () {
	_animation = animation;

	// we randomize the running so all zombies don't look like they're running in sync
	_animation.Stop();

	var randomSeconds : float = Random.Range(0.5, 1);
	yield WaitForSeconds(randomSeconds);
	_animation.Play();

}

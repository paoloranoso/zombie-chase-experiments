using UnityEngine;
using System.Collections;

public class IncreaseScore : MonoBehaviour {
	public int currentScore = 0;
	
	private UITextInstance _scoreText;	
	
	// Use this for initialization
	void Start () {
		// setup our UIText which will parse our .fnt file and allow us to create instances that use it
		var text = new UIText( "prototype", "prototype.png" );

		
		// spawn new text instances showing off the relative positioning features by placing one text instance in each corner
		// Uses default color, scale, alignment, and depth.
		_scoreText = text.addTextInstance( "SCORE: " + currentScore, 0, 0 );
        _scoreText.pixelsFromTopLeft(10,10);
		
		//TODO: commented this out for now, as it hurts iPhone performance greatly...get rid of it or find a better way...
		//InvokeRepeating("IncreaseScoreOverTime", 0, 0.1f);		
	}
	
	
	private void IncreaseScoreOverTime(){
		currentScore+= 1;
		_scoreText.text = "SCORE: " + currentScore;
	}
	
	
	public void AddPoints(int points){
		currentScore += points;
		_scoreText.text = "SCORE: " + currentScore;
	}
	
	public void FinishedLevel(){
//		CancelInvoke();
	}
}

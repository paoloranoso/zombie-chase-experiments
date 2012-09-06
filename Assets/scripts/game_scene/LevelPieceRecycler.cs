using UnityEngine;
using System.Collections;

public class LevelPieceRecycler : MonoBehaviour {
	public GameObject[] levelPieces;

	private int _lastPieceUsedIndex = 0;

	public GameObject GetRandomAvailablePiece(){
		int randomIndex = Random.Range( 0, levelPieces.Length-1 );

		// make sure we just didn't select the already in use piece...keep drawing random numbers if we did
		while ( randomIndex == _lastPieceUsedIndex ){
			randomIndex = Random.Range( 0, levelPieces.Length-1 );
		}

		//Debug.Log("Selected random index:" + randomIndex);


		for (int i = 0; i < levelPieces.Length; i++){
			if ( (i == _lastPieceUsedIndex) || (i == randomIndex) ){
				continue;
			}


			//LEFT OFF HERE (READ BELOW)


			//TODO: instead of turning off/back on (since it causes a spike), perhaps we can just move to a transform far away and above or below the camera
			levelPieces[i].SetActiveRecursively(false);
		}

		_lastPieceUsedIndex = randomIndex;

		//TODO: instead of turning off/back on (since it causes a spike), perhaps we can just move to a transform far away and above or below the camera
		levelPieces[randomIndex].SetActiveRecursively(true);

		return levelPieces[randomIndex];
	}

}

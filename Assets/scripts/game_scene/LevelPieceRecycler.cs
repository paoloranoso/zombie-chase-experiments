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

		Debug.Log("Selected random index:" + randomIndex);

		_lastPieceUsedIndex = randomIndex;

		return levelPieces[randomIndex];
	}

}

using UnityEngine;
using System.Collections;

public class SpawnLevelPiece : MonoBehaviour {

	public Transform spawnPointTransform;

	private LevelPieceRecycler _levelPieceRecyclerScript;
	private GameObject _randomLevelPiece;

	void Awake(){
		_levelPieceRecyclerScript = GameObject.Find("levelPieceRecycler").GetComponent<LevelPieceRecycler>();
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "hero"){
			//_randomLevelPiece is guaranteed to be available (e.g: not current piece in use)
			_randomLevelPiece = _levelPieceRecyclerScript.GetRandomAvailablePiece();
			_randomLevelPiece.transform.position = spawnPointTransform.position;
			_randomLevelPiece.transform.rotation = other.gameObject.transform.rotation;
		}
	}

}

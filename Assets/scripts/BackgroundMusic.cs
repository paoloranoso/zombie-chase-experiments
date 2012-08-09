using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	
	public AudioClip[] musicTracks;
	
	private AudioSource _audioSource;
	
	void Start () {
	_audioSource = GetComponent<AudioSource>();
		
	int randomIndex = Random.Range(0, musicTracks.Length);
	_audioSource.clip = musicTracks[randomIndex];
		
	_audioSource.Play();
		
	}
	
}

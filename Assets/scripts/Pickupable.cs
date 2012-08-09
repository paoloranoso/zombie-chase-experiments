using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {
	
	public AudioClip bulletPickupSound;
	public AudioClip shotgunPickupSound;
	
	private GameObject _gameObject;
	private Transform _transform;

	private GameObject _hero;
	private HeroInventory _heroInventory;
	
	
	void Awake() {
		_gameObject = gameObject;
		_transform = transform;
		
		_hero = GameObject.FindGameObjectWithTag("hero");
		_heroInventory = _hero.GetComponent<HeroInventory>();
	}
	
	
	void Update () {
		_transform.Rotate(Vector3.up * Time.deltaTime * 180);
	}
	
	
	public void OnTriggerEnter(Collider other){		
		if (other.gameObject.tag == "hero") {
	
			if (_gameObject.tag == "shotgun"){
				_heroInventory.PickedUpWeapon(WeaponTypes.shotgun);			
				AudioSource.PlayClipAtPoint(shotgunPickupSound, _transform.position);
			}
	
			if (_gameObject.tag == "bullet"){
				AudioSource.PlayClipAtPoint(bulletPickupSound, _transform.position);
			}
		
			Destroy(gameObject);
		}	
	}
	
}

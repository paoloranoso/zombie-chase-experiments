using UnityEngine;
using System.Collections;

public enum WeaponTypes {pistol=0, shotgun};

public class HeroInventory : MonoBehaviour {
		
	public WeaponTypes currentWeapon = WeaponTypes.pistol;
	private int[] _inventory = new int[10];
	
	private GameObject _shotgunMesh;
	
	// Use this for initialization
	void Start () {
		_shotgunMesh = GameObject.Find("shotguninhand");
	
		_inventory[(int)WeaponTypes.pistol] = 15; //start of with a full clip
		_inventory[(int)WeaponTypes.shotgun] = 0;
		
		// hide all weapon meshes on start except for the pistol
		_shotgunMesh.renderer.enabled = false;
	}
	
	
	public void SetCurrentWeapon(WeaponTypes weapon){
		currentWeapon = weapon;
	}
	
	
	public void PickedUpWeapon(WeaponTypes weapon){
		switch (weapon){
			case WeaponTypes.pistol:
				break; //already have the pistol!
			case WeaponTypes.shotgun:
				_inventory[(int)weapon] = 6;  //add 6 rounds to the shotgun
				_shotgunMesh.renderer.enabled = true; //show the shotgun in hero's hand
				break;
			default:
				break;			
		}
		SetCurrentWeapon(weapon);		
	}
	
	
	public void UsedABullet(){
		//only decrease bullets if we have ammo
		if  ( _inventory[(int)currentWeapon] > 0 ){
			_inventory[(int)currentWeapon]-= 1;
		}
	
		//switch weapon if we are out of ammo and current weapon is not a pistol
		if ( (_inventory[(int)currentWeapon] <= 0) && ( currentWeapon != WeaponTypes.pistol ) ) {
			switch (currentWeapon){
				case WeaponTypes.shotgun:
					_shotgunMesh.renderer.enabled = false; //hide the shotgun again
					break;
				default:
					break;				
			}
			
			// TODO: we may want to do a check in the future to see if they actually have the next best weapon
			currentWeapon-=1;  //out of ammo! switch to the next best thing
		}	
	}
	
	
	public bool CanShoot(){
		bool canShoot = false;
		foreach (int currentWeaponAmmo in _inventory){
			if (currentWeaponAmmo > 0){
				canShoot = true;
				break;
			}
		}
		return canShoot;	
	}
	
	
	public WeaponTypes GetCurrentWeapon(){
		return currentWeapon;
	}
	
	
	public int GetCurrentWeaponDamage(){
		int damage = 0;

		switch (currentWeapon){
			case WeaponTypes.pistol:
				damage = 1; break;
			case WeaponTypes.shotgun:
				damage = 3; break;
			default:
				break;			
		}
		return damage;		
	}
	

	public int[] GetInventory(){
		return _inventory;
	}
	
	
	
	
}

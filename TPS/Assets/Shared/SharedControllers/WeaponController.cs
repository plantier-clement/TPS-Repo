using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	[SerializeField] float weaponSwitchTime;

	Shooter[] weapons;
	int currentWeaponIndex;
	Transform weaponHolster;

	public event System.Action OnWeaponStartSwitch;
	public event System.Action<Shooter> OnWeaponSwitched;
	[HideInInspector] public bool CanFire;

	Shooter m_ActiveWeapon;
	public Shooter ActiveWeapon {
		get {
			return m_ActiveWeapon;
		}
	}


	void Awake(){
		CanFire = true;
		weaponHolster = transform.Find ("Weapons");
		weapons = weaponHolster.GetComponentsInChildren <Shooter> ();
		DeactivateWeapons ();

		if (weapons.Length > 0) 
			EquipWeapon (0);

	}

	internal void SwitchWeapon(int direction){
		if (OnWeaponStartSwitch != null)
			OnWeaponStartSwitch ();

		CanFire = false;
		currentWeaponIndex += direction;

		if (currentWeaponIndex > (weapons.Length - 1))
			currentWeaponIndex = 0;

		if (currentWeaponIndex < 0)
			currentWeaponIndex = weapons.Length - 1;

		GameManager.Instance.Timer.Add (() => {
			EquipWeapon (currentWeaponIndex);
		}, weaponSwitchTime);
	}



	internal void EquipWeapon(int index){
		DeactivateWeapons ();
		m_ActiveWeapon = weapons [index];
		m_ActiveWeapon.Equip ();
		weapons [index].gameObject.SetActive (true);
		CanFire = true;

		if (OnWeaponSwitched != null)
			OnWeaponSwitched (m_ActiveWeapon);

	}

	void DeactivateWeapons(){
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].gameObject.SetActive (false);
			weapons [i].transform.SetParent (weaponHolster);

		}
	}
}

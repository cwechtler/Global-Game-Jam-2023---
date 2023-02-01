using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
	public Animator animator;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Throw");
			//Shoot();
		}
		if (Input.GetButtonDown("Fire2"))
		{
			animator.SetTrigger("IsAttacking");
			//Shoot();
		}
	}

	void Shoot ()
	{
		//animator.ResetTrigger("Throw");
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}

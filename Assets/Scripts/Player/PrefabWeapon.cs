using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	void Shoot ()
	{
		//animator.ResetTrigger("Throw");
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}

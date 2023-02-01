using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAxe : MonoBehaviour
{
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject bulletPrefab;

	void Shoot()
	{
		//animator.ResetTrigger("Throw");
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Axe : MonoBehaviour
{
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private int meleDamage = 60;

	GameObject enemy;

	//Called in animation by an event
	void Shoot()
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}

	//Called in animation by an event
	void Hit()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		if (enemy != null)
		{
			float dist = Vector3.Distance(enemy.transform.position, transform.position);
			print("Distance to other: " + dist);
			if (dist <= 2.5)
			{
				BossHealth enemyHealth = enemy.GetComponent<BossHealth>();
				enemyHealth.TakeDamage(meleDamage);

				print("Hit");
			}
		}
	}
}

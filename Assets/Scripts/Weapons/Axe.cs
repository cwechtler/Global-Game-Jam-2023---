using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Axe : MonoBehaviour
{
	[SerializeField] private Transform firePoint;
	[SerializeField] private Transform attackPoint;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float attackRange = .8f;
	[SerializeField] private int meleDamage = 60;
	[SerializeField] private LayerMask enemyLayers;

	GameObject enemy;

	//Called in Throw animation by an event
	void Shoot()
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}

	//Called in Attack animation by an event
	void Hit()
	{
		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
		if (colInfo != null)
		{
			colInfo.GetComponent<EnemyHealth>().TakeDamage(meleDamage);
			//GameController.instance.Score += 1;
			//Instantiate(impactEffect, transform.position, transform.rotation);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}

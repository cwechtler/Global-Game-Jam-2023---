using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

	[SerializeField] private int health = 500;
	[SerializeField] private GameObject deathEffect;
	[SerializeField] private bool isBoss;

	private bool isInvulnerable = false;

	public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }
	public int Health { get => health; set => health = value; }

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		Health -= damage;
	
		if (Health <= 300 && isBoss)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (Health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}

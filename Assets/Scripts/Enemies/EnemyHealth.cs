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
	public bool IsBoss { get => isBoss; set => isBoss = value; }

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		Health -= damage;
	
		if (Health <= 400 && IsBoss)
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
		Vector3 spawnPosition = new Vector3(transform.position.x, (transform.position.y + 1f), transform.position.z);
		Instantiate(deathEffect, spawnPosition, Quaternion.identity);

		if (IsBoss)
		{
			Destroy(GetComponent<Animator>());
			Destroy(GetComponent<Collider2D>());
			GetComponent<BossWeapon>().DestroyRootAttack();	

			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
			StartCoroutine(LevelManager.instance.LoadLevel("Credits", 2f));
		}
		else {
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RootAttack : MonoBehaviour
{
	[SerializeField] private int damage = 10;

	private bool isTriggered = false;
	private float damageTimer = .5f;

	private void Update()
	{
		if (damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
		}
	}
	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
		if (player != null && !isTriggered)
		{
			isTriggered = true;
			player.TakeDamage(damage);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		PlayerHealth player = collision.GetComponent<PlayerHealth>();
		if (player != null && damageTimer <= 0)
		{
			damageTimer = .5f;
			player.TakeDamage(5);
		}
	}
}

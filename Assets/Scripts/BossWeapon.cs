using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public float attackDistance = 5f;
	public float minRootAttackRange = 10f;
	public float maxRootAttackRange = 20f;
	public GameObject rootAttackPrefab;
	public LayerMask attackMask;

	private GameObject attackRoots;
	private Animator attackRootsAnimator;

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
		
	}

	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}

	// Called from Event in Gnarlwood_RootAttack animation
	public void RootAttack() 
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 spawnPosition = new Vector3(player.transform.position.x, 0, 0);
		attackRoots = Instantiate(rootAttackPrefab, spawnPosition, Quaternion.identity);
		attackRootsAnimator = attackRoots.GetComponentInChildren<Animator>();
	}

	// Called from Event in Gnarlwood_RootAttack animation
	public void ReverseAnimation() 
	{
		if (attackRoots != null) {
			attackRootsAnimator.SetFloat("Speed", -2);
		}
	}

	// Called from Event in Gnarlwood_RootAttack animation
	public void DestroyRootAttack()
	{
		if (attackRoots != null) {
			Destroy(attackRoots);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}

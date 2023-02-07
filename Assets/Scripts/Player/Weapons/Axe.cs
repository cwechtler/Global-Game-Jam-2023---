using UnityEngine;

public class Axe : MonoBehaviour
{
	[SerializeField] private Transform firePoint;
	[SerializeField] private Transform attackPoint;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float attackRange = .8f;
	[SerializeField] private int meleDamage = 60;
	[SerializeField] private LayerMask enemyLayers;

	//Called in Hero_Throw animation by an event
	void Shoot()
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}

	//Called in Hero_Attack animation by an event
	void Hit()
	{
		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
		if (colInfo != null)
		{
			EnemyHealth enemyHealth = colInfo.GetComponent<EnemyHealth>();
			if (enemyHealth != null) { 
				enemyHealth.TakeDamage(meleDamage);
			}
			SoundManager.instance.PlaySwingAxeImpactClip();
			//Instantiate(impactEffect, transform.position, transform.rotation);
		}
	}

	//Called in Hero_Run animation by an event
	void PlayWalkSound() {
		SoundManager.instance.PlayWalkClip();
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private CanvasController canvasController;
	public int health = 100;

	public GameObject deathEffect;
	public bool isDead = false;

	public void TakeDamage(int damage)
	{
		health -= damage;
		canvasController.UpdateHealthBar();
		StartCoroutine(DamageAnimation());

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	void Die()
	{
		isDead = true;	
		SoundManager.instance.PlayPlayerDeathClip();
		StartCoroutine(LevelManager.instance.LoadLevel("Credits", 2.5f));
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

}

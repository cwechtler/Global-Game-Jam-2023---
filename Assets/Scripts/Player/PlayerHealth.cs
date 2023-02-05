using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private CanvasController canvasController;
	public int health = 100;

	public GameObject deathEffect;
	public bool isDead = false;
	public CameraShake cameraShake;

	public void TakeDamage(int damage)
	{
		health -= damage;
		canvasController.UpdateHealthBar();
		if (!isDead) {
			SoundManager.instance.PlayHurtClip();
			StartCoroutine(DamageAnimation());
			StartCoroutine(cameraShake.Shake(.15f, .1f));
		}

		if (health <= 0 && !isDead)
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		isDead = true;	
		SoundManager.instance.PlayPlayerDeathClip();
		yield return new WaitForSeconds(.2f);

		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		foreach (SpriteRenderer sr in srs)
		{
			Color c = sr.color;
			c.a = 0;
			sr.color = c;
		}

		StartCoroutine(LevelManager.instance.LoadLevel("Credits", 2f));
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			if (isDead)
				break;
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

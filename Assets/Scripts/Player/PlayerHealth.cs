using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private CanvasController canvasController;
	[SerializeField] private GameObject deathEffect;
	[SerializeField] private GameObject hero;

	public int Health { get => health; set => health = value; }
	public bool IsDead { get => isDead; set => isDead = value; }

	private int health = 100;
	private bool isDead = false;

	public void TakeDamage(int damage)
	{
		Health -= damage;
		canvasController.UpdateHealthBar();
		if (!IsDead) {
			SoundManager.instance.PlayHurtClip();
			StartCoroutine(DamageAnimation());
			StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.15f, .1f));
		}

		if (Health <= 0 && !IsDead)
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		IsDead = true;	
		SoundManager.instance.PlayPlayerDeathClip();
		yield return new WaitForSeconds(.2f);
		Destroy(hero);

		StartCoroutine(LevelManager.instance.LoadLevel("Credits", 2f));
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			if (IsDead)
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

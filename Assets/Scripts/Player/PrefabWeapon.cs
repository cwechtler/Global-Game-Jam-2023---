using UnityEngine;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;

	private Animator animator;

	void Start ()
	{
		animator = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (animator != null)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				animator.SetTrigger("Throw");
				SoundManager.instance.PlayShootClip();
			}
			if (Input.GetButtonDown("Fire2"))
			{
				animator.SetTrigger("IsAttacking");
				SoundManager.instance.PlaySwingAxeClip();
			}
		}
	}

	// Called from Event in Hero_Attack animation
	void Shoot ()
	{
		//animator.ResetTrigger("Throw");
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}

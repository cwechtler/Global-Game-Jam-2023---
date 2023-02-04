using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;
	public int throwingAxeRange = 30;

	private GameObject playerGo;

	// Use this for initialization
	void Start () {
		rb.velocity = transform.right * speed;
		playerGo = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		float playerDistance = Vector2.Distance(playerGo.transform.position, rb.position);
		if (playerDistance >= throwingAxeRange) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		EnemyHealth enemy = hitInfo.GetComponentInParent<EnemyHealth>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
			SoundManager.instance.PlayAxeImpactClip();
			GameController.instance.Score += 1;
		}

		//Instantiate(impactEffect, transform.position, transform.rotation);
		if (!hitInfo.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}

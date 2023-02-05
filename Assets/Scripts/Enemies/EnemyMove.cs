using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
	[SerializeField] private int damage = 5;
	[SerializeField] private float speed = 2f;
	[SerializeField] private Transform patrolPointA;
	[SerializeField] private Transform patrolPointB;

    private Rigidbody2D rb;
	private bool isFlipped = false;
	private Transform patrolTarget;

	Vector2 target;
	Vector2 newPos;
	float dist;
	bool flipped = false;	

	// Start is called before the first frame update
	void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
		patrolTarget = patrolPointA;
		dist = Mathf.Abs(transform.position.x - patrolTarget.transform.position.x);
	}

    // Update is called once per frame
    void Update()
    {
		LookAtPlayer();
		dist = Mathf.Abs(transform.position.x - patrolTarget.transform.position.x);

		if (dist < .01f)
		{
			if (!flipped) {
				patrolTarget = patrolPointA;
				flipped = true;
			}
			else
			{
				patrolTarget = patrolPointB;
				flipped = false;
			}
		}

		target = new Vector2(patrolTarget.position.x, rb.position.y);
		newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);
	}

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > patrolTarget.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < patrolTarget.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Player")) {
			print(collision.gameObject);
			collision.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			print(collision.gameObject);
			//collision.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
	}
}

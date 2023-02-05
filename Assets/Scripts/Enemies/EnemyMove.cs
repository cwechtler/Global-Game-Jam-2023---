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
	private float dist;
	private bool patrolPointFlipped = false;	

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
		SwitchPatrolPoints();
		LookAtPatrolTarget();

		Vector2 target = new Vector2(patrolTarget.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);
	}

	private void SwitchPatrolPoints()
	{
		dist = Mathf.Abs(transform.position.x - patrolTarget.transform.position.x);

		if (dist < .01f)
		{
			if (!patrolPointFlipped)
			{
				patrolTarget = patrolPointA;
				patrolPointFlipped = true;
			}
			else if (patrolPointFlipped)
			{
				patrolTarget = patrolPointB;
				patrolPointFlipped = false;
			}
		}
	}

	public void LookAtPatrolTarget()
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
			collision.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
	}
}

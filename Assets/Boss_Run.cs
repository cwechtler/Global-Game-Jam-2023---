using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

	public float speed = 2.5f;
	public float attackRange = 3f;
	public float timeRemaining = 0;

	Transform player;
	public Rigidbody2D rb;
	Boss boss;
	BossWeapon bossWeapon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		boss = animator.GetComponent<Boss>();
		bossWeapon = animator.GetComponent<BossWeapon>();
		SoundManager.instance.PlayBossWalkClip();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		if (player.GetComponent<PlayerHealth>().isDead) {
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsAttacking", false);
			return;
		}

		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
			
		}

		boss.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);
		float playerDistance = Vector2.Distance(player.position, rb.position);

		if (playerDistance <= attackRange)
		{
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsAttacking", true);
			animator.SetTrigger("Swing");
		}

		if (playerDistance >= bossWeapon.minRootAttackRange && playerDistance <= bossWeapon.maxRootAttackRange && timeRemaining <= 0)
		{
			GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
			CharacterController2D characterController2D = playerGo.GetComponent<CharacterController2D>();
			if (characterController2D.m_Grounded)
			{
				timeRemaining = Random.Range(0, 5);
				animator.SetBool("IsWalking", false);
				animator.SetBool("IsAttacking", true);
				animator.SetTrigger("RootAttack");
			}
		}

		if (GameController.instance.isInBossZone)
		{
			animator.SetBool("IsWalking", false);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Swing");
		animator.ResetTrigger("RootAttack");
	}
}

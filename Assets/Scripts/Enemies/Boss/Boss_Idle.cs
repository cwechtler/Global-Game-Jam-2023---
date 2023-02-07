using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
	Transform player;
	Rigidbody2D rb;
	BossWeapon bossWeapon;
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		bossWeapon= animator.GetComponent<BossWeapon>();
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if (player.GetComponent<PlayerHealth>().IsDead)
		{
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsAttacking", false);
			return;
		}

		if (GameController.instance.isInBossZone)
		{
			if (Vector2.Distance(player.position, rb.position) > bossWeapon.attackDistance)
			{
				animator.SetBool("IsWalking", true);
			}
			else if (Vector2.Distance(player.position, rb.position) <= bossWeapon.attackDistance)
			{
				animator.SetBool("IsAttacking", true);
				animator.SetTrigger("Swing");
			}
		}
		else
		{
			animator.SetBool("IsWalking", false);
		}
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
}

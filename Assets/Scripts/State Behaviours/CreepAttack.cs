using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreepAttack : StateMachineBehaviour
{
    Creep creep;
    public NavMeshAgent agent;
    int damage;
    Damager damager; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creep = animator.gameObject.GetComponent<Creep>();

        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        damager = animator.gameObject.GetComponent<Damager>();

        animator.speed = creep.attackTime;
       
        creep.gameObject.GetComponent<Damager>().DamageTarget();

        if (agent != null)
        {
            if (agent.enabled == true)
            {
                if (agent.isOnNavMesh)
                {
                    agent.isStopped = true;
                }
            }
            
        }
   
        creep.FaceTarget();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;
        if (agent.enabled = true)
        {
            if (agent.isOnNavMesh)
            {
                agent.isStopped = false;

            }
        }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

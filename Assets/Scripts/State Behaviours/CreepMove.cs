using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreepMove : StateMachineBehaviour
{
    Creep creep;
    public NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creep =  animator.gameObject.GetComponent<Creep>();
       
        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (creep!= null && agent != null)
        {
            //If there is a current Enemy target, prioritize chasing it
            if (creep.currentEnemyTarget != null)
            {
                agent.SetDestination(creep.currentEnemyTarget.transform.position);
            }
            else//Else if there is no enemy, go to path
            {
                animator.SetFloat("targetDistance", agent.remainingDistance);
                if (creep.currentPath != null)
                {
                    agent.SetDestination(creep.currentPath.position);
                }

                if (agent.remainingDistance >= agent.stoppingDistance)
                {
                    //If minion is on its way


                }
                else
                {
                    //if minion arrived at destination
                    if (animator.GetInteger("pathCount") + 1 < creep.paths.Count)
                    {
                        animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                        creep.currentPath = creep.paths[animator.GetInteger("pathCount")].transform;
                    }

                }
            }
        }
        


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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

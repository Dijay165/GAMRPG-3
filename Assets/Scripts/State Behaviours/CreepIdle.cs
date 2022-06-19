using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreepIdle : StateMachineBehaviour
{
    Creep creep;
    public NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creep = animator.gameObject.GetComponent<Creep>();

        agent = animator.gameObject.GetComponent<NavMeshAgent>();


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (creep != null && agent != null)
        {
            //If there is a current Enemy target, prioritize chasing it
            if (creep.currentTarget != null)
            {
                agent.SetDestination(creep.currentTarget.transform.position);
            }
            else//Else if there is no enemy, go to path
            {
                animator.SetFloat("targetDistance", agent.remainingDistance);
                if (creep.currentPath != null)
                {
                    agent.SetDestination(creep.currentPath.position);
                }
            }
            if (animator.GetBool("isFollowingPath") && agent.remainingDistance <= 40 && agent.pathPending == false)
            {
                if (animator.GetInteger("pathCount") + 1 < creep.paths.Count)
                {

                    animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                    creep.currentPath = creep.paths[animator.GetInteger("pathCount")].transform;
                    agent.SetDestination(creep.paths[animator.GetInteger("pathCount")].transform.position);
                    animator.SetFloat("targetDistance", agent.remainingDistance);
    
                }
            }
        }

    }
}

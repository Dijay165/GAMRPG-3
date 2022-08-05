using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreepMove : StateMachineBehaviour
{
    Creep creep;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creep = animator.gameObject.GetComponent<Creep>();
        NavMeshAgent agent = creep.agent;

        creep.obstacle.enabled = false;
        if (agent.isOnNavMesh)
        {
            agent.enabled = true;
        }
        else
        {
            agent.Warp(new Vector3(agent.transform.position.x, 0f, agent.transform.position.z));
            agent.enabled = false;
            agent.enabled = true;
        }
    
        if (creep.destination != null)
        {
            if (agent.isOnNavMesh == false)
            {
                agent.Warp(new Vector3(agent.transform.position.x, 0f, agent.transform.position.z));
                agent.enabled = false;
                agent.enabled = true;
            }
            if (agent.isOnNavMesh == true)
            {
                agent.SetDestination(creep.destination.position);

            }

        }
  

    }


   
}

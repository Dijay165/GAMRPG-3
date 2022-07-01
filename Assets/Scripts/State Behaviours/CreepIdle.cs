using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreepIdle : StateMachineBehaviour
{
    Creep creep;
 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creep = animator.gameObject.GetComponent<Creep>();
        NavMeshAgent agent = creep.agent;
  
        agent.enabled = false;
        creep.obstacle.enabled = true;

      
    }

}

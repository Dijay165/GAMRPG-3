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

        agent.enabled = true;
        creep.obstacle.enabled = false;

        //if (creep.runningUpdateTarget != null)
        //{
        //    creep.StopCoroutine(creep.runningUpdateTarget);
        //}
        //creep.runningUpdateTarget = creep.Co_Detection();
        //creep.StartCoroutine(creep.runningUpdateTarget);

    }

}

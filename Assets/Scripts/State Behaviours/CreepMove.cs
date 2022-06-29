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
        agent.enabled = true;
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
  
        //if (creep.runningUpdateTarget != null)
        //{
        //    creep.StopCoroutine(creep.runningUpdateTarget);
        //    creep.runningUpdateTarget = null;
        //}
        //creep.runningUpdateTarget = creep.Co_Detection();
        //creep.StartCoroutine(creep.runningUpdateTarget);

        //if (creep.runningUpdateDestination != null)
        //{
        //    Debug.Log("Disable");
        //    creep.StopCoroutine(creep.runningUpdateDestination);
        //    creep.runningUpdateDestination = null;
        //}
        //if (creep.currentTarget != null)
        //{

        //    creep.runningUpdateDestination = FollowTarget(creep.destination);
        //    creep.StartCoroutine(creep.runningUpdateDestination);
        //}


    }

    //private IEnumerator FollowTarget(Transform target)
    //{
    //    Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);
    //    while (Vector3.SqrMagnitude(creep.transform.position - target.position) > 0.1f)
    //    {
    //        // did target move more than at least a minimum amount since last destination set?
    //        if (Vector3.SqrMagnitude(previousTargetPosition - target.position) > 0.1f)
    //        {
    //            agent.SetDestination(target.position);
    //            previousTargetPosition = target.position;
    //        }
    //        yield return new WaitForSeconds(10f);
    //    }
    //    yield return null;
    //}
  

   
}

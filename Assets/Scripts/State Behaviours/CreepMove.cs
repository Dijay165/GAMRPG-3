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
        agent.enabled = true;
        creep.obstacle.enabled = false;

        if (creep.runningUpdateTarget != null)
        {
            creep.StopCoroutine(creep.runningUpdateTarget);
        }
        creep.runningUpdateTarget = creep.Co_Detection();
        creep.StartCoroutine(creep.runningUpdateTarget);

        if (creep.runningUpdateDestination != null)
        {
            creep.StopCoroutine(creep.runningUpdateDestination);
        }
        creep.runningUpdateDestination = FollowTarget(creep.currentTarget.transform);
        creep.StartCoroutine(creep.runningUpdateDestination);

    }

    public void UpdateDestination(Animator animator)
    {
        if (creep != null && agent != null)
        {
            //If there is a current Enemy target, prioritize chasing it
            if (creep.currentTarget != null)
            {
                if (agent.enabled)
                {
                    agent.SetDestination(creep.currentTarget.transform.position);
                }


            }
            else//Else if there is no enemy, go to path
            {
                //animator.SetFloat("targetDistance", agent.remainingDistance);
      
                if (creep.currentPath != null)
                {
                    if (agent.enabled)
                    {
                        agent.SetDestination(creep.currentPath.position);
                    }

                }

                //if (animator.GetFloat("targetDistance") >= 20)
                //{
                //    //If minion is on its way


                //}
                //else
                //{
                //    //if minion arrived at destination
                //    if (animator.GetBool("isFollowingPath"))
                //    {
                //        if (animator.GetInteger("pathCount") + 1 < creep.paths.Count)
                //        {
                //            Debug.Log("Test");
                //            animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                //            creep.currentPath = creep.paths[animator.GetInteger("pathCount")].transform;
                //        }
                //    }


                //}
            }
        }
    }
    private IEnumerator FollowTarget(Transform target)
    {
        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);
        while (Vector3.SqrMagnitude(creep.transform.position - target.position) > 0.1f)
        {
            // did target move more than at least a minimum amount since last destination set?
            if (Vector3.SqrMagnitude(previousTargetPosition - target.position) > 0.1f)
            {
                agent.SetDestination(target.position);
                previousTargetPosition = target.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (creep != null && agent != null)
        {


        }
    }

   
}

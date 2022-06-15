using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Creep : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform currentPath;
    public List<GameObject> paths;

    public int layerMask = 1 << 8;

    public float detectionRadius;
    public float attackRadius;

    public float attackRate;

    public Health currentEnemyTarget;

    public Animator animator;
    public List<Health> enemies = new List<Health>();


 
    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        animator = GetComponent<Animator>();


    }

    private void OnEnable()
    {
        StartCoroutine(Co_Detection());
    }

    
    
    IEnumerator Co_Detection()
    {
        yield return new WaitForSeconds(1f);
        CheckEnemyTargetStatus();
        StartCoroutine(Co_Detection());
    }

    void DetectEnemies()
    {

        enemies.Clear();
        //Detected enemy list
        //Debug.Log("IT WORKS");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            //if enemy is within its detection radius, it sees enemy

            if (hitCollider.gameObject.GetComponent<Health>())
            {
                //Debug.Log("CREEP DETECTED " + hitCollider.gameObject.name);
                Health hitHealth = hitCollider.gameObject.GetComponent<Health>();
                //check if it is already in target list, if not in target list, add it

                if (hitHealth.invulnerable == false) //add it if it isnt invulnerable
                {

                    if (!hitHealth.CompareTeam(gameObject.GetComponent<Health>().team))
                    {
                       
                        enemies.Add(hitHealth);

                        //organize
                        if (enemies.Count > 2)
                        {
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                float newEnemyDistance = Vector3.Distance(enemies[i].transform.position, transform.position);
                                for (int si = 1; si < enemies.Count - 1; si++)
                                {
                                    float currentEnemyIndexDistance = Vector3.Distance(enemies[si].transform.position, transform.position);
                                    if (newEnemyDistance > currentEnemyIndexDistance) //if the newly detected enemy's distance is further away than the current index of enemy in list, move to the next enemy in the list
                                    {
                                        Health saved = enemies[i];
                                        enemies[i] = enemies[si];
                                        enemies[si] = saved;
                                    }
                                }
                            }
                        }

                    }


                }



            }


        }

        //else       //if enemy is not within its detection radius, it does not see enemy
        //{

        //}


    }


    void CheckEnemyTargetStatus()
    {
        //check if current target still within range
        if (currentEnemyTarget != null)
        {
            //check current target if its still within detection radius
            if (Vector3.Distance(currentEnemyTarget.transform.position, transform.position) < detectionRadius) //if within attack radius
            {
                //check if current target is within attack radius
                if (Vector3.Distance(currentEnemyTarget.transform.position, transform.position) < attackRadius) //if within attack radius
                {
                    animator.SetBool("isAttacking", true);

                    //it will start attacking
                }
                else
                {
                    //it isnt within attack radius
                    animator.SetBool("isAttacking", false);

                }
            }
            else // not within detection radius anymore
            {
                NewCurrentTarget();
            }


        }
        else //if there is no current enemy target, check if there is a next target in enemy list
        {
            NewCurrentTarget();
        }
    }

    void NewCurrentTarget(Health objectHealth = null)
    {
        //old enemy target that just died / moved out of detection range
        animator.SetBool("isAttacking", false);
        currentEnemyTarget = null;
        if (currentEnemyTarget != null)
        {
            currentEnemyTarget.OnDeathEvent.RemoveListener(NewCurrentTarget);
        }

        DetectEnemies();
        if (enemies.Count > 0)
        {
            //Because the list was organizest nearest to furthest before coming here, we'll just choose the next index
            currentEnemyTarget = enemies[0];
            currentEnemyTarget.OnDeathEvent.AddListener(NewCurrentTarget);

            //enemies.RemoveAt(0);
        }
        else
        {
            currentEnemyTarget = null;
        }
    }

}

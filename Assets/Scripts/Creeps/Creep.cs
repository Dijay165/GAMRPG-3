using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class Creep : Unit
{
    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;

    [SerializeField] private float carvingMoveThreshold = 0.1f;

    private float lastMoveTime;
    private Vector3 lastPosition;
    public Transform currentPath;
    public List<GameObject> paths;

    public int layerMask = 1 << 8;

    public float detectionRadius;
    public float attackRadius;

    public float attackRate;

    public IEnumerator runningUpdateDestination;
    public IEnumerator runningUpdateTarget;
    //public Health currentEnemyTarget;

    public Animator animator;
    public List<Health> enemies = new List<Health>();


    // Start is called before the first frame update

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
            obstacle = GetComponent<NavMeshObstacle>();
    }
    protected override void Start()
    {
        base.Start();

    

       
       obstacle.carveOnlyStationary = false;
        obstacle.carving = true;

        agent.updatePosition = true;
        agent.updateRotation = false;
        animator = GetComponent<Animator>();

        //if (health != null)
        //{
        //    health.OnDeathEvent.AddListener(UnitDeath);

        //    // Debug.Log("Listen");
        //}
            


    }

    private void OnEnable()
    {
       // StartCoroutine(Co_Detection()); 
    }

    protected override void InitializeValues()
    {
        base.InitializeValues();
        obstacle.enabled = false;
        agent.enabled = true;

    }
    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
        obstacle.enabled = false;
        agent.enabled = false;
    }
    public IEnumerator Co_Detection()
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
                    
                    if (!hitHealth.CompareTeam(team))
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
        if (currentTarget != null)
        {
            //check current target if its still within detection radius
            if (Vector3.Distance(currentTarget.transform.position, transform.position) < detectionRadius) //if within attack radius
            {
             
                //check if current target is within attack radius
                if (Vector3.Distance(currentTarget.transform.position, transform.position) < attackRadius) //if within attack radius
                {

                    animator.SetTrigger("isAttacking");
                    animator.SetBool("isFollowingPath",false);
                    //it will start attacking
                }
                else
                {
                    //it isnt within attack radius

                    animator.SetTrigger("isIdle");
                    animator.SetBool("isFollowingPath", true);

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
       
        currentTarget = null;
        if (currentTarget != null)
        {
            currentTarget.OnDeathEvent.RemoveListener(NewCurrentTarget);
        }

        DetectEnemies();
        if (enemies.Count > 0)
        {
            //Because the list was organizest nearest to furthest before coming here, we'll just choose the next index
            currentTarget = enemies[0];
            currentTarget.OnDeathEvent.AddListener(NewCurrentTarget);
            animator.SetBool("isFollowingPath", false);
            animator.SetTrigger("isAttacking");


            //enemies.RemoveAt(0);
        }
        else
        {
            currentTarget = null;
            animator.SetTrigger("isIdle");
            animator.SetBool("isFollowingPath", true);
            
            if (agent != null)
            {
                //If there is a current Enemy target, prioritize chasing it
                if (currentTarget != null)
                {
                    agent.SetDestination(currentTarget.transform.position);
                }
                else//Else if there is no enemy, go to path
                {
                    //animator.SetFloat("targetDistance", agent.remainingDistance);
                    animator.SetTrigger("isMoving");
                    if (currentPath != null)
                    {
                        agent.SetDestination(currentPath.position);
                    }
                }
                if (animator.GetBool("isFollowingPath") && agent.remainingDistance <= attackRadius && agent.pathPending == false)
                {
                    if (animator.GetInteger("pathCount") + 1 < paths.Count)
                    {

                        animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                        currentPath = paths[animator.GetInteger("pathCount")].transform;
                        agent.SetDestination(paths[animator.GetInteger("pathCount")].transform.position);
                        //animator.SetFloat("targetDistance", agent.remainingDistance);
                        animator.SetTrigger("isMoving");

                    }
                }
            }
        }
    }

    

}

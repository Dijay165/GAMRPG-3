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
    public Transform currentWaypoint;
    public Transform destination;
    public List<GameObject> waypoints;

    public int layerMask = 1 << 8;

    public float detectionRadius;
    public float attackRadius;

    public float attackRate;

    public IEnumerator runningUpdateDestination;
    //public IEnumerator runningUpdateEnemyTarget;
    public IEnumerator runningUpdateWaypoint;
    //public Health currentEnemyTarget;

    public Animator animator;
    public List<Health> enemies = new List<Health>();

    public float test;
    // Start is called before the first frame update

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
            obstacle = GetComponent<NavMeshObstacle>();
    }
    protected override void Start()
    {
        
    

    

       
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

        base.Start();

    }

    private void OnEnable()
    {
       // StartCoroutine(Co_Detection()); 
    }

    protected override void InitializeValues()
    {
        base.InitializeValues();
        //obstacle = GetComponent<NavMeshObstacle>();
        //agent = GetComponent<NavMeshAgent>();
        obstacle.enabled = false;
        agent.enabled = true;
        if (agent.isOnNavMesh == false)
        {
            agent.Warp(new Vector3(transform.position.x, 0f,transform.position.z));
            agent.enabled = false;
            agent.enabled = true;
        }
       
        //transform.position.x, 0.5f,transform.position.zagent.Warp(new Vector3(transform.position.x, 0.5f,transform.position.z));
        StartCoroutine(Co_Load());

    }

    IEnumerator Co_Load()
    {
        if (agent.isOnNavMesh == true)
        {
           // Debug.Log("true");
            //agent.SetDestination(paths[animator.GetInteger("pathCount")].transform.position);
        }
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("isIdle");
        if (runningUpdateDestination != null)
        {
            StopCoroutine(runningUpdateDestination);
            runningUpdateDestination = null;
        }
        runningUpdateDestination = Co_Detection();
        StartCoroutine(runningUpdateDestination);

        if (runningUpdateWaypoint != null)
        {
            StopCoroutine(runningUpdateWaypoint);
            runningUpdateWaypoint = null;
        }
        runningUpdateWaypoint = Co_UpdateWaypoint();
        StartCoroutine(runningUpdateWaypoint);

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
        if (runningUpdateDestination != null)
        {
            StopCoroutine(runningUpdateDestination);
            runningUpdateDestination = null;
        }
        runningUpdateDestination = Co_Detection();
        StartCoroutine(runningUpdateDestination);
  
    }

    bool Condi()
    {
        Vector2 t = new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z);
        Vector2 cp = new Vector2(transform.position.x, transform.position.z);
        if (Vector2.Distance(t, cp) <= detectionRadius) //if within attack radius
        {
            return true;
        }
        else
        {
            return false;
        }
           
    }

    void CheckDistanceFromTarget()
    {
        //check current target if its still within detection radius
        Vector2 t = new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z);
        Vector2 cp = new Vector2(transform.position.x, transform.position.z);
        
        if (currentTarget != null)
        {
            //check if current target is within attack radius
            if (Vector2.Distance(t, cp) <= attackRadius) //if within attack radius
            {

                //  Debug.Log(gameObject.name + " - " + currentTarget.gameObject.name);
               // Debug.Log(t + " - " + cp + " - " + " - " + attackRadius + gameObject.name + " isAttacking");
                animator.SetTrigger("isAttacking");
                animator.SetBool("isFollowingPath", false);
                //it will start attacking
            }
            else
            {
                //it isnt within attack radius
                destination = currentTarget.transform;
                animator.SetTrigger("isMoving");

                //animator.SetBool("isFollowingPath", true);

            }
        }
        else
        {
           
            if (destination != null)
            {
                //Debug.Log("EMERGENCY");
                animator.SetTrigger("isMoving");
            }
         
        }
       
       
    }

    void CheckEnemyTargetStatus()
    {
        //check if current target still within range
        if (currentTarget != null)
        {
            if (Condi() == true) //if within attack radius
            {
                CheckDistanceFromTarget();
            }
       
        
            else if (Condi() == false)// not within detection radius anymore
            {
                NewCurrentTarget();
            }

        }
        else //if there is no current enemy target, check if there is a next target in enemy list
        {
            NewCurrentTarget();
        }
    }

    void DeregisterDeath(Health objectHealth = null)
    {
        objectHealth.OnDeathEvent.RemoveListener(NewCurrentTarget);
        NewCurrentTarget();
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
            if (hitCollider.gameObject != gameObject)
            {
                if (hitCollider.gameObject.GetComponent<Health>()) 
                {
                   // Debug.Log(gameObject.name + " - " + hitCollider.gameObject.name);
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
                                    Vector2 eo = new Vector2(enemies[i].transform.position.x,enemies[i].transform.position.z);
                                    Vector2 cpo = new Vector2(transform.position.x, transform.position.z);
                                    float newEnemyDistance = Vector2.Distance(eo, cpo);
                                    for (int si = 1; si < enemies.Count - 1; si++)
                                    {
                                        Vector2 et = new Vector2(enemies[si].transform.position.x, enemies[si].transform.position.z);
                                    
                                        float currentEnemyIndexDistance = Vector2.Distance(et, cpo);
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
            


        }

        //else       //if enemy is not within its detection radius, it does not see enemy
        //{

        //}


    }

    void NewCurrentTarget(Health objectHealth = null)
    {
        //old enemy target that just died / moved out of detection range

        currentTarget = null;
        DetectEnemies();
       // Debug.Log(gameObject.name + "  phase 1a");
        if (enemies.Count > 0) // AT LEAST 1 ENEMY FOUND
        {
            //Because the list was organizest nearest to furthest before coming here, we'll just choose the next index
            currentTarget = enemies[0];
            currentTarget.OnDeathEvent.AddListener(DeregisterDeath);
           // Debug.Log(gameObject.name + "  phase 2aD");
            CheckDistanceFromTarget();
            //CheckDistanceFromTarget();
            //animator.SetBool("isFollowingPath", false);
            //animator.SetTrigger("isAttacking");


            //enemies.RemoveAt(0);
        }
        else // NO ENEMIES FOUND
        {
            //currentTarget = null;

            //animator.SetBool("isFollowingPath", true);
           // Debug.Log(gameObject.name + "  phase 1b");
            if (agent != null)
            {
              
                //test = agent.remainingDistance; //DELETE
                //If there is a current Enemy target, prioritize chasing it
                //if (currentTarget != null)
                //{

                //    destination = currentPath;
                //    //agent.SetDestination(currentPath.position);
                //    animator.SetTrigger("isMoving");
                //    //agent.SetDestination(currentTarget.transform.position);
                //}
                //else//Else if there is no enemy, go to path
                //{
                //animator.SetFloat("targetDistance", agent.remainingDistance);

                if (currentWaypoint != null)
                {
                  //  Debug.Log(gameObject.name + "  phase 2a");
                    if (agent.isOnNavMesh == false)
                    {
                        //Debug.Log("false");
                        agent.Warp(new Vector3(transform.position.x, 0f, transform.position.z));
                        agent.enabled = false;
                        agent.enabled = true;
                 
                    }
                    if (agent.isOnNavMesh == true)
                    {
                        test = agent.remainingDistance; //DELETE
                      //  Debug.Log(gameObject.name + " NO TARGETS THUS FOLLOWING PATH true");
                        destination = currentWaypoint;
                        //agent.SetDestination(currentPath.position);
                        animator.SetBool("isFollowingPath", true);
                        animator.SetTrigger("isMoving");
                    }
                       
                }
                else
                {
                   // Debug.Log(gameObject.name + "  phase 2b");
                    if (destination != null)
                    {
                        animator.SetTrigger("isMoving");
                    }
                    else
                    {
                       // Debug.Log(gameObject.name + " ISIDLING");
                        animator.SetTrigger("isIdle");
                    }
                       
                }

                //}
               
            }
        }
    }

    public IEnumerator Co_UpdateWaypoint()
    {
        //if (animator.GetBool("isFollowingPath")) //&& agent.remainingDistance <= attackRadius
        //{
            //Debug.Log(gameObject.name + "  TEST");
            Vector2 currPath = new Vector2(currentWaypoint.transform.position.x,currentWaypoint.transform.position.z);
            Vector2 currPos = new Vector2(transform.position.x, transform.position.z);
            if (Vector2.Distance(currPath, currPos) < 110f)
            {
                if (animator.GetInteger("pathCount") + 1 < waypoints.Count)
                {

                    animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                    currentWaypoint = waypoints[animator.GetInteger("pathCount")].transform;
                    //if (agent.isOnNavMesh == false)
                    //{
                    //    agent.Warp(new Vector3(transform.position.x, 0f, transform.position.z));
                    //    agent.enabled = false;
                    //    agent.enabled = true;
                    //}
                    //if (agent.isOnNavMesh == true)
                    //{
                    //    Debug.Log("new true");
                    //    destination = paths[animator.GetInteger("pathCount")].transform;
                    //    //agent.SetDestination(paths[animator.GetInteger("pathCount")].transform.position);
                    //    animator.SetTrigger("isMoving");
                    //}

                    //animator.SetFloat("targetDistance", agent.remainingDistance);


                }




            }
            
        //}
        yield return new WaitForSeconds(1f);
        if (runningUpdateWaypoint != null)
        {
            StopCoroutine(runningUpdateWaypoint);
            runningUpdateWaypoint = null;
        }
        runningUpdateWaypoint = Co_UpdateWaypoint();
        StartCoroutine(runningUpdateWaypoint);
    }

    

}

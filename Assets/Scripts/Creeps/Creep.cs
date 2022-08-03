using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class Creep : Unit
{
    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;

    //[SerializeField] private float carvingMoveThreshold = 0.1f;

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
    public IEnumerator runningUpdateWaypoint;

    public Animator animator;
    public List<Health> enemies = new List<Health>();

    public float test;


    public SkinnedMeshRenderer skinnedMeshRenderer;

    public float attackTime;

    public override void Death(Health objectHealth = null)
    {
        base.Death();
        HeroPerformanceData performanceData = GameManager.GetHeroData(health.damager);
        if (performanceData != null)
        {
            Debug.Log(health.damager.gameObject.name + " GAINED GOLD " + goldReward);
            performanceData.gold += goldReward;
            GameManager.OnUpdateHeroUIEvent.Invoke(performanceData);
        }




    }
    // Start is called before the first frame update
    protected override void Awake()
    {
      
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
    
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.carveOnlyStationary = false;
        obstacle.carving = true;

        animator = GetComponent<Animator>();

    }

    private void Start()
    {
    
        agent.speed = unitStat.startingMovementSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // urgent
       

    }

    private void Update()
    {
        if (!isStun)
        {
            //agent.isStopped = false;
            FaceTarget();
            
        }
        else
        {
            if (agent != null)
            {
                agent.isStopped = true;
            }
        
            Debug.Log(gameObject.name + " is stop");
        }
      

       // Debug.Log(attackTime);    
        //transform
    }

    protected override void InitializeValues()
    {
        base.InitializeValues();
        if (unitFaction == Faction.Dire)
        {
            Material[] mats = skinnedMeshRenderer.materials;
            mats[0] = SpawnManager.instance.redFace;
            skinnedMeshRenderer.materials = mats;
        }
        else if (unitFaction == Faction.Radiant)
        {
            Material[] mats = skinnedMeshRenderer.materials;
            mats[0] = SpawnManager.instance.whiteFace;
            skinnedMeshRenderer.materials = mats;
        }
        obstacle.enabled = false;
        agent.enabled = true;

       


        if (agent.isOnNavMesh == false)
        {
            agent.Warp(new Vector3(transform.position.x, 0f,transform.position.z));
            agent.enabled = false;
            agent.enabled = true;
        }
        currentWaypoint = waypoints[2].transform;
        StartCoroutine(Co_Load());


        if (TryGetComponent<Attributes>(out Attributes unit))
        {
            attackRate = (unit.baseAttackSpeed + unit.totalAttackSpeed + unit.agiFlatModifiers) / (100 * unit.baseAttackSpeed);
            attackTime = 1 / attackRate;
        }

    }

    protected override void DeinitializeValues()
    {
        base.DeinitializeValues();
        obstacle.enabled = false;
        agent.enabled = false;
    }

    IEnumerator Co_Load()
    {
   
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

    bool IsInRange()
    {
        Vector2 t = new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z);
        Vector2 cp = new Vector2(transform.position.x, transform.position.z);


        return Vector2.Distance(t, cp) <= detectionRadius;
           //return vector 2 dist tcp, <= cp 
    }

    void CheckDistanceFromTarget()
    {
        //check current target if its still within detection radius
        Vector2 t = new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z);
        Vector2 cp = new Vector2(transform.position.x, transform.position.z);
        
        if (currentTarget != null)
        {
            //check if current target is within attack radius
            if (Vector2.Distance(t, cp) <= attackRadius) //if within attack radius it will start attacking
            {

                animator.SetTrigger("isAttacking");
                animator.SetBool("isFollowingPath", false);
               // agent.speed;

            }
            else   //it isnt within attack radius
            {
              
                destination = currentTarget.transform;
                animator.SetTrigger("isMoving");
                agent.speed = unitStat.startingMovementSpeed;

            }
        }
        else
        {
           
            if (destination != null)
            {
                animator.SetTrigger("isMoving");
            }
         
        }
       
       
    }

    void CheckEnemyTargetStatus()
    {
        //check if current target still within range
        if (currentTarget != null)
        {
            if (IsInRange() == true) //if within attack radius
            {
                CheckDistanceFromTarget();
            }
       
        
            else if (IsInRange() == false)// not within detection radius anymore
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            //if enemy is within its detection radius, it sees enemy
            if (hitCollider.gameObject != gameObject)
            {
                if (hitCollider.gameObject.GetComponent<Health>()) 
                {
                    Health hitHealth = hitCollider.gameObject.GetComponent<Health>();
                    if (hitHealth.invulnerable == false) //add it if it isnt invulnerable
                    {
                        if (!hitHealth.CompareTeam(unitFaction))
                        {
                            enemies.Add(hitHealth);
                            //organize, RULE OF THUMB; IF THREE NESTED IF, WE CAN REFRACTOR
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
    }

    void NewCurrentTarget(Health objectHealth = null)
    {
        //old enemy target that just died / moved out of detection range

        currentTarget = null;
        DetectEnemies();
        if (enemies.Count > 0) // AT LEAST 1 ENEMY FOUND
        {
            //Because the list was organized nearest to furthest before coming here, we'll just choose the next index
            currentTarget = enemies[0];
            currentTarget.OnDeathEvent.AddListener(DeregisterDeath);
            CheckDistanceFromTarget();
 
        }
        else // NO ENEMIES FOUND
        {
            if (agent != null)
            {
              
                if (currentWaypoint != null)
                {
                    destination = currentWaypoint;
                       
                    animator.SetBool("isFollowingPath", true);
                    animator.SetTrigger("isMoving");
 
                }
                else
                {
                  
                    if (destination != null)
                    {
                        animator.SetTrigger("isMoving");
                    }
                    else
                    {
                       
                        animator.SetTrigger("isIdle");
                    }
                       
                }

               
            }
        }
    }

    public IEnumerator Co_UpdateWaypoint()
    {
        
        Vector2 currPath = new Vector2(currentWaypoint.transform.position.x,currentWaypoint.transform.position.z);
        Vector2 currPos = new Vector2(transform.position.x, transform.position.z);
        if (Vector2.Distance(currPath, currPos) < 110f)
        {
            if (animator.GetInteger("pathCount") + 1 < waypoints.Count)
            {

                animator.SetInteger("pathCount", animator.GetInteger("pathCount") + 1);
                currentWaypoint = waypoints[animator.GetInteger("pathCount")].transform;
            }
        }
 
        yield return new WaitForSeconds(1f);
        if (runningUpdateWaypoint != null)
        {
            StopCoroutine(runningUpdateWaypoint);
            runningUpdateWaypoint = null;
        }
        runningUpdateWaypoint = Co_UpdateWaypoint();
        StartCoroutine(runningUpdateWaypoint);
    }

    
    public void FaceTarget()
    {
        var turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

    }
}

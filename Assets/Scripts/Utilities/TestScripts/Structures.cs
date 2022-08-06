using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Structures : Unit
{
    // Start is called before the first frame update

    private DebugManager debugManager;

    //public GameObject towerDetectGO;

    // public TowerDetection detection;

    //public Collider collideDetect;

    public UnityEvent whenDestroyed;

    protected override void Awake()
    {
        base.Awake();
        debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
        RequiredBuildingsDestroyedCounter = requiredBuildingsDeadForToBeVulnerable.Count;

        if (RequiredBuildingsDestroyedCounter == 0)
        {
            GetComponent<Health>().invulnerable = false;
        }
        else
        {
            GetComponent<Health>().invulnerable = true;
        }
        for (int i = 0; i < requiredBuildingsDeadForToBeVulnerable.Count; i++)
        {
          //  Debug.Log(gameObject.name + " " + requiredBuildingsDeadForToBeVulnerable[i].gameObject.name);
            requiredBuildingsDeadForToBeVulnerable[i].OnDeathEvent.AddListener(ReduceRequirement);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Events.OnTowerDied.AddListener(OnSelectStructure);
  
        //StartCoroutine(Co_Delay());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Events.OnTowerDied.RemoveListener(OnSelectStructure);
        if (health != null)
            health.OnDeathEvent.RemoveListener(Death);

        whenDestroyed.Invoke();
    }

    private void OnDestroy()
    {
        //if (health != null)
        //    health.OnDeathEvent.RemoveListener(Death);


        //requiredBuildingsDeadForToBeVulnerable[i].OnDeathEvent.RemoveListener(ReduceRequirement);
    }

    public void OnSelectStructure()
    {
        debugManager.structure = gameObject;
    }

    public override void Death(Health objectHealth = null)
    {
        //  Debug.Log("Death");
        // TowerDied();
        base.Death();
        int team = 0;
        if (health.team == 0)
        {
            team = 1;
        }
       
        foreach (HeroPerformanceData currentHero in GameManager.instance.teams[team].heroPerformanceData)
        {
            if (health.damager != null)
            {
                if (GameManager.GetHeroData(health.damager) != null)
                {
                    if (currentHero == GameManager.GetHeroData(health.damager))
                    {
                   //     Debug.Log(" SPLIT structure DEATH GAINED GOLD " + goldReward);
                        currentHero.gold += goldReward;
                        currentHero.networth = currentHero.gold;
                    }
                    
                   // Debug.Log(" SPLIT structure DEATH GAINED GOLD " + defaultGoldReward);
                    currentHero.gold += defaultGoldReward;
                    currentHero.networth = currentHero.gold;
                    
                }

            }
         
          
         
            GameManager.OnUpdateHeroUIEvent.Invoke(currentHero);

        }
    



        Destroy(gameObject);
    }

    public List<Health> requiredBuildingsDeadForToBeVulnerable;
    int RequiredBuildingsDestroyedCounter;

    void ReduceRequirement(Health objectHealth)
    {
        RequiredBuildingsDestroyedCounter--;
      //  Debug.Log(RequiredBuildingsDestroyedCounter);
        if (RequiredBuildingsDestroyedCounter == 0)
        {
            GetComponent<Health>().invulnerable = false;   
        }
    }

    void TowerDied()
    {
        Debug.Log("Tower Died");
        foreach(var towerDetection in requiredBuildingsDeadForToBeVulnerable)
        {
            if (towerDetection.gameObject.GetComponent<Health>() != null)
            {
                towerDetection.gameObject.GetComponent<Health>().invulnerable = false;
            }
       
            if (towerDetection.gameObject.GetComponentInChildren<TowerDetection>() != null)
            {
                towerDetection.gameObject.GetComponentInChildren<TowerDetection>().enabled = true;
                towerDetection.gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            }
           
            // do collider next
        }
       
    }

}

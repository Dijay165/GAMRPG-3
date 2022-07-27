using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Structures : Unit
{
    // Start is called before the first frame update

    private DebugManager debugManager;

    //public GameObject towerDetectGO;

   // public TowerDetection detection;

    //public Collider collideDetect;

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


    }
  
    public void OnSelectStructure()
    {
        debugManager.structure = gameObject;
    }

    public override void Death(Health objectHealth = null)
    {
      //  Debug.Log("Death");
       // TowerDied();
        DeinitializeValues();

        Destroy(gameObject);
    }

    public List<Health> requiredBuildingsDeadForToBeVulnerable;
    int RequiredBuildingsDestroyedCounter;

    void ReduceRequirement(Health objectHealth)
    {
        RequiredBuildingsDestroyedCounter--;
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

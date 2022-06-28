using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Structures : MonoBehaviour
{
    // Start is called before the first frame update

    private DebugManager debugManager;
    [HideInInspector]
    public Health health;
    private HealthOverheadUI healthOverheadUI;
    public TestStatsHolder statsHolder;
    protected virtual void InitializeValues()
    {
      

        healthOverheadUI = HealthOverheadUIPool.pool.Get();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);


       

    }

    protected virtual void DeinitializeValues()
    {

        health.OnHealthModifyEvent.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.RemoveListener(healthOverheadUI.OnHealthDied);
        //health.OnDeathEvent.RemoveAllListeners();

   
    }
    private void Start()
    {
        debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
        health = GetComponent<Health>();
        statsHolder = GetComponent<TestStatsHolder>();

        if (health != null)
            health.OnDeathEvent.AddListener(Death);

    }

    private void OnEnable()
    {
        Events.OnTowerDied.AddListener(OnSelectStructure);
        StartCoroutine(Co_Delay());
    }
    IEnumerator Co_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        InitializeValues();
    }
    private void OnDisable()
    {
        Events.OnTowerDied.RemoveListener(OnSelectStructure);
        if (health != null)
            health.OnDeathEvent.RemoveListener(Death);


    }


    public void OnSelectStructure()
    {
        debugManager.structure = gameObject;
    }

    public virtual void Death(Health objectHealth = null)
    {
        Debug.Log("Death");
        DeinitializeValues();
        Destroy(gameObject);
    }

    public List<Health> requiredBuildingsDeadForToBeVulnerable;
    int RequiredBuildingsDestroyedCounter;
    // Start is called before the first frame update
    void Awake()
    {
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
    void ReduceRequirement(Health objectHealth)
    {
        RequiredBuildingsDestroyedCounter--;
        if (RequiredBuildingsDestroyedCounter == 0)
        {
            GetComponent<Health>().invulnerable = false;
        }
    }

}

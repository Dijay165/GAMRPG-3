using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Health> requiredBuildingsDeadForToBeVulnerable;
    int RequiredBuildingsDestroyedCounter;
    // Start is called before the first frame update
    void Start()
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
        if (RequiredBuildingsDestroyedCounter == 0)
        {
            GetComponent<Health>().invulnerable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

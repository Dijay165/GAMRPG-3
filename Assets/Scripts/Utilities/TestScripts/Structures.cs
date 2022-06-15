using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour
{
    // Start is called before the first frame update

    private DebugManager debugManager;
    [HideInInspector]
    public Health health;

    public TestStatsHolder statsHolder;

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

    public void Death(Health objectHealth = null)
    {
        Debug.Log("Death");
        Destroy(gameObject);
    }



}

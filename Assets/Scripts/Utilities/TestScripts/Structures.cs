using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour
{
    // Start is called before the first frame update

    private DebugManager debugManager;
    [HideInInspector]
    public Health health;

    private void Start()
    {
        debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
        health = GetComponent<Health>();

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
        //  Events.OnTowerDied.Invoke();
        debugManager.structure = gameObject;
    }

    public void Death(Health objectHealth = null)
    {
        Debug.Log("Death");
    //    health.OnDeathEvent.AddListener(Death);
        Destroy(gameObject);
    }



}

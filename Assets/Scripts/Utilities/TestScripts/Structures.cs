using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour
{
    // Start is called before the first frame update

    private DebugManager debugManager;

    private void Start()
    {
        debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
    }

    private void OnEnable()
    {
        Events.OnTowerDied.AddListener(OnSelectStructure);
    }

    private void OnDisable()
    {
        Events.OnTowerDied.RemoveListener(OnSelectStructure);
    }


    public void OnSelectStructure()
    {
        //  Events.OnTowerDied.Invoke();
        debugManager.structure = gameObject;
    }


}

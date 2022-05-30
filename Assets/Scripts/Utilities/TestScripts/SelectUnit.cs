using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private TestStatsHolder testStatsHolder;
    public UnitSelectionManager selectionManager;


    void Start()
    {
        testStatsHolder = GetComponent<TestStatsHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        Debug.Log("Click");
        selectionManager.unitStat = testStatsHolder.unitStat;
        selectionManager.ChangeInfo();
    }

    
}

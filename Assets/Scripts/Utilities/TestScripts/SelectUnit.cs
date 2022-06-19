using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
  It has the same functionality as a normal event. You add and remove the desired event that you wish to connect with. 
   reference: https://www.youtube.com/watch?v=RPhTEJw6KbI&list=PLuiBbLS_hU1uu5bMXVceRpHBxO7fSmeLd&index=48
*/
public class SelectUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private TestStatsHolder testStatsHolder;
    private UnitSelectionUI selectionUI;
 //   TargetedDamager targetedDamager;


    private Camera cam;
    private Ray ray;
    RaycastHit raycastHit;

    void Start()
    {
        cam = Camera.main;
        selectionUI = GameObject.Find("UnitSelection").GetComponent<UnitSelectionUI>();
        testStatsHolder = GetComponent<TestStatsHolder>();
       // targetedDamager = GetComponent<TargetedDamager>();
    }

    private void OnEnable()
    {
        Events.OnUnitSelect.AddListener(CurrentUnitStatus);
    }

    private void OnDisable()
    {
        Events.OnUnitSelect.RemoveListener(CurrentUnitStatus);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit /*, 1000f*//*, unitLayerMask*/))
            {
                if (raycastHit.transform == transform)
                {
                    if(testStatsHolder != null)
                    {
                        selectionUI.ChangeInfo(testStatsHolder.unitStat);
                    }
                 

                    //Events.OnTowerDied.Invoke();
                    //Debug.Log("Test");
                    if(gameObject.TryGetComponent(out Structures structures))
                    {
//                        Events.OnTowerDied.Invoke();
                        structures.OnSelectStructure();
                      //  targetedDamager.targetHealth = structures.health;
                        
                    }
                }
            }
        }
    }

    public void CurrentUnitStatus()
    {
       // Debug.Log("Current");
        //selectionUI.ChangeInfo(testStatsHolder.unitStat);
    }


}

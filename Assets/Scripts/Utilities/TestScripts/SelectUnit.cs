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
   // private TestStatsHolder testStatsHolder;
    //private UnitSelectionUI selectionUI;
    //   TargetedDamager targetedDamager;
    Unit unit;
    private MiniSelectionUI miniSelectionUI;
    Attributes attributes;

    private Camera cam;
    private Ray ray;
    RaycastHit raycastHit;

    void Awake()
    {
        cam = Camera.main;
        //selectionUI = GameObject.Find("UnitSelection").GetComponent<UnitSelectionUI>();
        miniSelectionUI = GameObject.Find("MiniUnitSelection").GetComponent<MiniSelectionUI>();
        if (TryGetComponent<Unit>(out Unit unit))
        {
            this.unit = unit;
        }
 
        // targetedDamager = GetComponent<TargetedDamager>();
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
                    if(unit != null)
                    {
                        miniSelectionUI.border.gameObject.SetActive(true);
                        Events.OnUnitSelect.Invoke(unit);
                        FactionColor(unit);
               
                    }
                    else
                    {
                        Debug.Log("No Attributes");
                        miniSelectionUI.border.SetActive(false);
                    }
                    //Events.OnTowerDied.Invoke();
                    //Debug.Log("Test");
                    if (gameObject.TryGetComponent(out Structures structures))
                    {
                        //                        Events.OnTowerDied.Invoke();
                     //   Debug.Log("IsTower");
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

    void FactionColor(Unit unit)
    {
        if(unit.unitFaction == Faction.Radiant)
        {
           // Debug.Log("Radiant");
            miniSelectionUI.characterPortrait.color = Color.white;
        }
        else
        {
            //Debug.Log("Dire");
            miniSelectionUI.characterPortrait.color = Color.red;
        }
    }


}

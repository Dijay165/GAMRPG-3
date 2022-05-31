using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private TestStatsHolder testStatsHolder;
    public UnitSelectionManager selectionManager;

    private Camera cam;
    private Ray ray;
    RaycastHit raycastHit;

    //Unit Layer Tag
    public LayerMask unitLayerMask /*= 1 << 7*/;


   // public delegate void ClickUnit();
    //public event ClickUnit unitClicked;

    void Start()
    {
      
        cam = Camera.main;
        testStatsHolder = GetComponent<TestStatsHolder>();
    }

    private void OnEnable()
    {
        Debug.Log("Enalve");
        selectionManager.unitClicked += CurrentUnitStatus;
    }

    private void OnDisable()
    {
        selectionManager.unitClicked -= CurrentUnitStatus;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = cam.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out raycastHit, 1000f, unitLayerMask))
        //    {
        //        if (raycastHit.transform == transform)
        //        {
        //            //Do something 
        //            selectionManager.unitStat = testStatsHolder.unitStat;
        //            selectionManager.ChangeInfo();

        //            //if (testStatsHolder != null)
        //            //{
        //            //    Debug.Log("Hero" + raycastHit.transform.name);
                     
                       
        //            //}
                   
        //        }
        //    }


        //}
    }

    public void CurrentUnitStatus()
    {
        Debug.Log("Current");
       //selectionManager.unitStat = testStatsHolder.unitStat;
    }


    private void OnMouseDown()
    {
        // selectionManager.unitStat = testStatsHolder.unitStat;
        selectionManager.ChangeInfo(testStatsHolder.unitStat);
        //Debug.Log("Current");
    }


}

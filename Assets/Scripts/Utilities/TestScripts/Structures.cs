using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour
{
    // Start is called before the first frame update

    public DebugManager debugManager;
    private void OnEnable()
    {
        Events.OnTowerDied.AddListener(OnSelectStructure);
    }

    private void OnDisable()
    {
        Events.OnTowerDied.RemoveListener(OnSelectStructure);
    }


    private void OnMouseDown()
    {
        //if clicked, set the structure to this tower. 
        OnSelectStructure();

        Debug.Log("Click");
       // Events.OnTowerDied.Invoke();
    }

    public void OnSelectStructure()
    {
        //  Events.OnTowerDied.Invoke();
        debugManager.structure = gameObject;
     //  Destroy(debugManager.struc)
      //  Debug.Log("Destroyed");
       // Destroy(gameObject);
    }


}

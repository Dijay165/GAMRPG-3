using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaneData 
{
    public bool meleeBarracksIsAlive;
    public bool rangeBarracksIsAlive;
    public GameObject creepSpawnPoint;
    public List<GameObject> wayPoints = new List<GameObject>();

}

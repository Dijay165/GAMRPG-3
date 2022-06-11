using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamData
{
    [SerializeField] public string name;
    public int kills;

    [SerializeField] public List<GameObject> heroSpawnPoints = new List<GameObject>();
    public List<LaneData> lanes = new List<LaneData>();

}


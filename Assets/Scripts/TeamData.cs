using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamData
{
    [SerializeField] public string name;
    public int kills;
    [NonReorderable] public List<HeroPerformanceData> heroPerformanceData;
    [SerializeField][NonReorderable] public List<GameObject> heroSpawnPoints = new List<GameObject>();
    [NonReorderable]  public List<LaneData> lanes = new List<LaneData>();

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime = 0;
    [NonReorderable] public List<TeamData> teams = new List<TeamData>();
    private void Awake()
    {
        instance = instance ? instance : this;
    }

  

    public static List<GameObject> MakePath(int p_team, int p_lane)
    {
         List<GameObject> newPath = new List<GameObject>();
        int otherTeam = p_team == 0 ? 1 : 0; //If p_team variable is equals to 0, make value 1, else (if its 1) make value 0
        foreach (GameObject go in instance.teams[p_team].lanes[p_lane].wayPoints)
        {

            newPath.Add(go);
        }

        for (int i = instance.teams[otherTeam].lanes[p_lane].wayPoints.Count - 1; i >= 0; i--)
        {

            newPath.Add(instance.teams[otherTeam].lanes[p_lane].wayPoints[i]);

        }



        return newPath;
    }
}

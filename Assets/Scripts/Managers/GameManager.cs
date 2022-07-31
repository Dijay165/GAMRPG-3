using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class HeroPerformanceData
{
    public Unit unit;
    public int networth;
    public int gold;
    public int killstreak;
    public int kills;
    public int deaths;
    public int buybackPrice;
    public int spawnTime;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime = 0;
    public Unit lastUnitSelect;
    public GameObject gameOverUI;
    public TextMeshProUGUI winnerText;
    [NonReorderable] public List<TeamData> teams = new List<TeamData>();
    public List<int> killStreakGoldReward;
    

    //Invoke event when nexus is destoryed, when called end the game. 
    private void Awake()
    {
        instance = instance ? instance : this; ///wow 

      
    }

    private void OnEnable()
    {
        Events.OnGameOver.AddListener(WinCondition);
    }

    private void OnDisable()
    {
        Events.OnGameOver.RemoveListener(WinCondition);
    }
    public static int GetKillStreakGold(int p_killstreak)
    {
       
        if (p_killstreak >= 10)
        {
            return instance.killStreakGoldReward[instance.killStreakGoldReward.Count - 1];
        }
        else
        {
            return instance.killStreakGoldReward[p_killstreak];
        }
    }
    public static HeroPerformanceData GetHeroData(Unit hero)
    {
        foreach (TeamData currentTeam in instance.teams)
        {
            foreach (HeroPerformanceData currentHero in currentTeam.heroPerformanceData)
            {
                if (currentHero.unit == hero)
                {
                    return currentHero;
                }
            }
        }
        Debug.Log("NONE FOUND");
        return null;
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

    //Wincondition function here. Time.timescale = 0; UI Victory screen will be shown  

    public void WinCondition(Unit unit)
    {

        winnerText.text = unit.unitFaction.ToString(); 
        gameOverUI.SetActive(true);
        Time.timeScale = 0;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class AddHeroUI : UnityEvent { }
public class UpdateHeroUI : UnityEvent<HeroPerformanceData> { }
public class UpdateHeroTimeUI : UnityEvent<HeroPerformanceData> { }
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
    public int goldPerMinute = 1;
    public float gameTime = 0;
    public Unit lastUnitSelect;
    public GameObject gameOverUI;
    public TextMeshProUGUI winnerText;
    [NonReorderable] public List<TeamData> teams = new List<TeamData>();
    public List<int> killStreakGoldReward;
    public List<int> expPerLevel = new List<int>();
    public static UpdateHeroUI OnUpdateHeroUIEvent = new UpdateHeroUI();
    public static UpdateHeroTimeUI OnUpdatarHeroTimeUI = new UpdateHeroTimeUI();
    public static AddHeroUI OnAddHeroUIEvent = new AddHeroUI();
    //Invoke event when nexus is destoryed, when called end the game. 
    private void Awake()
    {
        instance = instance ? instance : this; ///wow 

      
    }

    private void OnEnable()
    {
        Events.OnGameOver.AddListener(WinCondition);
        StartCoroutine(GoldPerMinute());
    }

    private void OnDisable()
    {
        Events.OnGameOver.RemoveListener(WinCondition);
    }




    IEnumerator GoldPerMinute()
    {
        float goldTime = 60;
        while (goldTime > 0)
        {
            yield return new WaitForSeconds(1f);
            goldTime -= 1;
           
        }
        foreach(HeroPerformanceData hpd in GameManager.instance.teams[0].heroPerformanceData)
        {
            hpd.gold += goldPerMinute;
            hpd.networth = hpd.gold;
            GameManager.OnUpdateHeroUIEvent.Invoke(hpd);
        }
        foreach (HeroPerformanceData hpd in GameManager.instance.teams[1].heroPerformanceData)
        {
            hpd.gold += goldPerMinute;
            hpd.networth = hpd.gold;
            GameManager.OnUpdateHeroUIEvent.Invoke(hpd);
        }
        StartCoroutine(GoldPerMinute());

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
    public static int GetHeroDataTeamIndex(HeroPerformanceData hero)
    {
        for (int i = 0; i < instance.teams.Count; i++)
        {
            for (int ii = 0; ii < instance.teams[i].heroPerformanceData.Count; ii++)
            {
                if (instance.teams[i].heroPerformanceData[ii] == hero)
                {
                    return i;
                }
            }
        }
        Debug.Log("NONE FOUND");
        return -1;
    }
    public static int GetHeroDataIndex(HeroPerformanceData hero)
    {
        foreach (TeamData currentTeam in instance.teams)
        {
            for (int i = 0; i < currentTeam.heroPerformanceData.Count;i++)
            {
                if (currentTeam.heroPerformanceData[i] == hero)
                {
                    return i;
                }
            }
        }
        Debug.Log("NONE FOUND");
        return -1;
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
       // Debug.Log("NONE FOUND");
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

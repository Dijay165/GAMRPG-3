using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopHUDUI : MonoBehaviour
{
    public RectTransform leftTeam;
    public RectTransform rightTeam;
    public List<AvatarUI> leftTeamAvatars;
    public List<AvatarUI> rightTeamAvatars;
    public AvatarUI prefab;

    private void Awake()
    {
        GameManager.OnAddHeroUIEvent.AddListener(AddUIs);
        GameManager.OnUpdatarHeroTimeUI.AddListener(UpdateUIs);
    }
    void AddUIs()
    {
        for (int i = 0; i < leftTeamAvatars.Count; i++)
        {
           Destroy(leftTeamAvatars[i].gameObject);

        }
        leftTeamAvatars.Clear();
        for (int i = 0; i < rightTeamAvatars.Count; i++)
        {
            Destroy(rightTeamAvatars[i].gameObject);

        }
        rightTeamAvatars.Clear();
        for (int i = 0; i < GameManager.instance.teams[0].heroPerformanceData.Count; i++)
        {
            AvatarUI instance = Instantiate(prefab, leftTeam);
            instance.SetUpUI(GameManager.instance.teams[0].heroPerformanceData[i].unit.unitStat.iconImage);
            leftTeamAvatars.Add(instance);
        }

        for (int i = 0; i < GameManager.instance.teams[1].heroPerformanceData.Count; i++)
        {
            AvatarUI instance = Instantiate(prefab, rightTeam);
            instance.SetUpUI(GameManager.instance.teams[1].heroPerformanceData[i].unit.unitStat.iconImage);
            rightTeamAvatars.Add(instance);
        }
    }
    void UpdateUIs(HeroPerformanceData curr)
    {
        int teamIndex = GameManager.GetHeroDataTeamIndex(curr);
        int index = GameManager.GetHeroDataIndex(curr);
        if (teamIndex == 0)
        {
            leftTeamAvatars[index].UpdateUI(GameManager.instance.teams[0].heroPerformanceData[index].spawnTime);


        }
        else
        {
            rightTeamAvatars[index].UpdateUI(GameManager.instance.teams[1].heroPerformanceData[index].spawnTime);
        }

    }

     
 
}

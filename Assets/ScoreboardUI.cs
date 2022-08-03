using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreboardUI : MonoBehaviour
{
    public RectTransform leftTeam;
    [SerializeField] GameObject scoreboardUI;

    public TMP_Text leftGold;

    public TMP_Text leftKills;
    public TMP_Text leftDeaths;

    public RectTransform rightTeam;

   
    public TMP_Text rightGold;

    public TMP_Text rightKills;
    public TMP_Text rightDeaths;

    public List<HeroScoreboardUI> leftTeamAvatars;
    public List<HeroScoreboardUI> rightTeamAvatars;
    public HeroScoreboardUI prefab;

    private void Awake()
    {
        GameManager.OnAddHeroUIEvent.AddListener(AddUIs);
        GameManager.OnUpdateHeroUIEvent.AddListener(UpdateUIs);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (scoreboardUI.activeSelf != true)
            {
                scoreboardUI.SetActive(true);
            }
            else
            {
                scoreboardUI.SetActive(false);
            }

        }
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
            HeroScoreboardUI instance = Instantiate(prefab, leftTeam);
            instance.SetUpUI(GameManager.instance.teams[0].heroPerformanceData[i].unit.unitStat.name.ToString(),
                GameManager.instance.teams[0].heroPerformanceData[i].unit.unitStat.iconImage);
            leftTeamAvatars.Add(instance);
        }

        for (int i = 0; i < GameManager.instance.teams[1].heroPerformanceData.Count; i++)
        {
            HeroScoreboardUI instance = Instantiate(prefab, rightTeam);
            instance.SetUpUI(GameManager.instance.teams[1].heroPerformanceData[i].unit.unitStat.name.ToString(), 
                GameManager.instance.teams[1].heroPerformanceData[i].unit.unitStat.iconImage);
            rightTeamAvatars.Add(instance);
        }
    }
    void UpdateUIs(HeroPerformanceData hpd)
    {
        int gold = 0;
        int kills = 0;
        int deaths = 0;
        for (int i = 0; i < GameManager.instance.teams[0].heroPerformanceData.Count; i++)
        {
            leftTeamAvatars[i].UpdateUI(GameManager.instance.teams[0].heroPerformanceData[i]);
            gold += GameManager.instance.teams[0].heroPerformanceData[i].gold;
            kills += GameManager.instance.teams[0].heroPerformanceData[i].kills;
            deaths += GameManager.instance.teams[0].heroPerformanceData[i].deaths;

        }

        leftGold.text = gold.ToString();

        leftKills.text = kills.ToString();
        leftDeaths.text = deaths.ToString();

        gold = 0;
        kills = 0;
        deaths = 0;
        for (int i = 0; i < GameManager.instance.teams[1].heroPerformanceData.Count; i++)
        {
            rightTeamAvatars[i].UpdateUI(GameManager.instance.teams[1].heroPerformanceData[i]);
            gold += GameManager.instance.teams[1].heroPerformanceData[i].gold;
            kills += GameManager.instance.teams[1].heroPerformanceData[i].kills;
            deaths += GameManager.instance.teams[1].heroPerformanceData[i].deaths;

        }
        rightGold.text = gold.ToString();

        rightKills.text = kills.ToString();
        rightDeaths.text = deaths.ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeroScoreboardUI : MonoBehaviour
{
    public Image avatar;
    public TMP_Text heroName;
    public TMP_Text level;
    public TMP_Text gold;
  
    public TMP_Text kills;
    public TMP_Text deaths;


    public void SetUpUI(string p_heroName, Sprite sprite)
    {
        avatar.sprite = sprite;

        heroName.text = p_heroName;
      
        level.text = "0";
        gold.text = "0";

        kills.text = "0";
        deaths.text = "0";
 
    }

    public void UpdateUI(HeroPerformanceData p_heroPerformanceData)
    {

        heroName.text = p_heroPerformanceData.unit.unitStat.name;

        level.text = p_heroPerformanceData.unit.level.currentLevel.ToString();
        gold.text = p_heroPerformanceData.gold.ToString();

        kills.text = p_heroPerformanceData.kills.ToString();
        deaths.text = p_heroPerformanceData.deaths.ToString();
      //  assists.text = p_heroPerformanceData.assist.ToString();

    }
}

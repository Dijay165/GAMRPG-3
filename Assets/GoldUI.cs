using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoldUI : MonoBehaviour
{

    public TMP_Text text;

    private void Awake()
    {
        GameManager.OnUpdateHeroUIEvent.AddListener(UpdateGold);
    }

    void UpdateGold(HeroPerformanceData hero)
    {
        if (hero != null)
        {
            if (hero == GameManager.instance.teams[0].heroPerformanceData[0])
            {
                text.text = hero.gold.ToString();
            }
     
        }

    }
}

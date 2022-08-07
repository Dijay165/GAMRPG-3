using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpgrade : MonoBehaviour
{
    // Start is called before the first frame update

    public Button statButton;
    Unit unit;
    Level level;
    public UpgradeSkillManager skillManager;

    private void Awake()
    {

        unit = PlayerManager.instance.player.GetComponent<Unit>();
        level = unit.GetComponent<Level>();
    }

    private void Update()
    {
        if (level.skillPoints > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        skillManager.OnUltActivate();
    }

    public void LevelStats()
    {
        Attributes attributes = unit.GetComponent<Attributes>();

        attributes.IncreaseStats();

        level.skillPoints--;
        Debug.Log(level.skillPoints + "Level Skillpoints");
        skillManager.lastUpgradeIndex = 0;

        skillManager.MaxLevelDecider();

        // skillManager.lastUpgradeIndex = 5;
        //skillManager.ButtonDecider(skillManager.lastUpgradeIndex);
    }
}

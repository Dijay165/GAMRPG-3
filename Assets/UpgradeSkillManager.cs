using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkillManager : MonoBehaviour
{
    // Start is called before the first frame update

    Unit unit;
    Level level;
    SkillHolder skillHolder;
    public List<Button> buttons;
    public SkillPointsIndicatorManager indicatorManager;
    public int lastUpgradeIndex;

    private void Awake()
    {
        unit = PlayerManager.instance.player.GetComponent<Unit>();
        level = unit.GetComponent<Level>();
        skillHolder = unit.GetComponent<SkillHolder>();
    }


    private void Update()
    {
        //Uncomment if finished 
        if (level.skillPoints > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void OnSkillUpgrade(int index)
    {

        indicatorManager.OnUpgradeSkill(skillHolder.skills[index].skillLevel, index);
     //   ButtonDecider(index);

        if (skillHolder.skills[index].skillLevel < skillHolder.skills[index].maxSkillLevel)
        {

            skillHolder.skills[index].skillLevel++;

            level.skillPoints--;
            lastUpgradeIndex = index;

        }
        else
        {
           // indicatorManager.OnUpgradeSkill(skillHolder.skills[index].skillLevel, index);

            skillHolder.skills[index].skillLevel = skillHolder.skills[index].maxSkillLevel;
            DisableButton(index);
            Debug.Log("Already Max");
        }

    }


    public void DisableButton(int index)
    {
        buttons[index].interactable = false;
        buttons[index].image.color = Color.gray;
    }

    public void LevelStats()
    {
        Attributes attributes = unit.GetComponent<Attributes>();

        attributes.IncreaseStats();
        level.skillPoints--;

    }

    public void ButtonDecider(int index)
    {

        if (index == lastUpgradeIndex)
        {
            DisableButton(index);
        }
        //if (level.skillPoints > 1)
        //{

           
         
        //}
    }
}

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

    private void Awake()
    {
        unit = PlayerManager.instance.player.GetComponent<Unit>();
        level = unit.GetComponent<Level>();
        skillHolder = unit.GetComponent<SkillHolder>();
    }


    private void Update()
    {
        //if (level.skillPoints > 0)
        //{
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }


    public void OnSkillUpgrade(int index)
    {


        if (skillHolder.skills[index].skillLevel < skillHolder.skills[index].maxSkillLevel)
        {

            skillHolder.skills[index].skillLevel++;

            indicatorManager.OnUpgradeSkill(skillHolder.skills[index].skillLevel - 1, index);



            //    Debug.Log(skillHolder.skills[index].name + " " + skillHolder.skills[index].skillLevel);

            level.skillPoints--;
           
            Debug.Log("Skillpoints: " + level.skillPoints);

         //   skillHolder.skills[index].skillLevel++;

        }
        else
        {
            indicatorManager.OnUpgradeSkill(skillHolder.skills[index].skillLevel, index);

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
}

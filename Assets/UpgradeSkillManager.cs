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

        if (skillHolder.skills[index].skillLevel < skillHolder.skills[index].maxSkillLevel)
        {
            skillHolder.skills[index].skillLevel++;
        }
        else
        {
            skillHolder.skills[index].skillLevel = skillHolder.skills[index].maxSkillLevel;
            DisableButton(index);
            //Debug.Log("Already Max");
        }

        level.skillPoints--;


        if (level.skillPoints <= 1)
        {
            Debug.Log("Skillpoints");
            ButtonDecider(index);
        }


        lastUpgradeIndex = index;

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
            Debug.Log("Disable specific buttons");

            DisableButton(index);
        }
        else
        {
            Debug.Log("Button Index");

            for(int i = 0; i < buttons.Count; i++)
            {
                if(i == lastUpgradeIndex)
                {

                    if(skillHolder.skills[i].skillLevel < skillHolder.skills[i].maxSkillLevel)
                    {
                        Debug.Log("Upgrade Skill Index");

                        buttons[i].interactable = true;
                        buttons[i].image.color = Color.white;
                       // break;
                    }
                    //else
                    //{
                    //    Debug.Log(skillHolder.skills[i].name);
                    //    Debug.Log("Skill is already max Index");

                    //}

                }
            }
         
        }
        //if (level.skillPoints > 1)
        //{

           
         
        //}
    }

}

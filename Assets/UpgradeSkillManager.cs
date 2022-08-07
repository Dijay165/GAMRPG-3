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
    public Button statButton;
    public SkillPointsIndicatorManager indicatorManager;
    public int lastUpgradeIndex;
    public int ultIndex;
    int ultCount;

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

            if (!skillHolder.skills[index].isUnlock)
            {
                skillHolder.skills[index].isUnlock = true;
            }
        }
        else
        {
           skillHolder.skills[index].isMaxLevel = true;
           skillHolder.skills[index].skillLevel = skillHolder.skills[index].maxSkillLevel;
            DisableButton(index);
            //Debug.Log("Already Max");
        }

        level.skillPoints--;


        if (level.skillPoints <= 1)
        {
          //  Debug.Log("Skillpoints");
            ButtonDecider(index);
        }




        lastUpgradeIndex = index;

    }


    public void DisableButton(int index)
    {
        buttons[index].interactable = false;
        buttons[index].image.color = Color.gray;
    }

    private void OnEnable()
    {
        statButton.transform.parent.gameObject.SetActive(true);

        OnUltActivate();
    }

    private void OnDisable()
    {
        statButton.transform.parent.gameObject.SetActive(false);
    }

    public void ButtonDecider(int index)
    {
        if (index == lastUpgradeIndex)
        {
           // Debug.Log("Disable specific buttons");

            DisableButton(index);
        }
        else
        {
            // Debug.Log("Button Index");

            MaxLevelDecider();
         
        }
    }

    public void MaxLevelDecider()
    {
        

        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == lastUpgradeIndex)
            {
                if (!skillHolder.skills[i].isMaxLevel)
                {
                    buttons[i].interactable = true;
                    buttons[i].image.color = Color.white;
                }
            }
        }
    }
    public void OnUltActivate()
    {
        //  Debug.Log("OnUltActivate");
         ultCount = skillHolder.skills[3].ultimateIndexReq.Count;
     //   Debug.Log("Ult Count: " + ultCount);

        Debug.Log(buttons.Count - 1);
        if (level.currentLevel == skillHolder.skills[3].ultimateIndexReq[ultIndex])
        {
        
            buttons[buttons.Count - 1].interactable = true;
            buttons[buttons.Count - 1].image.color = Color.white;
          //  ultIndex++;
        }
        else
        {
            DisableButton(buttons.Count - 1);
        }
    }


    public void UltIndexAdder()
    {
        if(ultIndex < ultCount - 1)
        {

            ultIndex++;

            Debug.Log("if Clicking : " + ultIndex);

        }
        else
        {


            ultIndex = ultCount - 1;

            Debug.Log("Else Clicking : " + ultIndex);

        }

    }

    public void LevelStats()
    {
        Attributes attributes = unit.GetComponent<Attributes>();

        attributes.IncreaseStats();

        level.skillPoints--;
        Debug.Log(level.skillPoints + "Level Skillpoints");
        lastUpgradeIndex = 0;

        MaxLevelDecider();

    }
}

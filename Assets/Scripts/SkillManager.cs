using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    int skillIndex;
    public List<Button> buttons;
    public List<TextMeshProUGUI> text;
    SkillHolder player;
    public static SkillManager instance;
    public List<float> cooldownIndexes;

    float refCountdown;
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillHolder>();
       
    }
    void Start()
    {
      
       
    }
    private void OnEnable()
    {
        Events.OnPlayerSkillIndex.AddListener(skillGetter);
        InitializeButtons();
    }

    private void OnDisable()
    {
        Events.OnPlayerSkillIndex.RemoveListener(skillGetter);
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.skills[skillIndex].isCooldown)
        //{
        //    buttons[skillIndex].GetComponent<Image>().fillAmount += 1 / 
        //        player.skills[skillIndex].coolDownDuration[player.skills[skillIndex].skillLevel] * Time.deltaTime;

        //    if(buttons[skillIndex].GetComponent<Image>().fillAmount >= 1)
        //    {
        //        buttons[skillIndex].GetComponent<Image>().fillAmount = 0;
        //    }
        //}

        //Compare all 
        // if(player.GetComponent<Mana>().currentMana < )
        ButtonChecker();
        LockButton();
    }


    public void ButtonChecker()
    {
        
        
        for(int i = 0; i < buttons.Count; i++)
        {
           // Debug.Log("Start Loop i " + i + " Button count " + buttons.Count);
            if (player.job.skills[i].skillType == SkillType.Active)
            {
              //  Debug.Log("Start Active " + i);
                if (player.GetComponent<Mana>().currentMana < player.job.skills[i].manaCost[player.job.skills[i].skillLevel])
                {
                //    Debug.Log("Active " + player.job.skills[i].name);
                    buttons[i].GetComponent<Image>().color = Color.blue;

                }
                else
                {
                  //  Debug.Log("Passive " + player.job.skills[i].name);
                    buttons[i].GetComponent<Image>().color = Color.white;

                }
              
            }
        }
    }

    public void LockButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            // Debug.Log("Start Loop i " + i + " Button count " + buttons.Count);
            if (player.job.skills[i].isUnlock)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<Image>().color = Color.white;

            }
            else
            {
                buttons[i].interactable = false;

                buttons[i].GetComponent<Image>().color = Color.grey;
            }
        }
    }



    void skillGetter(int index)
    {
        skillIndex = index;
      //  Debug.Log(skillIndex);
    }

    void InitializeButtons()
    {
      
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Image>().sprite = player.job.skills[i].skillIcon;
            text.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
            text[i].text = "";
        }

    }

    public void ActivateSkill(int index)
    {
       
        if (!player.job.skills[index].isCooldown)
        {
           // player.job.skills[index].OnActivate(player.GetComponent<Unit>());
           // StartCoroutine(player.job.skills[index].CoolDownEnumerator());
        }
    }


    public IEnumerator CountdownText(float cooldown, TextMeshProUGUI cooldownText)
    {
       
        float refCountdown = cooldown;
       // Debug.Log(skillIndex);

        cooldownIndexes.Add(refCountdown);

        while (refCountdown >= 0)
        {

            //for (int i = 0; i < text.Count; i++)
            //{
            //    if (player.job.skills[i].isCooldown)
            //    {
            //        text[i].text = refCountdown.ToString();
            //    }
            //}

            cooldownText.text = refCountdown.ToString();
    

            yield return new WaitForSeconds(1f);
           /// Debug.Log("After Countdown");
            refCountdown--;

        }

        cooldownText.text = "";
       // Debug.Log("Cooldown finish");
    }

}

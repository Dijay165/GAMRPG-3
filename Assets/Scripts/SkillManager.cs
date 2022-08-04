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


    public IEnumerator CountdownText(float cooldown)
    {
        
      //  refCountdown = cooldown;
     //   Debug.Log(refCountdown + " Start Cooldown");

        while (cooldown >= 0)
        {

            Debug.Log(cooldown + " Cooldown");
            text[skillIndex].text = cooldown.ToString();
            yield return new WaitForSeconds(1f);
            Debug.Log("After Countdown");
            cooldown--;

        }

        text[skillIndex].text = "";

    }

}

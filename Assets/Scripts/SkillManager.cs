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
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillHolder>();
    }
    void Start()
    {
        Events.OnPlayerSkillIndex.AddListener(skillGetter);
       
    }
    private void OnEnable()
    {
        
        InitializeButtons();
    }

    private void OnDisable()
    {
        Events.OnPlayerSkillIndex.RemoveListener(skillGetter);
    }

    // Update is called once per frame
    void Update()
    {
        
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


}

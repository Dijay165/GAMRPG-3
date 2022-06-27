using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*
  It has the same functionality as a normal event. You call the Events script to invoke the declared event variable that you've created. 
  please look at the SelectUnit.cs to see how to connect them. 
 reference: https://www.youtube.com/watch?v=RPhTEJw6KbI&list=PLuiBbLS_hU1uu5bMXVceRpHBxO7fSmeLd&index=48
*/
public class UnitSelectionUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;
    public Slider slider;
    public Image characterPortrait;


    //UnitStat unit;
    Attributes attributes;

    public void OnEnable()
    {
        Events.OnResetInfoUI.AddListener(ResetInfo);
        Events.OnPlayerSelect.AddListener(PlayerInfo);
    }

    private void OnDisable()
    {
        Events.OnResetInfoUI.RemoveListener(ResetInfo);
        Events.OnPlayerSelect.RemoveListener(PlayerInfo);
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // unit = player.GetComponent<TestStatsHolder>().unitStat;
        attributes = player.GetComponent<Attributes>();
       
    }
    private void Start()
    {
  
        PlayerInfo();
    }

   

    public void ResetInfo()
    {
        attackText.text = "";
        defenseText.text = "";
        moveSpeedText.text = "";
        maxHealthText.text = "";
    }

    public void PlayerInfo()
    {
        slider.maxValue = attributes.hp.maxHealth;
        slider.value = attributes.hp.currentHealth;
        attackText.text = attributes.attackDamage.ToString();
        defenseText.text = attributes.armor.ToString();
        moveSpeedText.text = attributes.movementSpeed.ToString();
        currentHealthText.text = attributes.hp.currentHealth.ToString();
        maxHealthText.text = attributes.hp.maxHealth.ToString();
        characterPortrait.color = Color.white;
        characterPortrait.sprite = attributes.portraitImage;

    }

    public void FactionPortrait(UnitStat unitStat)
    {
        if(unitStat.faction == Faction.Radiant)
        {
            characterPortrait.color = Color.white;
        }
        else
        {
            characterPortrait.color = Color.red;
        }
    }
}

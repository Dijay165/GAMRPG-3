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
    public TextMeshProUGUI healthPointText;
    public Image characterPortrait;


    UnitStat unit;
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

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        unit = player.GetComponent<TestStatsHolder>().unitStat;
        attributes = player.GetComponent<Attributes>();
    }
    //public void ChangeInfo(UnitStat unitStat)
    //{
    //    Events.OnUnitSelect.Invoke();
        
    //    if (unitStat != null)
    //    {
    //        attackText.text = unitStat.attack.ToString();
    //        defenseText.text = unitStat.defense.ToString();
    //        moveSpeedText.text = unitStat.moveSpeed.ToString();
    //        healthPointText.text = unitStat.healthPoints.ToString();
    //    }
    //}
    public void ChangeAttributeUI(Attributes unitStat)
    {
        Events.OnUnitSelect.Invoke();
      //  Debug.Log("Touched");
        if (unitStat != null)
        {
            attackText.text = unitStat.attackDamage.ToString();
            defenseText.text = unitStat.armor.ToString();
            moveSpeedText.text = unitStat.movementSpeed.ToString();
            healthPointText.text = unitStat.health.ToString();
            characterPortrait.sprite = unitStat.portraitImage;
        }
    }

    public void ResetInfo()
    {
        attackText.text = "";
        defenseText.text = "";
        moveSpeedText.text = "";
        healthPointText.text = "";
    }

    public void PlayerInfo()
    {

        attackText.text = attributes.attackDamage.ToString();
        defenseText.text = attributes.armor.ToString();
        moveSpeedText.text = attributes.movementSpeed.ToString();
        healthPointText.text = attributes.health.ToString();
        characterPortrait.sprite = attributes.portraitImage;
    }

}

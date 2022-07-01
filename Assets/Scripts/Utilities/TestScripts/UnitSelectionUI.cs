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


    Unit playerUnit; //TEMPORARY MAKE THIS EVENT

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
   
        if (player.TryGetComponent<Unit>(out Unit unit))
        {
     
            playerUnit = unit;
        }
    }
    public void ChangeInfo(Unit unit)
    {
        Events.OnUnitSelect.Invoke();
        
        if (unit != null)
        {
            if (unit.TryGetComponent<Health>(out Health health))
            {
                healthPointText.text = health.GetHealth().ToString();
            }
            if (unit.TryGetComponent<Attributes>(out Attributes attributes))
            {
                attackText.text = attributes.attackDamage.ToString();
                defenseText.text = attributes.armor.ToString();
                moveSpeedText.text = attributes.movementSpeed.ToString();
            } 
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
        if (playerUnit != null)
        {
            if (playerUnit.TryGetComponent<UnitStat>(out UnitStat unitStat))
            {
                characterPortrait.sprite = unitStat.portraitImage;
            }
            if (playerUnit.TryGetComponent<Health>(out Health health))
            {
                healthPointText.text = health.GetHealth().ToString();
            }
            if (playerUnit.TryGetComponent<Attributes>(out Attributes attributes))
            {
                attackText.text = attributes.attackDamage.ToString();
                defenseText.text = attributes.armor.ToString();
                moveSpeedText.text = attributes.movementSpeed.ToString();
                
            }
        }
       
    }

}

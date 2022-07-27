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
    [Header("Left Section")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI healthText;
    public Image characterPortrait;

    [Header("Middle Section")]
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI healthRegenText;
    public TextMeshProUGUI manaRegenText;
    public TextMeshProUGUI currentManaText;
    public TextMeshProUGUI maxManaText;
    public Slider sliderHealth;
    public Slider sliderMana;


    [Header("Attack Hover Section")]
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI attackRangeText;
    public TextMeshProUGUI magicAttackText;


    [Header("Defense Hover Section")]
    public TextMeshProUGUI armor;
    public TextMeshProUGUI armorTypeText;
    public TextMeshProUGUI magicResistText;


    Unit playerUnit; //TEMPORARY MAKE THIS EVENT

    public void Start()
    {
      //  StartCoroutine(Temporary());
    }

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
   
        if (player.TryGetComponent<Unit>(out Unit unit))
        {
     
            playerUnit = unit;
        }
    }


    //Can be improved
    private void Update()
    {
        PlayerInfo();   
    }
    public void ChangeInfo(Unit unit)
    {
        Events.OnUnitSelect.Invoke(unit);
        
        if (unit != null)
        {
           
            characterPortrait.sprite = unit.unitStat.portraitImage;
            
            if (unit.TryGetComponent<Health>(out Health health))
            {
                //healthPointText.text = health.GetHealth().ToString();
                sliderHealth.maxValue = health.maxHealth;
                sliderHealth.value = health.currentHealth;
            }
            if (unit.TryGetComponent<Attributes>(out Attributes attributes))
            {
                attackText.text = attributes.attackDamage.ToString();
                defenseText.text = attributes.armor.ToString();
                moveSpeedText.text = attributes.movementSpeed.ToString();
                healthRegenText.text = attributes.healthRegen.ToString();
                manaRegenText.text = attributes.manaRegen.ToString();

            } 
        }
    }

   

    public void ResetInfo()
    {
        attackText.text = "";
        defenseText.text = "";
        moveSpeedText.text = "";
        healthRegenText.text = "";
        manaRegenText.text = "";
        maxHealthText.text = "";

    }

    public void PlayerInfo()
    {
        if (playerUnit != null)
        {
            
            characterPortrait.sprite = playerUnit.unitStat.portraitImage;
            
            if (playerUnit.TryGetComponent<Health>(out Health health))
            {
                currentHealthText.text = health.GetHealth().ToString();
                maxHealthText.text = health.maxHealth.ToString();
                sliderHealth.maxValue = health.maxHealth;
                sliderHealth.value = health.currentHealth;
            }

            if(playerUnit.TryGetComponent<Mana>(out Mana mana))
            {
                currentManaText.text = mana.currentMana.ToString();
                maxManaText.text = mana.maxMana.ToString();
                sliderMana.maxValue = mana.maxMana;
                sliderMana.value = mana.currentMana;
            }

            if (playerUnit.TryGetComponent<Attributes>(out Attributes attributes))
            {
                attackText.text = attributes.attackDamage.ToString();
                defenseText.text = attributes.armor.ToString();
                moveSpeedText.text = attributes.movementSpeed.ToString();
                healthRegenText.text = "+ " + attributes.healthRegen.ToString();
                manaRegenText.text = "+ " + attributes.manaRegen.ToString();
                manaText.text = manaRegenText.text;
                healthText.text = healthRegenText.text;

                attackSpeedText.text = attributes.totalAttackSpeed.ToString();
                attackRangeText.text = attributes.attackRange.ToString();
                magicAttackText.text = attributes.magicAttack.ToString();

                armor.text = attributes.totalArmor.ToString();
                armorTypeText.text = attributes.armorType.ToString();
                magicResistText.text = attributes.totalMagicResistance.ToString();

            }


        }
       
    }

    public void FactionPortrait(Health unitStat)
    {
        if(unitStat.CompareTeam(Faction.Radiant))
        {
            characterPortrait.color = Color.white;
        }
        else
        {
            characterPortrait.color = Color.red;
        }
    }
}
